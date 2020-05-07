using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.ViewModels
{
    public class RequestEditViewModel : RequestCreateViewModel
    {
        public int ID { get; set; }

        public string ExistingImagePath { get; set; }
    }
}
