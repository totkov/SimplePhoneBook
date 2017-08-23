namespace SimplePhoneBook.Data.Common.Repositories
{
    using System;
    using System.Linq;
    using System.Data.Entity;

    using SimplePhoneBook.Data.Common.Models;

    public class EfRepository<T> : IDbRepository<T> where T : class, IAuditInfo, IDeletableEntity
    {
        private IDbSet<T> dbSet;
        private DbContext context;

        public EfRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", nameof(context));
            }
            this.context = context;
            this.dbSet = this.context.Set<T>();
        }

        public IQueryable<T> All()
        {
            return this.dbSet.Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return this.dbSet;
        }

        public T GetById(object id)
        {
            var item = this.dbSet.Find(id);
            if (item.IsDeleted)
            {
                return null;
            }
            return item;
        }

        public void Add(T entity)
        {
            this.dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
        }

        public void HardDelete(T entity)
        {
            this.dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            this.dbSet.Attach(entity);
            var entry = this.context.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }
}
