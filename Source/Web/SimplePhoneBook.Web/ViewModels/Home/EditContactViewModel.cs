namespace SimplePhoneBook.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;

    public class EditContactViewModel
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [MaxLength(30)]
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [MaxLength(2000)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}