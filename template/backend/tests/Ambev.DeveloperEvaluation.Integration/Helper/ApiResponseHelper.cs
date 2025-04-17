namespace Ambev.DeveloperEvaluation.Integration.Helper
{
    public class ApiResponseHelper<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
    }
}
