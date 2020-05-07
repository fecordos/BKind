using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.Models
{
    public class Address
    {
        public int ID { get; set; }

        [Required]
        [Display(Name ="Strada")]
        public string Street { get; set; }

        [Required]
        [Display(Name ="Număr")]
        public int Number { get; set; }

        [Required]
        [Display(Name ="Localitate/Oraș")]
        public string City { get; set; }

        //one-to-many relationship
        public virtual ICollection<Person> Persons { get; set; }
    }
}
