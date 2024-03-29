﻿using BookStoreCore.Entity;
using BookStoreCore.EntityTest;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.IService
{
    public interface IEmployeeService:IBaseService<Employee>
    {
        dynamic filter(string filter, int pageNumber, int pageSize);
        public dynamic sortByList(ResponseModel response, string listSort);

        public dynamic insertNewEmployee(ResponseModel response, Employee newEmployee, int type);

        public dynamic importExcel(ResponseImportModel response, IFormFile fileImport);
    }
}
