using AutoMapper;
using BusinessServiceTemplate.Api.Models.ResponseModels;
using BusinessServiceTemplate.Core.Dtos;

namespace BusinessServiceTemplate.Api.Models.Mapper
{    
    public class TestSelectionDtoToVmMapper : Profile
    {
        public TestSelectionDtoToVmMapper()
        {
            CreateMap<TestSelectionDto, TestSelectionResponseModel>()
                .ForMember(dest => dest.Panels, opt => opt.MapFrom(so => so.Panels.Select(t => t.Id).ToList()));
        }
    }
}
