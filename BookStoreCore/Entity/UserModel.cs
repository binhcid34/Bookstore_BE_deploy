﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.Entity
{
    public class User
    {
        public string? IdUser { get; set; }
        public string? Fullname { get; set; }    
        public string? Email { get; set; }
        public string? NewPassword { get; set; } = "12345678@Abc";

        public int? IsAdmin { get; set; }
        
        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }
        public string? Phone { get; set; }

        public string? ListOrderCode { get; set; }

    }
}
