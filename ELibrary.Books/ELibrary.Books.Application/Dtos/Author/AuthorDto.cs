namespace ELibrary.Books.Application.Dtos.Author
{
    public record AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
