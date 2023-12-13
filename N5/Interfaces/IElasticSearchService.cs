using N5.Domain;

namespace N5.Interfaces
{
    public interface IElasticSearchService
    {
        Task InsertDocument(string indexName, Permission permission);
    }
}
