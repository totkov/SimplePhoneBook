namespace SimplePhoneBook.Services.Data.Contracts
{
    using System.Linq;

    using SimplePhoneBook.Data.Models;

    public interface IContactsService
    {
        IQueryable<Contact> AllContacts(int? page = null, int? pageSize = null);

        Contact GetById(int id);

        int Count();

        void Add(Contact contact);

        void Delete(int id);

        void Edit(Contact contact);
    }
}
