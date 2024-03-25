using Newtonsoft.Json;
using primesys_backend.Interfaces;
using primesys_backend.Models;

namespace primesys_backend.Services
{
    public class ProductService : IProductService
    {
        private readonly string jsonPath = "data.json";
        private string fileContent = "";
        public int currentId = 0;
        public List<ProductBaseModel> products = new();
        public ProductService()
        {
            fileContent = ReadFile();
            products = GetProductList();
            GetProductList();
            GetLastItemId();
        }

        public string ReadFile()
        {
            if (!File.Exists(jsonPath))
            {
                using (FileStream fs = File.Create(jsonPath)) ;
            }

            return File.ReadAllText(jsonPath);
        }
        public void GetLastItemId()
        {
            if (products != null && products.Count > 0)
                currentId = products.Last().Id;
        }
        public void WriteToFile<T>(T objToWrite)
        {
            File.WriteAllText(jsonPath, string.Empty);

            string newJsonContent = JsonConvert.SerializeObject(objToWrite, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter sw = File.AppendText(jsonPath))
            {
                sw.WriteLine(newJsonContent);
            }

        }

        public List<ProductBaseModel> GetProductList()
        {
            if (!String.IsNullOrEmpty(fileContent))
            {
                return JsonConvert.DeserializeObject<List<ProductBaseModel>>(fileContent);
            }
            return new List<ProductBaseModel>();
        }

        public bool CheckManufacturerAndProductName(string ManufacturerName, string ProductName)
        {
            var search = products.Where(x => x.Manufacturer == ManufacturerName && x.Name == ProductName).FirstOrDefault();
            if (search != null)
            {
                return true;
            }
            return false;
        }

        public async Task<ResultModel<bool>> CreateProduct(ProductCreateModel product)
        {
            try
            {
                GetLastItemId();

                if (CheckManufacturerAndProductName(product.Manufacturer, product.Name))
                {
                    return new ResultModel<bool> { Success = false, Result = false, Message = "The product already exists for this manufacturer." };
                }

                var newProduct = new ProductBaseModel
                {
                    Id = currentId + 1,
                    Name = product.Name,
                    Manufacturer = product.Manufacturer,
                    Price = product.Price,
                    IsActive = true
                };

                products.Add(newProduct);

                WriteToFile(products);

                return new ResultModel<bool> { Success = true, Result = true, Message = "Product created." };
            }
            catch (Exception e)
            {
                return new ResultModel<bool> { Success = false, Result = false, Message = e.ToString() };
            }
        }
        public async Task<ResultModel<List<ProductBaseModel>>> GetAllProduct()
        {
            if (products != null && products.Any())
            {
                return new ResultModel<List<ProductBaseModel>> { Success = true, Result = products?.Where(x => x.IsActive).ToList() };
            }
            return new ResultModel<List<ProductBaseModel>> { Success = true, Message = "Empty list" };
        }

        public async Task<ResultModel<bool>> DeleteProduct(int prodId)
        {
            var selectedItem = products?.Where(x => x.Id == prodId).FirstOrDefault();
            if (selectedItem != null)
            {
                selectedItem.IsActive = false;
                WriteToFile(products);
                return new ResultModel<bool> { Success = true };
            }
            return new ResultModel<bool> { Success = false };
        }

        public async Task<ResultModel<ProductBaseModel>> GetProductById(int prodId)
        {
            var selectedItem = products?.Where(x => x.Id == prodId && x.IsActive).FirstOrDefault();
            if (selectedItem != null)
            {
                return new ResultModel<ProductBaseModel> { Success = true, Result = selectedItem };
            }
            return new ResultModel<ProductBaseModel> { Success = false, Message = "Id not found" };
        }

        public async Task<ResultModel<bool>> UpdateProduct(ProductBaseModel product)
        {
            if (CheckManufacturerAndProductName(product.Manufacturer, product.Name))
            {
                return new ResultModel<bool> { Success = false, Result = false, Message = "The product already exists for this manufacturer." };
            }
            var selectedItem = products?.Where(x => x.Id == product.Id).FirstOrDefault();

            if (selectedItem != null)
            {
                selectedItem.Name = product.Name;
                selectedItem.Price = product.Price;
                selectedItem.Manufacturer = product.Manufacturer;
                WriteToFile(products);
                return new ResultModel<bool> { Success = true, Result = true };
            }
            return new ResultModel<bool> { Success = false, Result = false };
        }
    }
}
