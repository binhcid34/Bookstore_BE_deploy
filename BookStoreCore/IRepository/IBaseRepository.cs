using BookStoreCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.IRepository
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll();
        /// <summary>
        /// Lấy chi tiết bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<T> GetById(string id);

        /// <summary>
        /// Lấy theo danh mục
        /// </summary>
        /// <param name="idCategory"></param>
        /// <returns></returns>
        public IEnumerable<T> GetByCategoryId(int idCategory);

        /// <summary>
        /// Thêm
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(T entity);
        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity);
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity);
        /// <summary>
        /// Số lượng
        /// </summary>
        /// <returns></returns>
        public dynamic Count();
        /// <summary>
        /// phân trang
        /// </summary>
        /// <param name="IdCategory"></param>
        /// <param name="NameProduct"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        object FilterProduct(int IdCategory,string NameProduct, int pageIndex, int pageSize);
        /// <summary>
        ///  tìm kiếm
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        IEnumerable<T> SearchProduct(string search);
    }
}
