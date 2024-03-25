namespace primesys_backend.Models
{
    public class ProductBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
