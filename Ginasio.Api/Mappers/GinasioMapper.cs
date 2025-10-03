using AutoMapper;
using Ginasio.Api.Models;
using Ginasio.Core.DTOs;

namespace Ginasio.Api.Mappers
{
    public class GinasioMapper : Profile
    {
        public GinasioMapper()
        {
            CreateMap<GinasioDto, Gym>().ReverseMap();
            CreateMap<EnderecoDto, Address>().ReverseMap();
        }
    }
}