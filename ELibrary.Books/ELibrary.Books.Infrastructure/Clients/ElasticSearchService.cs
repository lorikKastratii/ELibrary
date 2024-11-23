using ELibrary.Books.Application.Interfaces;
using Nest;

namespace ELibrary.Books.Infrastructure.Clients
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task IndexAsync<T>(T document, string indexName) where T : class
        {
            var response = await _elasticClient.IndexAsync(document, i => i.Index(indexName));
            if (!response.IsValid)
            {
                throw new Exception($"Failed to index document: {response.DebugInformation}");
            }
        }

        public async Task<IEnumerable<T>> SearchAsync<T>(string indexName, string query) where T : class
        {
            var existsResponse = await _elasticClient.Indices.ExistsAsync(indexName);
            if (!existsResponse.Exists)
            {
                throw new Exception($"Index '{indexName}' does not exist.");
            }

            var response = await _elasticClient.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .QueryString(d => d.Query(query))));

            if (!response.IsValid)
            {
                throw new Exception($"Failed to search index: {response.DebugInformation}");
            }

            if (!response.Documents.Any())
            {
                return Enumerable.Empty<T>();
            }

            return response.Documents;
        }
    }
}
