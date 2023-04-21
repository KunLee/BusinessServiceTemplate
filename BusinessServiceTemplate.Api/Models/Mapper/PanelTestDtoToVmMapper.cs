using AutoMapper;
using BusinessServiceTemplate.Api.Models.ViewModels;
using BusinessServiceTemplate.Core.Dtos;

namespace BusinessServiceTemplate.Api.Models.Mapper
{    
    public class PanelTestDtoToVmMapper : Profile
    {
        public PanelTestDtoToVmMapper()
        {
            CreateMap<PanelTestDto, PanelTestViewModel>();
        }
    }
}
