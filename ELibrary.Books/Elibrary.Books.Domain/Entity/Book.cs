﻿namespace ELibrary.Books.Domain.Entity
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int StockQuantity { get; set; }

        public Author Author { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
