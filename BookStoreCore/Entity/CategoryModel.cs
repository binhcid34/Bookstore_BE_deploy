using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.Entity
{
    public class Category
    {
        public int? IdCategory { get; set; }

        public string NameCategory { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Tổng số sách
        /// </summary>
        public int? TotalProduct { get; set; }


    }
}
