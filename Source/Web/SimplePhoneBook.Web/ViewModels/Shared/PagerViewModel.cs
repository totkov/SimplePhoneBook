namespace SimplePhoneBook.Web.ViewModels.Shared
{
    public class PagerViewModel
    {
        public string Prefix { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}