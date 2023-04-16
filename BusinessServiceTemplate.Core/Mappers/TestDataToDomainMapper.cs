using AutoMapper;
using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.DataAccess.Entities;

namespace BusinessServiceTemplate.Core.Mappers
{
    public class TestDataToDomainMapper : Profile
    {
        public TestDataToDomainMapper()
        {
            CreateMap<SC_Test, TestDto>();
        }
    }
}
