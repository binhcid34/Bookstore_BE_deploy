using BookStoreCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.IService
{
    public interface IProductService
    {
        /// <summary>
        /// Kiêm tra trc khi xóa
        /// </summary>
        /// <param name="idCategory"></param>
        /// <returns></returns>
        Task<ResponseModel> BeforeDeleteCategory(int idCategory);
    }
}
