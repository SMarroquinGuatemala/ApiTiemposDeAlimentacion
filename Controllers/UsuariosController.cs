using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiTiemposDeAlimentacion.Models;
using ApiTiemposDeAlimentacion.Models.Dtos;
using ApiTiemposDeAlimentacion.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiTiemposDeAlimentacion.Controllers
{
   [Authorize]
   [Route("api/Usuarios")]
   [ApiController]
   [ApiExplorerSettings(GroupName = "ApiUsuarios")]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public class UsuariosController : Controller
   {
      IUsuarioRepository UsuarioRepository;
      IMapper Mapper;
      private readonly IConfiguration _configuration;
      public UsuariosController(IUsuarioRepository usuarioRepository , IMapper mapper, IConfiguration configuration)
      {
         UsuarioRepository = usuarioRepository;
         Mapper = mapper;
         _configuration = configuration;
      }
      /// <summary>
      /// Obtener usuarios 
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      [ProducesResponseType(200, Type = typeof(List<UsuarioDto>))]
      [ProducesResponseType(404)]
      [ProducesDefaultResponseType]
      public IActionResult GetUsuarios()
      {
         var listausuarios = UsuarioRepository.GetUsuarios();
         var listaUsuariosDto = new List<UsuarioDto>();

         foreach (var lista in listausuarios)
         {
            listaUsuariosDto.Add(Mapper.Map<UsuarioDto>(lista));
         }
         return Ok(listaUsuariosDto);

      }
      /// <summary>
      /// Obtener usuario por ID
      /// </summary>
      /// <param name="IdUsuario">Id Usuario</param>
      /// <returns></returns>

      [HttpGet("{IdUsuario:int}", Name = "GetUsuarioByID")]
      [ProducesResponseType(200, Type = typeof(List<UsuarioDto>))]
      [ProducesResponseType(404)]
      [ProducesDefaultResponseType]
      public IActionResult GetUsuarioByID(int IdUsuario)
      {
         //var listaPersonalTiemposDeAlimentacion = tiemposDeAlimentacionRepository.GetPersonalTiempos();
         //var listaPersonalTiemposDeAlimentacionDto = new List<PersonalTiemposDeAlimentacionDto>();
         //foreach (var lista in listaPersonalTiemposDeAlimentacion)
         //{
         //   listaPersonalTiemposDeAlimentacionDto.Add(mapper.Map<PersonalTiemposDeAlimentacionDto>(lista));
         //}

         //return Ok(listaPersonalTiemposDeAlimentacionDto);
         var usuario = UsuarioRepository.GetUsuario(IdUsuario);
         //UsuarioDto usuarioDto = new UsuarioDto();
         var usuarioDto = Mapper.Map<UsuarioDto>(usuario);//UsuarioRepository.GetUsuario(IdUsuario);
         //Mapper.Map<UsuarioDto>(UsuarioRepository.GetUsuario(IdUsuario));
         //return Ok(usuarioDto);
         //var tiemposDeAlimentacionpersonal = Mapper.Map<UsuarioRepository.GetUsuario(IdUsuario)>(UsuarioDto);
         return Ok(usuarioDto);

      }
      /// <summary>
      /// Registro de Usuario
      /// </summary>
      /// <param name="usuarioAuthDto">Numero de Empleado, Usuario y Password</param>
      /// <returns></returns>
      [AllowAnonymous]
      [HttpPost("Registro")]
      [ProducesResponseType(201, Type = typeof(List<UsuarioAuthDto>))]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public IActionResult Registro(UsuarioAuthDto usuarioAuthDto)
      {
         if (UsuarioRepository.ExisteUsuario(usuarioAuthDto.Usuario))
         {
            return BadRequest("El usuario ya existe");
         }

         var usuarioACrear = new Usuario
         { 
         NumeroDeEmpleado = usuarioAuthDto.NumeroDeEmpleado,
         UsuarioAcceso = usuarioAuthDto.Usuario
         
         };

         var usuarioCreado = UsuarioRepository.Registro(usuarioACrear, usuarioAuthDto.Password);
         return Ok(usuarioCreado);
      }
      /// <summary>
      /// Login de Usuario
      /// </summary>
      /// <param name="usuarioAuthLoginDto">Usuario y Contraseña</param>
      /// <returns>Bearer Token</returns>
      [AllowAnonymous]
      [HttpPost("Login")]
      [ProducesResponseType(201, Type = typeof(List<UsuarioAuthLoginDto>))]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]

      public IActionResult Login(UsuarioAuthLoginDto usuarioAuthLoginDto)
      {
         var usuarioDesdeRepo = UsuarioRepository.Login(usuarioAuthLoginDto.Usuario, usuarioAuthLoginDto.Password);
         if (usuarioDesdeRepo == null)
         {
            return Unauthorized();
         }
         //   var claims = new[]
         //   {
         //      new Claim(ClaimTypes.NameIdentifier, usuarioDesdeRepo.IdUsuario.ToString()),
         //      new Claim(ClaimTypes.Name, usuarioDesdeRepo.UsuarioAcceso.ToString())
         //   };
         //   ///Generacion de Token
         //   var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
         //   var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
         //   var tokenDescriptor = new SecurityTokenDescriptor
         //   {
         //      Subject = new ClaimsIdentity(claims),
         //      Expires = DateTime.Now.AddDays(1),
         //      SigningCredentials = credenciales
         //   };
         //   var TokenHandler = new JwtSecurityTokenHandler();
         //   var token = TokenHandler.CreateToken(tokenDescriptor);
         //   return Ok(new
         //   {
         //      token = TokenHandler.WriteToken(token)
         //   });

         //}
         var claims = new[]
               {
            new Claim(ClaimTypes.NameIdentifier, usuarioDesdeRepo.IdUsuario.ToString()),
            new Claim(ClaimTypes.Name, usuarioDesdeRepo.UsuarioAcceso.ToString())
        };

         //Generación de token
         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
         
         var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

         var tokenDescriptor = new SecurityTokenDescriptor
         {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credenciales
         };

         var tokenHandler = new JwtSecurityTokenHandler();
         var token = tokenHandler.CreateToken(tokenDescriptor);

         return Ok(new
         {
            token = tokenHandler.WriteToken(token)
         });
      }


   }
}
