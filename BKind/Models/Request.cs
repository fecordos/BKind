using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.Models
{
    public class Request
    {
        public int ID { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImagePath { get; set; }


        //one-to-one relationship
        public int CategoryID { get; set; }
        public Category Category { get; set; }


        public int PersonID { get; set; } //FK
        public Person Person { get; set; } //nav property

        public string UserId { get; set; } //FK
        public AppUser AppUser { get; set; }

    }
}
