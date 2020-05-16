using BKind.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.ViewModels
{
    public class RequestCreateViewModel
    {
        [Required]
        [StringLength(maximumLength: 50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }


        public int CategoryID { get; set; }
        public Category Category { get; set; }


        public int PersonID { get; set; }
        public Person Person { get; set; }

        public IFormFile Image { get; set; }


    }
}
