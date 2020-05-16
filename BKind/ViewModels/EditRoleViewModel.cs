using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.ViewModels
{
    public class EditRoleViewModel
    {
        public string ID { get; set; }

        [Required(ErrorMessage = "Te rog introdu numele rolului")]
        [Display(Name = "Denumire rol")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
