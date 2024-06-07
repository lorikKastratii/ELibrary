namespace ELibrary.Books.Application.Requests.Book
{
    public class CreateBookRequest
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
