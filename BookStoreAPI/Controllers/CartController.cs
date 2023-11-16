using BookStoreCore.Entity;
using BookStoreCore.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using BookStoreAPI.Filter;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(UserAuthorizationFilterAttribute))]

    public class CartController : ControllerBase
    {
        // property
        private readonly IOrderRepository _iOrderRepository;

        //constructor
        public CartController(IOrderRepository iOrderRepository)
        {
            _iOrderRepository = iOrderRepository;
        }
        
        /// <summary>
        /// Lấy giỏ hàng theo người dùng
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetItems()
        {
            var res = new ResponseModel();
            var SSID = HttpContext.Session.GetString("SSID").ToString();
            res.Data = _iOrderRepository.GetItems(SSID);
            res.Success = true;
            return Ok(res);

        }

        /// <summary>
        /// Thêm vào giỏ hàng
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddItem([FromBody] List<Product> orderItems)
        {
            var res = new ResponseModel();
            var SSID = HttpContext.Session.GetString("SSID").ToString();
            res.Data = _iOrderRepository.AddItems(orderItems, SSID);
            res.Success = true;
            return Ok(res);
        }

        /// <summary>
        /// Thanh toán
        /// </summary>
        /// <param name="sessionOrder"></param>
        /// <returns></returns>
        [HttpPost("checkout")]
        public IActionResult Checkout([FromBody] SessionOrder sessionOrder)
        {
            var res = new ResponseModel();
            var SSID = HttpContext.Session.GetString("SSID").ToString();
            res.Data = _iOrderRepository.Checkout(sessionOrder, SSID);
            if(res.Data)
            {
                res.Success = true;
            }
            return Ok(res);
        }


        /// <summary>
        /// Lấy lịch sử mua hàng theo người dùng
        /// </summary>
        /// <returns></returns>
        [HttpGet("history")]
        public IActionResult GetHistory()
        {
            var res = new ResponseModel();
            var SSID = HttpContext.Session.GetString("SSID").ToString();
            res.Data = _iOrderRepository.GetAllOrdersByUserId(SSID);
            res.Success = true;
            return Ok(res);
        }

        /// <summary>
        /// Áp mã giảm giá vào giỏ hàng
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost("applypromotion")]
        public IActionResult ApplyPromotion(string code)
        {
            var res = new ResponseModel();
            var SSID = HttpContext.Session.GetString("SSID").ToString();
            res.Data = _iOrderRepository.ApplyPromotion(SSID, code);
            res.Success = true;
            return Ok(res);
        }
        /// <summary>
        /// cập nhật đơn hàng với TH xác nhân thanh toán và hủy đơn hàng
        /// </summary>
        /// <param name="idOrder"></param>
        /// <param name="type">1 là mua; 2 là hủy đơn hàng</param>
        /// <returns></returns>
        [HttpPut("updateQuantity/{idOrder}")]
        public IActionResult updateQuantity(string idOrder,  int type)
        {
            var res = new ResponseModel();

            // 1. Lấy orderData từ idOrder;
            SessionOrder sessionOrder = _iOrderRepository.GetOrderByID(idOrder);
            if (sessionOrder == null && sessionOrder.OrderDetail == null)
            {
                res.Success = false;
                return Ok(res);
            }
            // 2. for từng orderData để cập nhật đơn hàng
            var orderData = JsonSerializer.Deserialize <List<Product>>(sessionOrder.OrderDetail);
            foreach (var item in orderData)
            {
                if (item != null)
                {
                    // 3. Tính lại quantity
                    var quantityProduct = item.Quantity;
                    if (quantityProduct!= null)
                    {
                        _iOrderRepository.updateQuantity(item.IdProduct, quantityProduct, type);
                    }
                }
            }

            res.Success = true;
            return Ok(res);
        }
    }
}
