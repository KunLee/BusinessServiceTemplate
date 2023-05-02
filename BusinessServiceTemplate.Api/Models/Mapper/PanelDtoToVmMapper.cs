using AutoMapper;
using BusinessServiceTemplate.Api.Models.ResponseModels;
using BusinessServiceTemplate.Core.Dtos;

namespace BusinessServiceTemplate.Api.Models.Mapper
{    
    public class PanelDtoToVmMapper : Profile
    {
        public PanelDtoToVmMapper()
        {
            CreateMap<PanelDto, PanelResponseModel>()
                .ForMember(dest => dest.Tests, opt => opt.MapFrom(so => so.Tests.Select(t => 
                    new TestInPanelResponseModel 
                    { 
                        Id = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        DescriptionVisibility = t.DescriptionVisibility
                    }).ToList()));
        }
    }
}
