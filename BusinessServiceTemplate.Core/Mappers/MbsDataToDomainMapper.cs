using AutoMapper;
using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.DataAccess.Entities;

namespace BusinessServiceTemplate.Core.Mappers
{
    public class MbsDataToDomainMapper : Profile
    {
        public MbsDataToDomainMapper() 
        {
            CreateMap<SC_MBS, MbsDto>().ReverseMap();
        }
    }
}
