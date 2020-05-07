using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        //1-M relationship
        public virtual ICollection<Request> Requests { get; set; }

    }
}
