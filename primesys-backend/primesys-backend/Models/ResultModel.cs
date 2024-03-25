namespace primesys_backend.Models
{
    public class ResultModel<T>
    {
        public bool Success { get; set; }

        public T? Result { get; set; }
        public string? Message { get; set; }
    }
}
