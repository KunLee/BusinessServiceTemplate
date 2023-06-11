using AutoMapper;
using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.DataAccess.Entities;

namespace BusinessServiceTemplate.Core.Mappers
{
    public class CurrencyDataToDomainMapper : Profile
    {
        public CurrencyDataToDomainMapper() 
        {
            CreateMap<SC_Currency, CurrencyDto>();
        }
    }
}
