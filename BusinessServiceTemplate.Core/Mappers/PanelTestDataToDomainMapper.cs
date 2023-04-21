using AutoMapper;
using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.DataAccess.Entities;

namespace BusinessServiceTemplate.Core.Mappers
{
    public class PanelTestDataToDomainMapper : Profile
    {
        public PanelTestDataToDomainMapper() {
            CreateMap<SC_Panel_Test, PanelTestDto>();
        }
    }
}
