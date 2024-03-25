namespace primesysfrontend.Models
{
    public class BaseResponseModel<T>
    {
        public bool Success { get; set; }

        public T? Result { get; set; }
        public string? Message { get; set; }
    }
}
