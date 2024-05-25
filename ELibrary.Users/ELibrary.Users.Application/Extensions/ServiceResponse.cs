using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ELibrary.Users.Application.Extensions
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public Error? Error { get; set; } = null;

        public ServiceResponse()
        {
            IsSuccess = Error == null;
            Error = null;
        }

        public ServiceResponse(string errorMessage)
        {
            Error = new Error(errorMessage);
            IsSuccess = false;
        }
    }
}
