using AutoMapper;
using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.DataAccess.Entities;

namespace BusinessServiceTemplate.Core.Mappers
{
    public class AmaDataToDomainMapper : Profile
    {
        public AmaDataToDomainMapper() 
        {
            CreateMap<SC_AMA, AmaDto>().ReverseMap();
        }
    }
}
