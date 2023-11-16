using BookStoreCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.IService
{
    public interface IUserService
    {
        /// <summary>
        /// Thêm mới người dùng
        /// </summary>
        /// <param name="user"></param>
        /// <param name="response"></param>
        public void InsertUser(User user, ResponseModel response);

        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="user"></param>
        /// <param name="response"></param>
        public void UpdateUser(User user, ResponseModel response);

        /// <summary>
        /// Quên mật khẩu
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool RecoverPassword(string email);
    }
}
