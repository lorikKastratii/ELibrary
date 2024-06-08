namespace Elibrary.Books.Domain.Entity
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
