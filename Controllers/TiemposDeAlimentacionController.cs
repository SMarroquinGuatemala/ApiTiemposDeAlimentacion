using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTiemposDeAlimentacion.Models.Dtos;
using ApiTiemposDeAlimentacion.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent;

namespace ApiTiemposDeAlimentacion.Controllers
{
   [Authorize]
   [Route("api/TiemposDeAlimentacion")]
   [ApiController]
   [ApiExplorerSettings(GroupName = "ApiTiemposDeAlimentacion")]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public class TiemposDeAlimentacionController : Controller
   {
      private readonly ITiemposDeAlimentacionRepository _tiemposDeAlimentacion;
      private readonly IMapper _mapper;
      public TiemposDeAlimentacionController(ITiemposDeAlimentacionRepository tiemposDeAlimentacion, IMapper mapper)
      {
         _tiemposDeAlimentacion = tiemposDeAlimentacion;
         _mapper = mapper;
      }
      /// <summary>
      /// Obtener los tiempos de alimentacion en base al horario 
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      [ProducesResponseType(200, Type = typeof(List<TiemposDeAlimentacionDto>))]
      [ProducesResponseType(404)]
      [ProducesDefaultResponseType]
      public IActionResult GetTiemposDeAlimentacion()
      {
         var listaTiemposDeAlimentacion = _tiemposDeAlimentacion.GetTiempos();
         var listaTiemposDeAlimentacionDto = new List<TiemposDeAlimentacionDto>();
         foreach (var lista in listaTiemposDeAlimentacion)
         {
            listaTiemposDeAlimentacionDto.Add(_mapper.Map<TiemposDeAlimentacionDto>(lista));
         }

         return Ok(listaTiemposDeAlimentacionDto);
      }
   }
}
