using System.Collections.Generic;
using System.Threading.Tasks;
using TakeRepositoriesApi.Model;

namespace TakeRepositoriesApi.Services
{
    public interface IHostingSoftwareService
    {
        Task<IEnumerable<CodeRepository>> ListCodeRepositoryAsync(string organization, bool? sortByCreatedAsc = null);
    }
}