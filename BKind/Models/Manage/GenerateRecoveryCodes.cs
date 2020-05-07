using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.Models.Manage
{
    public class GenerateRecoveryCodes
    {
        [BindNever]
        public string StatusMessage { get; set; }
    }
}
