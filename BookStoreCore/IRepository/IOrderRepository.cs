using BookStoreCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.IRepository
{
    public interface IOrderRepository :IBaseRepository<SessionOrder>
    {
        /// <summary>
        /// thêm vào giỏ hàng
        /// </summary>
        /// <param name="orderItems"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public SessionOrder AddItems(List<Product> orderItems, string userID);
        /// <summary>
        /// Lấy giỏ hàng
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public SessionOrder GetItems(string userID);
        /// <summary>
        /// thanh toán
        /// </summary>
        /// <param name="sessionOrder"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Boolean Checkout(SessionOrder sessionOrder, string userID);
        /// <summary>
        /// Lấy tát 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SessionOrder> GetAllOrder();
        /// <summary>
        /// Tìm kiếm theo tên hoặc mã đơn hàng
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IEnumerable<SessionOrder> SearchByNameAndCode(string searchString);
        /// <summary>
        /// Cập nhật trạng thái đơn hàng
        /// </summary>
        /// <param name="paymentStatus"></param>
        /// <param name="OrderId"></param>
        public void updateSessionOrder(int paymentStatus, string OrderId);
        /// <summary>
        /// Lấy tấc cả đơn hàng theo người dùng
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<SessionOrder> GetAllOrdersByUserId(string userID);
        /// <summary>
        /// áp MGG
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public SessionOrder ApplyPromotion(string userId, string code);
        /// <summary>
        /// Dữ liệu báo cáo đơn hàng
        /// </summary>
        /// <returns></returns>
        public dynamic dashboardOrder();
        /// <summary>
        /// dữ liệu đồ thị đơn hàng
        /// </summary>
        /// <returns></returns>
        public dynamic chartOrder();
        /// <summary>
        /// dữ liệu đồ thị người dùng
        /// </summary>
        /// <returns></returns>
        public dynamic chartUser();
        /// <summary>
        /// dữ liệu đồ thị sản phẩm
        /// </summary>
        /// <returns></returns>
        public dynamic chartProduct();
        /// <summary>
        /// Cập nhật số lượng
        /// </summary>
        /// <param name="idProduct"></param>
        /// <param name="quantity"></param>
        /// <param name="type"></param>
        public void updateQuantity(string idProduct, int quantity, int type);
        /// <summary>
        /// Lấy đơn hàng theo id order
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        public SessionOrder GetOrderByID(string idOrder);
        /// <summary>
        /// Lấy tất cả mgg
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Promotion> GetAllPromotion();
        /// <summary>
        /// sinh mgg
        /// </summary>
        /// <param name="promotionName"></param>
        /// <param name="promotionPercent"></param>
        public void createNewPromotion(string promotionName, int promotionPercent);
        /// <summary>
        /// xóa mgg
        /// </summary>
        /// <param name="ID"></param>
        public void deletePrmotion(string ID);
    }
}
