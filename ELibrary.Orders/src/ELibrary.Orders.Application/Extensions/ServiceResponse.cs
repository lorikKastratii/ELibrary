using ELibrary.Orders.Application.Extensions.Errors;

namespace ELibrary.Orders.Application.Extensions
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public Error? Error { get; set; }

        public static ServiceResponse<T> Success(T? data) => new(data);
        public static ServiceResponse<T> Failure(string error) => new(error);

        private ServiceResponse(T? data)
        {
            Data = data;
            IsSuccess = true;
            Error = null;
        }
        
        private ServiceResponse(string errorMessage)
        {
            IsSuccess = false;
            Error = new Error(errorMessage);
        }
    }
}
