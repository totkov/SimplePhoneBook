namespace SimplePhoneBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SimplePhoneBook.Data.Common.Models;

    public class Contact : BaseModel<int>
    {
        [MaxLength(30)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(30)]
        [Required]
        public string LastName { get; set; }

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }
    }
}
