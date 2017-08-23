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

        public IQueryable<Contact> AllContacts()
        {
            return this.contacts.All();
        }

        public Contact GetById(int id)
        {
            return this.contacts.GetById(id);
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
