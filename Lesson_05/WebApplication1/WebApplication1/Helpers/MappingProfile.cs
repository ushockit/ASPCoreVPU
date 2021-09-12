using AutoMapper;
using Domain.Entity;
using Services.Abstract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Helpers
{
    internal sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }
}
