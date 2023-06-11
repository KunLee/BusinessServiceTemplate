using AutoMapper;
using BusinessServiceTemplate.Api.Models.ResponseModels;
using BusinessServiceTemplate.Core.Dtos;

namespace BusinessServiceTemplate.Api.Models.Mapper
{    
    public class CurrencyDtoToVmMapper : Profile
    {
        public CurrencyDtoToVmMapper()
        {
            CreateMap<CurrencyDto, CurrencyResponseModel>();
        }
    }
}
