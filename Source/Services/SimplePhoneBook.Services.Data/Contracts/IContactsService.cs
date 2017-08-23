namespace SimplePhoneBook.Services.Data.Contracts
{
    using System.Linq;

    using SimplePhoneBook.Data.Models;

    public interface IContactsService
    {
        IQueryable<Contact> AllContacts();

        Contact GetById(int id);

        void Add(Contact contact);

        void Delete(int id);

        void Edit(Contact contact);
    }
}
