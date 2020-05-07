using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.ViewModels
{
    public class AddUserViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string Username { get; set; }


        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name ="Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Birthday")]
        public DateTime DateOfBirth { get; set; }
       
        public string  City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
            ErrorMessage ="Please enter an email in a correct format")]
        public string Email  { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
