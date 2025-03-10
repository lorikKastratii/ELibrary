namespace ELibrary.Consumers.ElasticService.Clients
{
    public interface IBookClient
    {
        Task<Models.Book> GetBookAsync(int id);
    }
}
