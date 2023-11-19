using BookStoreCore.EntityTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.IRepository
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        /// <summary>
        /// Thêm vào giỏ hàng
        /// </summary>
        /// <param name="orderDeatil"></param>
        /// <returns></returns>
        public IEnumerable<Cart> AddToCart(string orderDeatil);
    }
}
