namespace Neredekal.Application.Models.Response
{
    public class BaseResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public  string ErrorMessage { get; set; }
        
        public T Data { get; set; }

        public void SetMessage(string message, bool isSuccess = false)
        {
            Message = message;
            IsSuccess = isSuccess;
        }
    }
}