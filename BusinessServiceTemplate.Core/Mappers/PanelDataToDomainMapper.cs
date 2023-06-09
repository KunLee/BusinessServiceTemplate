﻿using AutoMapper;
using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.DataAccess.Entities;

namespace BusinessServiceTemplate.Core.Mappers
{
    public class PanelDataToDomainMapper : Profile
    {
        public PanelDataToDomainMapper() {
            CreateMap<SC_Panel, PanelDto>()
                .ForMember(dest => dest.Tests, opt => opt.MapFrom(source => source.Tests));
        }
    }
}
