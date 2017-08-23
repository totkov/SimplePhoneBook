namespace SimplePhoneBook.Data.Common.Repositories
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
