using BookStoreCore.Entity;
using BookStoreCore.IRepository;
using BookStoreCore.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;
using BookStoreAPI.Filter;
using BookStoreInfrastructure.Repository;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AdminAuthorizationFilterAttribute))]
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// property
        /// </summary>
        IUserRepository _IUserRepository;
        IUserService _IUserService;
        IOrderRepository _IOrderRepository;
        IProductRepository _IProductRepository;

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="IUserRepository"></param>
        /// <param name="userService"></param>
        /// <param name="iOrderRepository"></param>
        /// <param name="iProductRepository"></param>
        public AdminController(IUserRepository IUserRepository, IUserService userService, IOrderRepository iOrderRepository, IProductRepository iProductRepository)
        {
            _IUserRepository = IUserRepository;
            _IUserService = userService;
            _IOrderRepository = iOrderRepository;
            _IProductRepository = iProductRepository;
        }

        /// <summary>
        ///  Lấy toàn bộ thông tin account
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel getAllAccount(bool isAdmin = true)
        {
            var response = new ResponseModel();

            try
            {
                response.Data = _IUserRepository.GetAll(isAdmin);
            }
            catch(Exception ex) {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Thêm tài khoản
        /// </summary>
        /// <returns></returns>
        [HttpPost("insert")]
        public ResponseModel insertAccount(User newUser) {
            var response = new ResponseModel();
            try
            {
                _IUserService.InsertUser(newUser, response);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <returns></returns>
        [HttpPost("search/{search}")]
        public ResponseModel searchByEmail(string search, bool isAdmin = true )
        {
            var response = new ResponseModel();
            try
            {
                response.Data  = _IUserRepository.searchByEmail(search, isAdmin);
                response.Success = true;
                response.Status =   200;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        /// <summary>
        /// Xóa nhân viên theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpDelete("{IdUser}")]
        public ResponseModel deleteUser(string IdUser)
        {
            var response = new ResponseModel();
            try
            {
                var SSID = HttpContext.Session.GetString("SSID").ToString();
                if (SSID == IdUser)
                {
                    response.Success = false;
                    response.Message = "Không thể xóa chính mình";
                    return response;
                }
                _IUserRepository.DeleteUser(IdUser);
                response.Message = "Đã xóa thành công";

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Xóa toàn bộ nhưng đang fix cứng là xóa 10 bản ghi , Không nên test
        /// </summary>
        /// <returns></returns>
        [HttpDelete("deleteAll")]
        public ResponseModel DeleteAll ()
        {
            var response = new ResponseModel();
            try
            {
                _IUserRepository.DeleteAll();

                response.Message = "Đã xóa thành công";

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }


        [HttpGet("getAllOrder")]
        public ResponseModel getALlOrders()
        {
            var response = new ResponseModel();
            try
            {
                response.Data = _IOrderRepository.GetAllOrder();

                response.Status = 200;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Cập nhật đơn hàng
        /// </summary>
        /// <param name="idorder"></param>
        /// <param name="paymentSatus"></param>
        /// <returns></returns>
        [HttpPost("updateOrder")]
        public ResponseModel updateOrder(string idorder, int paymentSatus)
        {
            var response = new ResponseModel();
            try
            {
                 _IOrderRepository.updateSessionOrder(paymentSatus, idorder);

                response.Status = 200;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        
        /// <summary>
        /// tìm kiếm đơn hàng
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("filterOrder")]
        public ResponseModel filterOrder(string filter)
        {
            var response = new ResponseModel();
            try
            {
                response.Data = _IOrderRepository.SearchByNameAndCode(filter);

                response.Status = 200;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Lấy thông tin trang dashboard
        /// </summary>
        /// <returns></returns>
        [HttpGet("dashboard")]
        public ResponseDashboardModel dashboard()
        {
            var response = new ResponseDashboardModel();
            try
            {
                response.orderDashboard = _IOrderRepository.dashboardOrder();
                response.orderChart = _IOrderRepository.chartOrder();
                response.userDashboard = _IOrderRepository.chartUser();
                response.productDashboard = _IOrderRepository.chartProduct();
                response.Status = 200;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = 400;
                response.Success = false;
                return response;
            }
        }

        /// <summary>
        /// Lấy tất cả mgg
        /// </summary>
        /// <returns></returns>
        [HttpGet("getALlPromotion")]

        public ResponseModel getALlPromotion()
        {
            var response = new ResponseModel();
            try
            {
                response.Data = _IOrderRepository.GetAllPromotion();

                response.Status = 200;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Sinh thêm mgg mới 
        /// </summary>
        /// <param name="promotionPercent"></param>
        /// <returns></returns>
        [HttpPost("createNewPromotion/{promotionPercent}")]
        public ResponseModel createNewPromotion(int promotionPercent)
        {
            var response = new ResponseModel();
            try
            {
                var promotionName = GetRandomPassword(8);
                  _IOrderRepository.createNewPromotion(promotionName, promotionPercent);

                response.Status = 200;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Xóa mgg theo id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete("deletePrmotion/{ID}")]
        public ResponseModel deletePrmotion(string ID)
        {
            var response = new ResponseModel();
            try
            {
                _IOrderRepository.deletePrmotion(ID);

                response.Status = 200;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }


        /// <summary>
        /// Xóa sản phẩm theo id
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [HttpDelete("deleteProduct/{productID}")]
        public ResponseModel delete(string productID)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                _IProductRepository.DeleteProduct(productID);
                response.Status = 200;
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = 400;
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Sinh ra password mới
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

        /// <summary>
        /// Tạo ra thêm danh mục
        /// </summary>
        /// <param name="nameCategory"></param>
        /// <returns></returns>
        [HttpPost("createNewCategory/{nameCategory}")]
        public ResponseModel createNewCategory(string nameCategory)
        {
            var response = new ResponseModel();
            try
            {
                var UserName = HttpContext.Session.GetString("UserName").ToString();

                _IProductRepository.createNewCategory(nameCategory, UserName);

                response.Status = 200;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Xóa danh mục theo id
        /// </summary>
        /// <param name="idCategory"></param>
        /// <returns></returns>
        [HttpDelete("deleteCategory/{idCategory}")]
        public  ResponseModel createNewCategory(int idCategory)
        {
            var response = new ResponseModel();
            try
            {

                 _IProductRepository.deleteCategory(idCategory);

                response.Status = 200;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
