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
        public string Username { get; set; }


        [Required]
        [Display(Name = "Prenume")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name ="Nume")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Data nașterii")]
        public DateTime DateOfBirth { get; set; }
       
        [Display(Name = "Localitate/Oraș")]
        public string  City { get; set; }

        [Required]
        [Display(Name = "Țara")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
            ErrorMessage ="Format invalid")]
        public string Email  { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }
    }
}
