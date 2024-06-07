namespace ELibrary.Books.Application.Requests.Author
{
    public class CreateAuthorRequest
    {
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
