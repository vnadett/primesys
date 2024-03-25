namespace primesysfrontend.Models
{
    public class ProductModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
