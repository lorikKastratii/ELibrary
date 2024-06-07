namespace ELibrary.Books.Application.Extensions
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public Error? Error { get; set; } = null;

        public ServiceResponse()
        {
            IsSuccess = Error == null;
        }

        public ServiceResponse(string error)
        {
            Error = new Error(error);
            IsSuccess = false;
        }

        public ServiceResponse(T data)
        {
            Data = data;
        }
    }
}
