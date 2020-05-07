using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.Models.Manage
{
    public class DeletePersonalData
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
