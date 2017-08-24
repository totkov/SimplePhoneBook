namespace SimplePhoneBook.Services.Data
{
    using System;
    using System.Linq;

    using SimplePhoneBook.Data.Models;
    using SimplePhoneBook.Data.Common.Repositories;
    using SimplePhoneBook.Services.Data.Contracts;

    public class ContactsService : IContactsService
    {
        private IDbRepository<Contact> contacts;
        private IUnitOfWork uof;

        public ContactsService(IDbRepository<Contact> contacts, IUnitOfWork uof)
        {
            this.contacts = contacts;
            this.uof = uof;
        }

        public IQueryable<Contact> AllContacts(int? page = null, int? pageSize = null)
        {
            IQueryable<Contact> query = this.contacts.All();
            if (page != null && pageSize != null)
            {
                query = query
                    .OrderBy(i => i.Id)
                    .Skip(page.Value * pageSize.Value)
                    .Take(pageSize.Value);
            }
            return query.AsQueryable();
        }

        public Contact GetById(int id)
        {
            return this.contacts.GetById(id);
        }
        
        public int Count()
        {
            return this.contacts.All().Count();
        }

        public void Add(Contact contact)
        {
            this.contacts.Add(contact);
            this.uof.Commit();
        }

        public void Delete(int id)
        {
            this.contacts.Delete(this.GetById(id));
            this.uof.Commit();
        }

        public void Edit(Contact contact)
        {
            Contact dbContact = this.GetById(contact.Id);
            dbContact.FirstName = contact.FirstName;
            dbContact.LastName = contact.LastName;
            dbContact.PhoneNumber = contact.PhoneNumber;
            dbContact.Description = contact.Description;
            this.contacts.Update(dbContact);
            this.uof.Commit();
        }
    }
}
