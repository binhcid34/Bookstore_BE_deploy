using BookStoreCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.IRepository
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        public IEnumerable<Cart> AddToCart(string orderDeatil);
    }
}
