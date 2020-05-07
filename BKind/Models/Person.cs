using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.Models
{
    public class Person
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Prenume")]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nume")]
        [StringLength(40)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Data nașterii")]
        [DataType(DataType.Date)] //format de tipul dd/mm/yyyy
        public DateTime DateOfBirth { get; set; }

        //one-to-one relationship
        public int AddressID { get; set; } //FK

        [Display(Name = "Adresa")]
        public Address Address { get; set; } //nav property

        //one-to-many relationship
        public virtual ICollection<Request> Requests { get; set; } //nav property
    }
}
