using AutoMapper;
using Ginasio.Core.DTOs;
using Ginasio.Infrastructure.Data.Models;

namespace Ginasio.Core.Mappers
{
    public class GinasioMapper: Profile
    {
        public GinasioMapper()
        {
            CreateMap<GinasioData, GinasioDto>().ReverseMap();
            CreateMap<EnderecoData, EnderecoDto>().ReverseMap();
        }
    }
}