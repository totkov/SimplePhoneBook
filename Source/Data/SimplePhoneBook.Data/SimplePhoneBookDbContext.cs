namespace SimplePhoneBook.Data
{
    using System;
    using System.Linq;
    using System.Data.Entity;

    using SimplePhoneBook.Data.Models;
    using SimplePhoneBook.Data.Common.Models;

    public class SimplePhoneBookDbContext : DbContext
    {
        public SimplePhoneBookDbContext() : base("SimplePhoneBookDb")
        {
        }

        public virtual IDbSet<Contact> Contacts { get; set; }

        public override int SaveChanges()
        {
            try
            {
                this.ApplyAuditInfoRules();
                return base.SaveChanges();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
