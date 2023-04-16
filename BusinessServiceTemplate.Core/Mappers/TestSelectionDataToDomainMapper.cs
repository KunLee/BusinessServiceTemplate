using AutoMapper;
using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.DataAccess.Entities;

namespace BusinessServiceTemplate.Core.Mappers
{
    public class TestSelectionDataToDomainMapper : Profile
    {
        public TestSelectionDataToDomainMapper()
        {
            CreateMap<SC_TestSelection, TestSelectionDto>();
        }
    }
}
