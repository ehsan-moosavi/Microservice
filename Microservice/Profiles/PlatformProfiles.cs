using Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microservice.Dtos;

namespace Microservice.Profiles
{
    public class PlatformProfiles : Profile
    {
        public PlatformProfiles()
        {
            //source to Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();
        }

    }
}
