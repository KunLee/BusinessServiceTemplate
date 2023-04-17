using AutoMapper;
using BusinessServiceTemplate.Api.Models.ViewModels;
using BusinessServiceTemplate.Core.Dtos;

namespace BusinessServiceTemplate.Api.Models.Mapper
{    public class PanelDtoToVmMapper : Profile
    {
        public PanelDtoToVmMapper()
        {
            CreateMap<PanelDto, PanelViewModel>()
                .ForMember(dest => dest.Tests, opt => opt.MapFrom(so => so.Tests.Select(t => t.Id).ToList()));
        }
    }
}
