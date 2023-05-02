using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreCore.Entity;
using BookStoreCore.IRepository;
using BookStoreCore.IService;

namespace BookStoreInfrastructure.Service
{
    public class OrderService : BaseService<SessionOrder>, IOrderService
    {
        public OrderService(IBaseRepository<SessionOrder> baseRepository) : base(baseRepository)
        {
        }

        public bool CheckOrderExist(string UserID)
        {
            throw new NotImplementedException();
        }
    }
}
