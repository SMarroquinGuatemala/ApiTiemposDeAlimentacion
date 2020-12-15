using ApiTiemposDeAlimentacion.Models;
using ApiTiemposDeAlimentacion.Models.Dtos;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.TiemposDeAlimentacionMapper
{
   public class TiemposDeAlimentacionMappers: Profile
   {
      public TiemposDeAlimentacionMappers()
      {
         
         CreateMap<ModulosHabitacionales, ModuloHabitacionalDto>().ReverseMap();
         CreateMap<TiemposDeAlimentacion, TiemposDeAlimentacionDto>().ReverseMap();
         CreateMap<PersonalTiemposDeAlimentacion, PersonalTiemposDeAlimentacionDto>().ReverseMap();
         CreateMap<PersonalTiemposDeAlimentacion, PersonalTiemposDeAlimentacionCreateDto>().ReverseMap();
         CreateMap<PersonalTiemposDeAlimentacion, PersonalTiemposDeAlimentacionUpdateDto>().ReverseMap();
         CreateMap<Usuario, UsuarioDto>().ReverseMap();
         CreateMap<Usuario, UsuarioAuthDto>().ReverseMap();
         CreateMap<Usuario, UsuarioAuthLoginDto>().ReverseMap();
      }
   }
}
