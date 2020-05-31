using BKind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.ViewModels
{
    public class RequestDetailsViewModel
    {
        public virtual Person Person { get; set; }

        public virtual Address Address { get; set; }

        public virtual Category Category { get; set; }

        public virtual Request Request { get; set; }

    }
}
