using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.Models
{
    public class AppUser : IdentityUser
    {

        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Birthday")]
        [DataType(DataType.Date)] //format de tipul dd/mm/yyyy
        public DateTime DateOfBirth { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        //1-M relationship
        public virtual ICollection<Request> Requests { get; set; }



    }
}
