namespace ELibrary.Books.Domain.Exceptions.Book
{
    public class InvalidBookException : Exception
    {
        public InvalidBookException() : base("Book was invalid.")
        {            
        }
    }
}
