using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.Models.Manage
{
    public class Index
    {

        [Display(Name = "First name")]
        public string FirstName { get; set; }


        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Birthday")]
        [DataType(DataType.Date)] //format of type dd/mm/yyyy
        public DateTime DateOfBirth { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [BindNever]
        public string StatusMessage { get; set; }

    }
}
