namespace ELibrary.Books.Application.Extensions
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public Error? Error { get; set; } = null;

        public static ServiceResponse<T> Success(T? data) => new(true, null, data);
        public static ServiceResponse<T> Failure(string errorMessage) => new(false, new Error(errorMessage));

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

        private ServiceResponse(bool isSuccess, Error? error, T? data)
        {
            IsSuccess = isSuccess;
            Error = error;
            Data = data;
        }
        
        private ServiceResponse(bool isSuccess, Error? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }
}
