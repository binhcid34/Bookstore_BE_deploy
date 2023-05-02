using BookStoreCore.Entity;
using BookStoreCore.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreInfrastructure.Service
{
    public class ProductService : BaseService<Product>
    {
        public ProductService(IBaseRepository<Product> baseRepository) : base(baseRepository)
        {
        }
    }
}
