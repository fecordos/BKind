using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BKind.Models;

namespace BKind.ViewModels
{
    public class ReqHistoryUserViewModel
    {
        [Key]
        public string UserId { get; set; }


        [Key]
        public int RequestId { get; set; }
        public ICollection<Request> Requests { get; set; }



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
    }
}
