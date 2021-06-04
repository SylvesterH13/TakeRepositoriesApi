using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeRepositoriesApi.ApiModel;
using TakeRepositoriesApi.Filters;
using TakeRepositoriesApi.Services;

namespace TakeRepositoriesApi.Controllers
{
    [ApiController]
    [ApiKeyFilter]
    [Route("[controller]")]
    public class CodeRepositoryController : ControllerBase
    {
        private readonly IHostingSoftwareService _hostingSoftwareService;
        private readonly IMapper _mapper;

        public CodeRepositoryController(IHostingSoftwareService hostingSoftwareService, IMapper mapper)
        {
            _hostingSoftwareService = hostingSoftwareService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string language, int? limit, bool? sortByCreatedAsc = null)
        {
            var repositories = (await _hostingSoftwareService.ListCodeRepositoryAsync(organization: "takenet", sortByCreatedAsc: sortByCreatedAsc))
                .Where(r => string.IsNullOrWhiteSpace(language) || language == r.Language)
                .Take(limit != null ? limit.Value : int.MaxValue);

            var responseModel = _mapper.Map<IEnumerable<GetCodeRepositoriesResponseApiModel>>(repositories);

            return Ok(responseModel);
        }
    }
}