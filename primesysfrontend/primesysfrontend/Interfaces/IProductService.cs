using primesysfrontend.Models;

namespace primesysfrontend.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProduct(ProductModel product);
        Task<List<ProductModel>> GetProducts();
        Task<bool> UpdateProduct(ProductModel product);
        Task<bool> DeleteProduct(int id);
    }
}
