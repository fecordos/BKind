using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.ViewModels
{
    public class AddRoleViewModel
    {
        [Required]
        [Display(Name = "Denumire rol")]
        public string RoleName { get; set; }
    }
}
