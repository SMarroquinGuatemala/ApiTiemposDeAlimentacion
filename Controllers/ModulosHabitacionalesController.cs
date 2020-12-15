using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiTiemposDeAlimentacion.Models.Dtos;
using ApiTiemposDeAlimentacion.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTiemposDeAlimentacion.Controllers
{
   [Authorize] 
   //[AllowAnonymous]
   [Route("api/ModulosHabitacionales")]
   [ApiController]
   [ApiExplorerSettings(GroupName = "ApiModulosHabitacionales")] 
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public class ModulosHabitacionalesController : Controller
   {
      private readonly IModuloHabitacionalRepository _moduloHabitacionalRepo;
      private readonly IMapper _mapper;
      public ModulosHabitacionalesController(IModuloHabitacionalRepository moduloHabitacionalRepo, IMapper mapper)
      {
         _moduloHabitacionalRepo = moduloHabitacionalRepo;
         _mapper = mapper;
      } 
      /// <summary>
      /// Obtener los modulos habitacionales de los servicios de cortadores
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      [ProducesResponseType(200, Type = typeof(List<ModuloHabitacionalDto>) )]
      [ProducesResponseType(400)]
      public IActionResult GetModulosHabitacionales ()
      {
         var listaModulosHabitacionales = _moduloHabitacionalRepo.GetModulosHabitacionales();
         var listaMoudlosHabitacionalesDto = new List<ModuloHabitacionalDto>();
         foreach (var lista in listaModulosHabitacionales)
         {
            listaMoudlosHabitacionalesDto.Add(_mapper.Map<ModuloHabitacionalDto>(lista));
         }

         return Ok(listaMoudlosHabitacionalesDto);
      }



   }
}
