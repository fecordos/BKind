using System;
using System.ComponentModel.DataAnnotations;

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

        //adaug data la care a fost postata cererea, pentru afisarea cronologica in istoric
        public DateTime DateAdded { get; set; }

        public Request()
        {
            DateAdded = DateTime.Now;
        }

    }
}
