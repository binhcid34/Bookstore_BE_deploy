using BookStoreCore.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// Check xem có tồn tại người dùng có emai và password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public User validateUser(string email, string pass);

        /// <summary>
        /// Thêm người dùng 
        /// </summary>
        /// <param name="parameters"></param>
        public void InsertUser(DynamicParameters parameters);

        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="parameters"></param>
        public void UpdateUser(DynamicParameters parameters);

        /// <summary>
        ///  Lấy thông tin người dùng
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User getInfo(string email);

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        /// <param name="IdUser"></param>
        public void DeleteUser(string IdUser);

        /// <summary>
        /// Xóa tất cả
        /// </summary>
        public void DeleteAll();

        /// <summary>
        /// Tìm kiếm người dùng theo email
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<User> searchByEmail(string search, bool isAdmin);

        /// <summary>
        /// Thay đổi password 
        /// </summary>
        /// <param name="newPass"></param>
        /// <param name="email"></param>
        public void ChangePassword(string newPass, string email);

        /// <summary>
        /// Lấy thông tin người dùng bằng ssid
        /// </summary>
        /// <param name="ssid"></param>
        /// <returns></returns>
        public User getInfoFromSSID(string ssid);


        /// <summary>
        /// Lấy người dùng theo quyền admin hay không
        /// </summary>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        IEnumerable<User> GetAll(bool isAdmin);
    }
}
