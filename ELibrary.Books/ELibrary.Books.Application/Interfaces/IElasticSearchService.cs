namespace ELibrary.Books.Application.Interfaces
{
    public interface IElasticSearchService
    {
        Task IndexAsync<T>(T document, string indexName) where T : class;
        Task<IEnumerable<T>> SearchAsync<T>(string indexName, string query) where T : class;
    }
}
