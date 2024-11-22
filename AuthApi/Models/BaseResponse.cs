namespace AuthApi.Models
{
    public class BaseResponse<TResult>
    {
        public bool Success { get; set; }
        public dynamic Message { get; set; }
        public dynamic Exception { get; set; }
        public TResult Result { get; set; }
    }
    public class ResponseModel<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
