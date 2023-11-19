using BookStoreCore.Entity;
using BookStoreCore.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;




namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[TypeFilter(typeof(UserAuthorizationFilterAttribute))]
    public class ProductController : ControllerBase
    {
        IProductRepository _IProductRepository;

        public ProductController(IProductRepository IProductRepository)
        {
            _IProductRepository = IProductRepository;
        }

        /// <summary>
        /// Lấy tất cả sản phẩm
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel getAllProduct()
        {
            var res = new ResponseModel();

            res.Data = _IProductRepository.GetAll();

            return res;
        }

        /// <summary>
        /// Lấy số lượng tất cả sản phẩm
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public ResponseModel getCountProduct()
        {
            var res = new ResponseModel();

            res.Data = _IProductRepository.Count();

            return res;
        }

        /// <summary>
        /// Lấy chi tiết của sản phẩm theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail")]
        public IActionResult GetProductDetailById(string id)
        {
            var res = new ResponseModel();
            res.Data = _IProductRepository.GetById(id);
            res.Success = true;
            return Ok(res);
        }

        /// <summary>
        /// Phân trang + lọc dữ liệu sản phẩm theo trường name và title
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost("filterbycategory")]
        public ResponseModel FilterByCategory(int IdCategory, int pageNumber, int pageSize, [FromBody] string filter)
        {

            ResponseModel response = new ResponseModel();

            response.Data = _IProductRepository.filterByCategory(filter, pageNumber, pageSize, IdCategory);

            return response;
        }



        /// <summary>
        /// Phân trang + lọc dữ liệu sản phẩm theo trường name và title
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost("filter")]
        public ResponseModel Filter(int pageNumber, int pageSize, [FromBody] string filter)
        {

            ResponseModel response = new ResponseModel();

            response.Data = _IProductRepository.filter(filter, pageNumber, pageSize);

            return response;
        }

        /// <summary>
        /// Lấy sản phẩm theo danh mục
        /// </summary>
        /// <param name="IdCategory"></param>
        /// <returns></returns>
        [HttpGet("getByCategory/{IdCategory}")]
        public ResponseModel getByIdCategory(int IdCategory)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                response.Data = _IProductRepository.getByIdCategory(IdCategory);
                response.Status = 200;
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = 400;
                response.Success = false;
            }
            return response;
        }


        /// <summary>
        ///  Thêm sản phẩm
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        public ResponseModel insertNewProduct([FromBody] Product product)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                _IProductRepository.InsertProduct(product);
                response.Status = 200;
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = 400;
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///  Cập nhật thông tin sản phẩm
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("update")]
        public ResponseModel updateProduct([FromBody] Product product)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                _IProductRepository.UpdateProduct(product);
                response.Status = 200;
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = 400;
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///  Lấy tất cả danh mục sản phẩm
        /// </summary>
        /// <returns></returns>
        [HttpGet("allCategory")]
        public ResponseModel allCategory()
        {
            ResponseModel response = new ResponseModel();

            try
            {
                response.Data = _IProductRepository.getAllCategory();
                response.Status = 200;
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = 400;
                response.Success = false;
            }

            return response;
        }

        [HttpPost("storage")]
        public ResponseModel getProductByStatus([FromBody]StorageRequestModel storageRequest)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                response.Data = _IProductRepository.getProductStorage(storageRequest.search, storageRequest.status);
                response.Status = 200;
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = 400;
                response.Success = false;
            }

            return response;
        }

    }
}