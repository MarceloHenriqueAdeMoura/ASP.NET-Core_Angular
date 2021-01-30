
using System.Linq;
using AutoMapper;
using ProjectAngular.API.Dtos;
using ProjectAngular.Domain.Identity;
using ProjectAngular.Domain.Models;

namespace ProjectAngular.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDto>().ForMember(destinationMember: dest => dest.Palestrantes, memberOptions: opt => {
                opt.MapFrom(sourceMember: src => src.PalestranteEventos.Select(x => x.Palestrante).ToList());
            }).ReverseMap();

            CreateMap<Palestrante, PalestranteDto>().ForMember(destinationMember: dest => dest.Eventos, memberOptions: opt => {
                opt.MapFrom(sourceMember: src => src.PalestranteEventos.Select(x => x.Evento).ToList());
            }).ReverseMap();

            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}