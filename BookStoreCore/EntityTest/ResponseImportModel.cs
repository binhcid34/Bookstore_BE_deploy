﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreCore.Entity;

namespace BookStoreCore.EntityTest
{
    public class ResponseImportModel : ResponseModel
    {
        public List<object> listEmployeesUpdate { get; set; }
        public List<object> listEmployeesInsert { get; set; }
        public List<object> listEmployeesError { get; set; }

        public int TotalRow { get; set; }
    }
}
