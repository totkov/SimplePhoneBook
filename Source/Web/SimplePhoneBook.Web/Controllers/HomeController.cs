namespace SimplePhoneBook.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using SimplePhoneBook.Web.ViewModels.Home;
    using SimplePhoneBook.Web.ViewModels.Shared;
    using SimplePhoneBook.Data.Models;
    using SimplePhoneBook.Services.Data.Contracts;

    public class HomeController : Controller
    {
        private IContactsService contacts;

        public HomeController(IContactsService contacts)
        {
            this.contacts = contacts;
        }
        
        public ActionResult Index(IndexViewModel model)
        {
            model.ContactsPager = model.ContactsPager ?? new PagerViewModel();
            model.ContactsPager.ItemsPerPage = model.ContactsPager.ItemsPerPage == 0 ? 5 : model.ContactsPager.ItemsPerPage;
            model.ContactsPager.PagesCount = (int)Math.Ceiling((double)this.contacts.Count() / model.ContactsPager.ItemsPerPage);
            model.ContactsPager.Prefix = "ContactsPager";

            model.Contacts = this.contacts
                    .AllContacts(model.ContactsPager.CurrentPage, model.ContactsPager.ItemsPerPage)
                    .Select(c => new ContactViewModel
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        PhoneNumber = c.PhoneNumber
                    })
                    .ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Contact dbContact = contacts.GetById(id);
            if (dbContact == null)
            {
                return this.HttpNotFound();
            }
            ContactDetailsViewModel model = new ContactDetailsViewModel
            {
                Id = dbContact.Id,
                FirstName = dbContact.FirstName,
                LastName = dbContact.LastName,
                PhoneNumber = dbContact.PhoneNumber,
                Description = dbContact.Description,
                CreatedOn = dbContact.CreatedOn,
                ModifiedOn = dbContact.ModifiedOn
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddContactViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Contact dbContact = new Contact
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Description = model.Description
                };
                this.contacts.Add(dbContact);
                this.TempData["SuccessMsg"] = "Contact is added to the database!";
                return this.RedirectToAction("Index", "Home");
            }
            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Contact dbContact = contacts.GetById(id);
            if (dbContact == null)
            {
                return this.HttpNotFound();
            }
            EditContactViewModel model = new EditContactViewModel
            {
                Id = dbContact.Id,
                FirstName = dbContact.FirstName,
                LastName = dbContact.LastName,
                PhoneNumber = dbContact.PhoneNumber,
                Description = dbContact.Description
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditContactViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Contact dbContact = new Contact
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Description = model.Description
                };
                this.contacts.Edit(dbContact);
                this.TempData["SuccessMsg"] = "Contact is updated!";
                return this.RedirectToAction("Index", "Home");
            }
            return this.View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Contact dbContact = this.contacts.GetById(id);
            if (dbContact == null)
            {
                return this.HttpNotFound();
            }
            ContactViewModel model = new ContactViewModel
            {
                Id = dbContact.Id,
                FirstName = dbContact.FirstName,
                LastName = dbContact.LastName,
                PhoneNumber = dbContact.PhoneNumber
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.contacts.Delete(id);
            this.TempData["SuccessMsg"] = "Contact is deleted!";
            return this.RedirectToAction("Index", "Home");
        }
    }
}