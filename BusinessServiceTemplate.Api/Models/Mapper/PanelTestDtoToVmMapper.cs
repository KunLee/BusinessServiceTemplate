using AutoMapper;
using BusinessServiceTemplate.Api.Models.ResponseModels;
using BusinessServiceTemplate.Core.Dtos;

namespace BusinessServiceTemplate.Api.Models.Mapper
{    
    public class PanelTestDtoToVmMapper : Profile
    {
        public PanelTestDtoToVmMapper()
        {
            CreateMap<PanelTestDto, PanelTestResponseModel>();
        }
    }
}
