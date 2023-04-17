﻿using AutoMapper;
using BusinessServiceTemplate.Api.Models.ViewModels;
using BusinessServiceTemplate.Core.Dtos;

namespace BusinessServiceTemplate.Api.Models.Mapper
{    public class TestDtoToVmMapper : Profile
    {
        public TestDtoToVmMapper()
        {
            CreateMap<TestDto, TestViewModel>()
                .ForMember(dest => dest.Panels, opt => opt.MapFrom(so => so.Panels.Select(t => t.Id).ToList()));
        }
    }
}