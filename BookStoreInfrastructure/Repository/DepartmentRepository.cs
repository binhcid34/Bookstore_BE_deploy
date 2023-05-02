using BookStoreCore.Entity;
using BookStoreCore.IRepository;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreInfrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository() : base() { }
        public List<Department> getAllDepartment()
        {
            var sqlConnector = new MySqlConnection(base.connectString);
            var sqlQuery = "Select * from Department";
            var res = sqlConnector.Query<Department>(sqlQuery).ToList();
            return res;
        }
    }
}
