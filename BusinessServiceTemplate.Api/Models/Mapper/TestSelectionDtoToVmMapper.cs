using AutoMapper;
using BusinessServiceTemplate.Api.Models.ResponseModels;
using BusinessServiceTemplate.Core.Dtos;

namespace BusinessServiceTemplate.Api.Models.Mapper
{    
    public class TestSelectionDtoToVmMapper : Profile
    {
        public TestSelectionDtoToVmMapper()
        {
            CreateMap<TestSelectionDto, TestSelectionResponseModel>();
        }
    }
}
