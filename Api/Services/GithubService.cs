using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TakeRepositoriesApi.Model;

namespace TakeRepositoriesApi.Services
{
    public class GithubService : IHostingSoftwareService
    {
        private readonly HttpClient _httpClient;

        public GithubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CodeRepository>> ListCodeRepositoryAsync(string organization, bool? sortByCreatedAsc = null)
        {
            var relativeUri = @$"orgs/{organization}/repos?{((sortByCreatedAsc ?? false) ? "sort=created&direction=asc" : string.Empty)}";

            var response = await _httpClient.GetAsync(relativeUri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<CodeRepository>>(content);
            }
            throw new HttpRequestException();
        }
    }
}