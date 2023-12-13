using Microsoft.Extensions.Configuration;
using N5.Domain;
using N5.Interfaces;
using Nest;

namespace N5.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IElasticClient _client;
        private readonly IConfiguration _configuration;
        public ElasticSearchService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = CreateInstance();
            
        }
        private ElasticClient CreateInstance()
        {
            string host = _configuration["ElasticSearchServer:Host"];
            string username = _configuration["ElasticSearchServer:Username"];
            string password = _configuration["ElasticSearchServer:Password"];
            var settings = new ConnectionSettings(new Uri(host));
            settings.EnableApiVersioningHeader();
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                settings.BasicAuthentication(username, password);
            return new ElasticClient(settings);
        }
        public async Task InsertDocument(string indexName, Permission permission)
        {
            var ping = await _client.PingAsync();
            var response = await _client.CreateAsync(permission, q => q.Index(indexName));
            if (response.ApiCall?.HttpStatusCode == 409)
            {
                await _client.UpdateAsync<Permission>(permission.Id, a => a.Index(indexName).Doc(permission));
            }
        }
    }
}
