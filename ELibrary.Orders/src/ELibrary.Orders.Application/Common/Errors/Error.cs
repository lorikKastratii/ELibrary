namespace ELibrary.Orders.Application.Common.Errors
{
    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }

        public string Message{ get; set; }
    }
}
