namespace SimplePhoneBook.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using SimplePhoneBook.Web.ViewModels.Shared;

    public class IndexViewModel
    {
        public IEnumerable<ContactViewModel> Contacts { get; set; }

        public PagerViewModel ContactsPager { get; set; }
    }
}