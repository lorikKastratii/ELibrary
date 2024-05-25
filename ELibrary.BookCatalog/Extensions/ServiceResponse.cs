namespace ELibrary.BookCatalog.Extensions
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public Error? Error { get; set; } = null;

        public ServiceResponse()
        {
            IsSuccess = true;
            Error = null;
        }

        public ServiceResponse(string errorMessage)
        {
            Error = new Error(errorMessage);
            IsSuccess = false;
        }
    }
}
