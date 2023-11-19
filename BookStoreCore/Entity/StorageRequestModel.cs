using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.Entity
{
    public class StorageRequestModel
    {
        public int status { get; set; }
        public string search { get; set; } = string.Empty;
    }
}
