using primesys_backend.Models;

namespace primesys_backend.Interfaces
{
    public interface IProductService
    {
        public Task<ResultModel<bool>> CreateProduct(ProductCreateModel product);
        public Task<ResultModel<List<ProductBaseModel>>> GetAllProduct();
        public Task<ResultModel<bool>> DeleteProduct(int prodId);
        public Task<ResultModel<ProductBaseModel>> GetProductById(int prodId);
        public Task<ResultModel<bool>> UpdateProduct(ProductBaseModel product);
    }
}
