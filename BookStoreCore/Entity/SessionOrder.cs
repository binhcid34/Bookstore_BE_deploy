using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BookStoreCore.Entity
{
    /// <summary>
    ///  Đơn hàng theo người dùng   
    /// </summary>
    public class SessionOrder
    {
        public string? IdOrder { get; set; }
        //public List<Product>? OrderDetailList { get; set; }
        public string? IdUser { get; set; }
        public int Status { get; set; } //  0: chưa thanh toán, 1: Đang vận chuyển, 2: Thành công ,3: Bị lỗi
        public int DiscountCombo { get; set; }
        /// <summary>
        /// Tổng tiền
        /// </summary>
        public float TotalPayment { get; set; }
        /// <summary>
        /// Trạng thái đơn hàng
        /// </summary>
        public int? PaymentStatus { get; set; } // 0 hoặc null: mới, 1 = chưa thanh toán, 2 : Đã thanh toán, 3 : Chờ chuyển khoản, 4: Hủy
        /// <summary>
        /// Phí đơn hàng
        /// </summary>
        public int PaymentFee { get; set; } // phí vẩn chuyển
        /// <summary>
        /// Cách thức thanh toán
        /// </summary>
        public int PaymentType { get; set; } // cách thanh toán:// 1: chuyển tiền mặt , 2 : chuyển khoản
        /// <summary>
        /// tên người nhận đơn hàng
        /// </summary>
        public string? FullName { get; set; }
        /// <summary>
        /// sđt
        /// </summary>
        public string? PhoneNumber { get; set; }    
        /// <summary>
        /// Địa chỉ nận hàng
        /// </summary>
        public string? Address { get; set; }   
        /// <summary>
        /// email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Chi tiết đơn hàng
        /// </summary>
        public dynamic? OrderDetail { get; set; }
        /// <summary>
        /// mã code đơn hàng
        /// </summary>
        public string? OrderCode { get; set; }
        /// <summary>
        /// thời gian chỉnh sửa cuối cung
        /// /// </summary>
        public DateTime LastTime { get; set; }
        /// <summary>
        /// Số lượng giảm giá
        /// </summary>
        public int? PromotionPercent { get; set; }
        /// <summary>
        /// Ảnh sản phẩm
        /// </summary>
        public List<byte[]>? ListImage { get; set; }
        /// <summary>
        /// Id mgg
        /// </summary>
        public string? IdPromotion { get; set; }
        /// <summary>
        /// mgg
        /// </summary>
        public string? PromotionName { get; set; }

    }
}
