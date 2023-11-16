using BookStoreAPI.Filter;
using BookStoreCore.Email;
using BookStoreCore.Entity;
using BookStoreCore.IRepository;
using BookStoreCore.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IConfiguration _configuration;
        IUserRepository _IUserRepository;
        IUserService _IUserService;
        IMailService _IMailService;

        public AccountController(IConfiguration config, IUserRepository IUserRepository, IUserService userService, IMailService mailService)
        {
            _configuration = config;
            _IUserRepository = IUserRepository;
            _IUserService = userService;
        }
        /// <summary>
        /// Đăng nhập và tạo ra token để authencation và authozation
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel Get(string email, string pass)
        {
            ResponseModel response = new ResponseModel();
            // Kiểm tra xem email và mật khẩu có trống
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(pass))
            {
                // Kiêm tra xem có tồn tại email và pass trong dữ liệu
                var user = GetUser(email, pass);
                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("SSID", user.IdUser.ToString()),
                    };
                    // Sinh ra token 
                  
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                        response.Data = new JwtSecurityTokenHandler().WriteToken(token);
                        response.Status = 200;
                        return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = 401 ;
                    response.Message = "Invalid user";
                    return response;
                }
            }
            else
            {
                response.Data = null;
                response.Status = 402;
                response.Message = "Email hoặc password trống";
                return response;
            };
        }
        
        /// <summary>
        /// Chỉnh sửa thông tin người dùng
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public ResponseModel UpdateUser(User user)
        {
            var response = new ResponseModel();
            try
            {
                _IUserService.UpdateUser(user, response);
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        ///  Lấy thông tin của account theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("{email}")]
        public ResponseModel GetInfoUser(string email)
        {
            var response = new ResponseModel();
            try
            {
                response.Data =  _IUserRepository.getInfo(email);
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        /// <summary>
        /// Check xem có người dùng nào có email và password này không trả về người dùng nếu có tồn tại
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private User GetUser(string email, string password)
        {
            return   _IUserRepository.validateUser(email, password);
        }

        /// <summary>
        /// Quên mật khẩu
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("recoverPassword/{email}")]
        public ResponseModel RecoverPassword(string email)
        {
            var response = new ResponseModel();
            try
            {
                // check xem có email có tồn tại không
                var userInfo = _IUserRepository.getInfo(email);
                if (userInfo == null)
                {
                    response.Success = false;
                    response.Message = $"{email} không tồn tại";
                    response.Status = 302;
                    return response;
                };
                // Sinh lại password mới và gửi email
                bool isValid = _IUserService.RecoverPassword(email);
                if (isValid)
                {
                    response.Success = true;
                    response.Message = $"Đã gửi password mới tới địa chỉ email: {email}";
                    response.Status = 200;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Có lỗi trong quá trình xử lý";
                    response.Status = 301;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Kiểm tra quyền của người dùng xem có được truy cập vào trang admin không?
        /// </summary>
        /// <returns></returns>
        [TypeFilter(typeof(UserAuthorizationFilterAttribute))]
        [HttpGet("checkPermission")]
        public ResponseModel checkPermission()
        {
            var response = new ResponseModel();

            try
            {
                // lấy SSID (key để xác thực) 
                var SSID = HttpContext.Session.GetString("SSID").ToString();
                if (SSID != null)
                {
                    // Lấy người dùng từ ssid
                    User newUser = _IUserRepository.getInfoFromSSID(SSID);

                    if (newUser != null)
                    {
                        response.Data = newUser;
                    }
                     
                    if (newUser != null && newUser.IsAdmin == 1) {

                        response.Message = "Bạn là admin";
                        response.Success = true;
                        response.Status = 200;
                    }
                    else
                    {
                        response.Message = "Tài khoản không có quyền truy cập vào trang admin";
                        response.Success = false;
                        response.Status = 301;
                    }
                }
                else
                {
                    response.Message = "Tài khoản không có quyền truy cập vào trang admin";
                    response.Success = false;
                    response.Status = 302;
                }
            }
            catch ( Exception ex ) {

                response.Message = ex.Message;
                response.Success = false;
                response.Status = 400;
            }
            return response;
        }

        /// <summary>
        /// Đăng ký người dùng
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public ResponseModel register([FromBody] User user)
        {
            var response = new ResponseModel();
            try
            {
                _IUserService.InsertUser(user, response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}