using Newtonsoft.Json;
using primesysfrontend.CoreSettings;
using primesysfrontend.Interfaces;
using primesysfrontend.Models;
using System.Text;

namespace primesysfrontend.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public ProductService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> CreateProduct(ProductModel product)
        {
            if (product == null) return false;

            var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.ProductEndpoint + "CreateNewProduct");
            request.Content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<BaseResponseModel<bool>>(content);

            if (res != null && res.Success)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ProductModel>> GetProducts()
        {

            var request = new HttpRequestMessage(HttpMethod.Get, Endpoints.ProductEndpoint + "GetAllProduct");

            HttpResponseMessage response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var res = JsonConvert.DeserializeObject<BaseResponseModel<List<ProductModel>>>(content);
                if (res != null && res.Success && res.Result != null)
                {
                    return res.Result;
                }
            }
            return null;

        }
        public async Task<bool> UpdateProduct(ProductModel product)
        {
            if (product == null) return false;

            var request = new HttpRequestMessage(HttpMethod.Put, Endpoints.ProductEndpoint + "UpdateProduct");
            request.Content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<BaseResponseModel<bool>>(content);

            if (res != null && res.Success)
            {
                return true;
            }
            return false;

        }
        public async Task<bool> DeleteProduct(int id)
        {

            var request = new HttpRequestMessage(HttpMethod.Delete, Endpoints.ProductEndpoint + "DeleteProduct/" + id);

            HttpResponseMessage response = await _client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<BaseResponseModel<bool>>(content);

            if (res != null && res.Success)
            {
                return true;
            }
            return false;

        }
    }
}
