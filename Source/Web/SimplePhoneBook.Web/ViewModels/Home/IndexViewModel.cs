namespace SimplePhoneBook.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<ContactViewModel> Contacts { get; set; }
    }
}