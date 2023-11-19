using BookStoreCore.Email;
using BookStoreCore.Entity;
using BookStoreCore.IRepository;
using BookStoreCore.IService;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreInfrastructure.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        private IUserRepository _IUserRepository;
        private IMailService _IMailService;
        public UserService(IBaseRepository<User> baseRepository, IUserRepository iUserRepository, IMailService mailService) : base(baseRepository)
        {
            _IUserRepository = iUserRepository;
            _IMailService = mailService;
        }
        /// <summary>
        /// Thêm mới người dùng
        /// </summary>
        /// <param name="user"></param>
        /// <param name="response"></param>
        public void InsertUser(User user, ResponseModel response)
        {
            // validate
            if (!String.IsNullOrEmpty(user.Email))
            {
                var _user = _IUserRepository.getInfo(user.Email);
                if (_user == null)
                {
                    // tạo ra param để thêm mới
                    var parameters = new DynamicParameters();
                    parameters.Add("v_FullName", user.Fullname);
                    parameters.Add("v_Email", user.Email);
                    parameters.Add("v_Password", user.NewPassword);
                    parameters.Add("v_IsAdmin", user.IsAdmin);
                    parameters.Add("v_CreatedBy", user.CreatedBy);
                    parameters.Add("v_Address1", user.Address1);
                    parameters.Add("v_Address2", user.Address2);
                    parameters.Add("v_Phone", user.Phone);

                    _IUserRepository.InsertUser(parameters);

                    response.Success = true;
                    response.Message = "Đã thêm thành công với mật khẩu mặc định là 12345678@Abc";
                    response.Status = 200;
                    return;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Đã tồn tại email";
                    response.Status = 301;
                    return;
                }

            };
            response.Success = false;
            response.Message = "Email không thể trống";
            response.Status = 302;
            return;

        }

        /// <summary>
        /// Quên mật khẩu trả về true nếu thành công và false nếu thất bại
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool RecoverPassword(string email)
        {
            // check xem có email đó không
            var userInfo = _IUserRepository.getInfo(email);
            if (userInfo == null)
            {
                return false;
            };
            bool isValid = true;
            // genarate password mới
            var newPass = GetRandomPassword(8);
            string content = $"Cấp lại mật khẩu";
            string body = $"Password mới của bạn là: {newPass}";
            //gửi email
            isValid =  _IMailService.SendMailAsync(email, content, body);
            // update password mới trong db
            _IUserRepository.ChangePassword(newPass, email);
            return isValid;
        }

        /// <summary>
        /// Cập nhật dữ liệu người dùng
        /// </summary>
        /// <param name="user"></param>
        /// <param name="response"></param>
        public void UpdateUser(User user, ResponseModel response)
        {
            // validate email
            if (!String.IsNullOrEmpty(user.Email))
            {
                var _user = _IUserRepository.getInfo(user.Email);
                if (_user != null)
                {
                    // tạo ra param để cập nhật dữ liệu
                    var parameters = new DynamicParameters();
                    parameters.Add("v_IdUser", user.IdUser);
                    parameters.Add("v_FullName", user.Fullname);
                    parameters.Add("v_Email", user.Email);
                    parameters.Add("v_IsAdmin", user.IsAdmin);
                    parameters.Add("v_Address1", user.Address1);
                    parameters.Add("v_Address2", user.Address2);
                    parameters.Add("v_Phone", user.Phone);
                    _IUserRepository.UpdateUser(parameters);

                    response.Success = true;
                    response.Message = "Đã chỉnh sửa thành công";
                    response.Status = 200;
                    return;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Tài khoản không đúng";
                    response.Status = 301;
                    return;
                }

            };
            response.Success = false;
            response.Message = "Email không thể trống";
            response.Status = 302;
            return;

        }

        /// <summary>
        /// sinh ra mật khẩu mới khi quên mật khẩu
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string GetRandomPassword(int length)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
    }
}
