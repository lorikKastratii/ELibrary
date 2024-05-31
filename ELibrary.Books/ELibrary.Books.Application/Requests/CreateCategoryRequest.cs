namespace ELibrary.Books.Application.Requests
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
