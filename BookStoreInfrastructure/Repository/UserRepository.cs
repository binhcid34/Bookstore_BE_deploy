﻿using BookStoreCore.Entity;
using BookStoreCore.EntityTest;
using BookStoreCore.IRepository;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreInfrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository() : base() { }
        public User validateUser(string email, string pass)
        {
            var sqlConnector = new MySqlConnection(base.connectString);
            var sqlQuery = $"Select * from User where Email = '{email}' and Password = '{pass}' ";
            var res = sqlConnector.Query<User>(sqlQuery).FirstOrDefault();
            return res;
        }

        public void InsertUser(DynamicParameters parameters)
        {
            var sqlConnector = new MySqlConnection(base.connectString);
            var sqlQuery = "Proc_Insert_User_v2";
            sqlConnector.Query<User>(sqlQuery, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public User getInfo(string email)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var sqlQuery = $"Select * from User where Email = '{email}'";
            var res = sqlConnector.Query<User>(sqlQuery).FirstOrDefault();
            return res;
        }

        //public void UpdateUser(User user)
        //{
        //    var sqlConnector = new MySqlConnection(base.connectString);

        //    var parameters = new DynamicParameters();
        //    parameters.Add("v_Name", user.Name);
        //    parameters.Add("v_Email", user.Email);
        //    parameters.Add("v_Password", user.Password);
        //    parameters.Add("v_ModifiedBy", user.ModifiedBy);

        //    var queryProc = "Proc_Upadate_User";
        //    var res = sqlConnector.Query<User>(queryProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
        //}

        public void UpdateUser(DynamicParameters parameters)
        {
            var sqlConnector = new MySqlConnection(base.connectString);
            var sqlQuery = "Proc_Update_User_v2";
            sqlConnector.Query<Employee>(sqlQuery, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteUser(string IdUser)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            // Xóa người dùng ở bảng user và account
            var sqlQuery = $"DELETE FROM User WHERE IdUser = '{IdUser}';" +
                $"DELETE FROM Account WHERE IdUser = '{IdUser}'";
            var res = sqlConnector.Query<User>(sqlQuery).FirstOrDefault();
        }

        public void DeleteAll()
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var sqlQuery = $"DELETE FROM User LIMIT 10;";
            var res = sqlConnector.Query<User>(sqlQuery).FirstOrDefault();
        }

        public IEnumerable<User> searchByEmail(string search, bool isAdmin)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var sqlQuery = $"Select * from view_user where Email like '%{search}%' and isAdmin = {isAdmin}";
            var res = sqlConnector.Query<User>(sqlQuery).ToList();
            return res;
        }

        public void ChangePassword(string newPass, string email)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var sqlQuery = $"UPDATE User u SET Password = '{newPass}' WHERE u.Email = '{email}';";
            var res = sqlConnector.Query<User>(sqlQuery);
        }

        public User getInfoFromSSID(string ssid)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var sqlQuery = $"Select * from User Where IdUser = '{ssid}'";
            var res = sqlConnector.Query<User>(sqlQuery).FirstOrDefault();
            return res;
        }

       public  IEnumerable<User> GetAll(bool isAdmin)
        {
            var sqlConnector = new MySqlConnection(connectString);
            var sqlQuery = $"Select * from view_user where isAdmin = {isAdmin} ";
            var res = sqlConnector.Query<User>(sqlQuery);
            return res;
        }
    }
}