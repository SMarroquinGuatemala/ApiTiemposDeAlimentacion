using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTiemposDeAlimentacion.Models;
using ApiTiemposDeAlimentacion.Models.Dtos;
using ApiTiemposDeAlimentacion.Repository;
using ApiTiemposDeAlimentacion.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTiemposDeAlimentacion.Controllers
{
   [Authorize]
   [Route("api/PersonalTiemposDeAlimentacion")]
   [ApiController]
   [ApiExplorerSettings(GroupName = "ApiPersonalTiemposDeAlimentacion")]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public class PersonalTiemposDeAlimentacionController : Controller
   {
      private readonly IPersonalTiemposDeAlimentacionRepository tiemposDeAlimentacionRepository;
      private readonly IMapper mapper;
      public PersonalTiemposDeAlimentacionController(IPersonalTiemposDeAlimentacionRepository personalTiemposDeAlimentacionRepository, IMapper Mapper)
      {

         tiemposDeAlimentacionRepository = personalTiemposDeAlimentacionRepository;
         //PersonalTiemposDeAlimentacionRepository tiemposDeAlimentacionRepository = new PersonalTiemposDeAlimentacionRepository();
         mapper = Mapper;
      }
      /// <summary>
      /// Obtener los tiempos de alimentacion por personal
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      [ProducesResponseType(200, Type = typeof(List<PersonalTiemposDeAlimentacionDto>))]
      [ProducesResponseType(404)]
      [ProducesDefaultResponseType]
      public IActionResult GetPersonalTiemposDeAlimentacion()
      {
         var listaPersonalTiemposDeAlimentacion = tiemposDeAlimentacionRepository.GetPersonalTiempos();
         var listaPersonalTiemposDeAlimentacionDto = new List<PersonalTiemposDeAlimentacionDto>();
         foreach (var lista in listaPersonalTiemposDeAlimentacion)
         {
            listaPersonalTiemposDeAlimentacionDto.Add(mapper.Map<PersonalTiemposDeAlimentacionDto>(lista));
         }

         return Ok(listaPersonalTiemposDeAlimentacionDto);
      }
      /// <summary>
      /// Crear tiempo de alimentación 
      /// </summary>
      /// <param name="personalTiemposDeAlimentacionCreateDto"></param>
      /// <returns></returns>
      [HttpPost]
      [ProducesResponseType(201, Type = typeof(List<PersonalTiemposDeAlimentacionCreateDto>))]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      
      public IActionResult CreatePersonalTiemposDeAlimentacion([FromBody]  PersonalTiemposDeAlimentacionCreateDto personalTiemposDeAlimentacionCreateDto )
      {
         if (personalTiemposDeAlimentacionCreateDto == null)
         {
            return BadRequest(ModelState);
         }

         if (tiemposDeAlimentacionRepository.ExistePersonalTiemposDeAlimentacion(personalTiemposDeAlimentacionCreateDto.NumeroDeEmpleado, personalTiemposDeAlimentacionCreateDto.Fecha, personalTiemposDeAlimentacionCreateDto.TiemposDeAlimentacionID ))
         {
            ModelState.AddModelError("", "El registro ya existe para este empleado");
            return StatusCode(404, ModelState);
         }

         var tiemposDeAlimentacionpersonal = mapper.Map<PersonalTiemposDeAlimentacion>(personalTiemposDeAlimentacionCreateDto);
         if (!tiemposDeAlimentacionRepository.CrearPersonalTiemposDeAlimentacion(tiemposDeAlimentacionpersonal))
         {
            ModelState.AddModelError("", $"Error al grabar el registro{tiemposDeAlimentacionpersonal.NumeroDeEmpleado}");
            return StatusCode(404, ModelState);
         }
         return Ok();
      }
      /// <summary>
      /// Actualizar tiempo de alimentación
      /// </summary>
      /// <param name="PersonalTiemposDeAlimentacionID"></param>
      /// <param name="personalTiemposDeAlimentacionUpdateDto"></param>
      /// <returns></returns>
      [HttpPatch("{PersonalTiemposDeAlimentacionID:int}", Name =  "ActualizarPersonalTiemposDeAlimentacion") ]
      [ProducesResponseType(204)]      
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]

      public IActionResult ActualizarPersonalTiemposDeAlimentacion(int PersonalTiemposDeAlimentacionID, [FromBody] PersonalTiemposDeAlimentacionUpdateDto personalTiemposDeAlimentacionUpdateDto)
      {
         if (personalTiemposDeAlimentacionUpdateDto == null || PersonalTiemposDeAlimentacionID!= personalTiemposDeAlimentacionUpdateDto.PersonalTiemposDeAlimentacionID)
         {
            return BadRequest(ModelState);
         }
        
         if (tiemposDeAlimentacionRepository.ExistePersonalTiemposDeAlimentacion(personalTiemposDeAlimentacionUpdateDto.NumeroDeEmpleado, personalTiemposDeAlimentacionUpdateDto.Fecha, personalTiemposDeAlimentacionUpdateDto.TiemposDeAlimentacionID))
         {
            ModelState.AddModelError("", "El registro ya existe para este empleado");
            return StatusCode(404, ModelState);
         }

         var tiemposDeAlimentacionpersonal = mapper.Map<PersonalTiemposDeAlimentacion>(personalTiemposDeAlimentacionUpdateDto);
         if (!tiemposDeAlimentacionRepository.ActualizarPersonalTiemposDeAlimentacion(tiemposDeAlimentacionpersonal))
         {
            ModelState.AddModelError("", $"Error al modificar  el registro{tiemposDeAlimentacionpersonal.NumeroDeEmpleado}");
            return StatusCode(404, ModelState);
         }
         return NoContent();
      }
      /// <summary>
      /// Eliminar tiempo de alimentación
      /// </summary>
      /// <param name="PersonalTiemposDeAlimentacionID"></param>
      /// <returns></returns>
      [HttpDelete("{PersonalTiemposDeAlimentacionID:int}", Name = "EliminarPersonalTiemposDeAlimentacion")]
      
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status409Conflict)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]

      public IActionResult EliminarPersonalTiemposDeAlimentacion(int PersonalTiemposDeAlimentacionID)
      {
                
         
         if (!tiemposDeAlimentacionRepository.BorrarPersonalTiemposDeAlimentacion(PersonalTiemposDeAlimentacionID))
         {
            ModelState.AddModelError("", $"Error al eliminar  el registro{PersonalTiemposDeAlimentacionID}");
            return StatusCode(404, ModelState);
         }
         return NoContent();
      }

   }
}
