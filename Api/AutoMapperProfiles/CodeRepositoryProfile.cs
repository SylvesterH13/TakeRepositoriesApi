using AutoMapper;
using TakeRepositoriesApi.ApiModel;
using TakeRepositoriesApi.Model;

namespace TakeRepositoriesApi.AutoMapperProfiles
{
    public class CodeRepositoryProfile : Profile
    {
        public CodeRepositoryProfile()
        {
            CreateMap<CodeRepository, GetCodeRepositoriesResponseApiModel>();
        }
    }
}