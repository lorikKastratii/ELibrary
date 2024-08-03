namespace ELibrary.Books.Contracts.Models
{
    public class StockUpdateRequest
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
