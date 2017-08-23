namespace SimplePhoneBook.Web
{
    using System.Data.Entity;

    using SimplePhoneBook.Data;
    using SimplePhoneBook.Data.Migrations;

    public class DataBaseConfig
    {
        public static void RegisterMigrations()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SimplePhoneBookDbContext, Configuration>());
        }
    }
}