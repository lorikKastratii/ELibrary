namespace ELibrary.Books.Domain.Exceptions.Book
{
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException(int bookId) : base($"Book with Id: {bookId} was not found")
        {
        }
    }
}
