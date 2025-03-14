namespace DummyApi.Common
{
    public class ApiResponse<T>
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ApiResponse<T> Result(T data, string message)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data
            };
        }
        public static ApiResponse<T> Error(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message
            };
        }
    }
}