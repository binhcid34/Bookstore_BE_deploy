﻿using BookStoreCore.Entity;
using BookStoreCore.IRepository;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreInfrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository() : base() { }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="product"></param>
        public void InsertProduct(Product product)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var queryProc = $"Proc_Insert_Product";

            var parameters = new DynamicParameters();
            //parameters.Add("v_IdProduct", product.IdProduct); tự sinh GUID36 trong db
            parameters.Add("v_IdCategory", product.IdCategory);
            parameters.Add("v_NameProduct", product.NameProduct);
            parameters.Add("v_TitleProduct", product.TitleProduct);
            parameters.Add("v_DescriptionProduct", product.DescriptionProduct);
            parameters.Add("v_QuantitySock", product.QuantitySock);
            parameters.Add("v_QuantitySold", product.QuantitySold);
            parameters.Add("v_PriceProduct", product.PriceProduct);
            parameters.Add("v_ImageProduct", product.ImageProduct);
            parameters.Add("v_Author", product.Author);
            parameters.Add("v_PageNumber", product.PageNumber);
            parameters.Add("v_PublishingCompany", product.PublishingCompany);
            parameters.Add("v_DiscountSale", product.DiscountSale);

            // query
            sqlConnector.Query(queryProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

        }

        /// <summary>
        /// cập nhật
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(Product product)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var queryProc = $"Proc_Update_Product";

            var parameters = new DynamicParameters();
            parameters.Add("v_IdProduct", product.IdProduct);
            parameters.Add("v_IdCategory", product.IdCategory);
            parameters.Add("v_NameProduct", product.NameProduct);
            parameters.Add("v_TitleProduct", product.TitleProduct);
            parameters.Add("v_DescriptionProduct", product.DescriptionProduct);
            parameters.Add("v_QuantitySock", product.QuantitySock);
            parameters.Add("v_QuantitySold", product.QuantitySold);
            parameters.Add("v_PriceProduct", product.PriceProduct);
            parameters.Add("v_ImageProduct", product.ImageProduct);
            parameters.Add("v_Author", product.Author);
            parameters.Add("v_PageNumber", product.PageNumber);
            parameters.Add("v_PublishingCompany", product.PublishingCompany);
            parameters.Add("v_DiscountSale", product.DiscountSale);

            sqlConnector.Query(queryProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

        }


        /// <summary>
        /// tìm kiếm
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<Product> SearchProduct(string search)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var likeQuery = '%' + search + '%';
            var querySQL = $"Select * from product where product.NameProduct like '{likeQuery}' or product.TitleProduct like '{likeQuery}' or product.DescriptionProduct like '{likeQuery}'";

            var res = sqlConnector.Query<Product>(querySQL);
            return res;
        }
        /// <summary>
        /// phân trang theo danh mục
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="IdCategory"></param>
        /// <returns></returns>
        public object filterByCategory(string filter, int pageNumber, int pageSize, int IdCategory)
        {
            var sqlConnector = new MySqlConnection(base.connectString);
            var parameters = new DynamicParameters();
            parameters.Add("v_filter", filter);
            parameters.Add("v_pageIndex", pageNumber);
            parameters.Add("v_pageSize", pageSize);
            parameters.Add("v_IdCategory", IdCategory);
            parameters.Add("v_TotalRecord", direction: System.Data.ParameterDirection.Output);
            parameters.Add("v_RecordStart", direction: System.Data.ParameterDirection.Output);
            parameters.Add("v_RecordEnd", direction: System.Data.ParameterDirection.Output);

            var queryProc = "Proc_Filter_ProductByCategory";
            var res = sqlConnector.Query<Product>(queryProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

            int totalRecord = parameters.Get<int>("@v_TotalRecord");
            int recordStart = parameters.Get<int>("@v_RecordStart");
            int recordEnd = parameters.Get<int>("@v_RecordEnd");

            return new
            {
                TotalRecord = totalRecord,
                RecordStart = recordStart,
                RecordEnd = recordEnd,
                Data = res
            };
        }

        /// <summary>
        /// phân trang
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public object filter(string filter, int pageNumber, int pageSize)
        {
            var sqlConnector = new MySqlConnection(base.connectString);
            var parameters = new DynamicParameters();
            parameters.Add("v_filter", filter);
            parameters.Add("v_pageIndex", pageNumber);
            parameters.Add("v_pageSize", pageSize);
            parameters.Add("v_TotalRecord", direction: System.Data.ParameterDirection.Output);
            parameters.Add("v_RecordStart", direction: System.Data.ParameterDirection.Output);
            parameters.Add("v_RecordEnd", direction: System.Data.ParameterDirection.Output);

            var queryProc = "Proc_Filter_ProductAll";
            var res = sqlConnector.Query<Product>(queryProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);


            int totalRecord = parameters.Get<int>("@v_TotalRecord");
            int recordStart = parameters.Get<int>("@v_RecordStart");
            int recordEnd = parameters.Get<int>("@v_RecordEnd");

            var queryDiscount = "SELECT * FROM product p WHERE p.DiscountSale > 0 and p.QuantitySock > 0";
            var resDiscount = sqlConnector.Query<Product>(queryDiscount);

            return new
            {
                TotalRecord = totalRecord,
                RecordStart = recordStart,
                RecordEnd = recordEnd,
                Data = res,
                ListDiscountProduct = resDiscount
            };
        }
        /// <summary>
        /// Lấy sản phẩm theo danh mục
        /// </summary>
        /// <param name="IdCategory"></param>
        /// <returns></returns>
        public IEnumerable<Product> getByIdCategory(int IdCategory)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var querySQL = $"Select * from product where product.IdCategory = {IdCategory} ";

            var res = sqlConnector.Query<Product>(querySQL).ToList();
            return res;
        }
        /// <summary>
        /// xóa sp
        /// </summary>
        /// <param name="IdProduct"></param>
        public void DeleteProduct(string IdProduct)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var querySQL = $"DELETE FROM product where product.IdProduct = '{IdProduct}' Limit 1 ";

            var res = sqlConnector.Query<Product>(querySQL);
        }

        /// <summary>
        /// lấy tổng danh mục
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> getAllCategory()
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var querySQL = $"Select * from view_category";

            var res = sqlConnector.Query<Category>(querySQL).ToList();
            return res;
        }
        /// <summary>
        /// tạo mới danh mục
        /// </summary>
        /// <param name="nameCategory"></param>
        /// <param name="nameUser"></param>
        public void createNewCategory(string nameCategory, string nameUser)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var querySQL = $"INSERT catagory ( NameCategory, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, CategoryCode)  VALUES ( '{nameCategory}', CURDATE(), '{nameUser}', CURDATE(), '{nameUser}', '');";

            var res = sqlConnector.Query(querySQL);
        }
        /// <summary>
        /// xóa danh mục
        /// </summary>
        /// <param name="idCategory"></param>
        public string deleteCategory(int idCategory)
        {
            var message = String.Empty;
            var sqlConnector = new MySqlConnection(base.connectString);

            // Kiểm tra xem có sản phẩm nào có danh mục nào không
            var queryExsit = $"Select * from product where idCategory = {idCategory}";

            var resDataExsit = sqlConnector.Query<Product>(queryExsit).ToList();

            if (resDataExsit != null && resDataExsit.Count > 0)
            {
                message = "Đã tồn tại sản phẩm là danh mục này";
                return message;
            }

            var querySQL = $"Delete from catagory where idCategory = {idCategory} Limit 1 ;";

            var res = sqlConnector.Query(querySQL);


            sqlConnector.Close();
            return message;
        }

        public IEnumerable<Product> getProductStorage(string search, int status)
        {
            var sqlConnector = new MySqlConnection(base.connectString);
            var querySQL = "Select TitleProduct, IdCategory, NameProduct, Author, QuantitySock, QuantitySold, NameCategory FROM view_product ";

            /// với TH lấy kho hàng tồn với status == 1
            if (status == 1)
            {
                querySQL += $" where QuantitySock > 0";
            }
            else
            {
                querySQL += $" where QuantitySock <= 0";

            }
            
            // thêm điều kiện tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                querySQL += $" and (NameProduct LIKE '%{search}%' OR TitleProduct LIKE '%{search}%' )";
            }

            var res = sqlConnector.Query<Product>(querySQL).ToList();
            return res;
        }
    }
}
