namespace ELibrary.Consumers.ElasticService.Interfaces
{
    public interface IElasticSearchService
    {
        Task<bool>IndexAsync<T>(T document, string indexName) where T : class;
        Task<IEnumerable<T>> SearchAsync<T>(string indexName, string query) where T : class;
    }
}
