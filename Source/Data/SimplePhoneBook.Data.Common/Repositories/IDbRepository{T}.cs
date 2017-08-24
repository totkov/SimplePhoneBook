namespace SimplePhoneBook.Data.Common.Repositories
{
    using System.Linq;

    using SimplePhoneBook.Data.Common.Models;

    public interface IDbRepository<T> where T : class, IAuditInfo, IDeletableEntity
    {
        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        T GetById(object id);

        int Count();

        void Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);

        void Update(T entity);
    }
}
