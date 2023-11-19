using BookStoreCore.Entity;
using BookStoreCore.IRepository;
using BookStoreCore.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreInfrastructure.Service
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IBaseRepository<Product> baseRepository) : base(baseRepository)
        {
        }

        /// <summary>
        /// Kiểm tra xem danh mục tồn tại trong sản phẩm không
        /// </summary>
        /// <param name="idCategory"></param>
        /// <returns></returns>
        public async Task<ResponseModel> BeforeDeleteCategory(int idCategory)
        {
            var res  = new ResponseModel();
            var products =  _IRepository.GetByCategoryId(idCategory);

            if (products != null) {
                res.Message = "Đã tồn tại sản phẩm của danh mục này";
                res.Success = false;
                return res; 
            }
            return res;
        }
    }
}
