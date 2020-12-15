using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Models.Dtos
{
   public class UsuarioAuthDto
   {
      [Key]
      public int IdUsuario { get; set; }
      
      [Required(ErrorMessage ="El numero de empleado es obligatorio")]
      public string NumeroDeEmpleado { get; set; }
      
      [Required(ErrorMessage = "El usuario es obligatorio")]
      public string Usuario { get; set; }
      
      [Required(ErrorMessage = "El password es obligatorio")]
      [StringLength(12, MinimumLength = 8, ErrorMessage = "El password debe de contener entre 8 y 12 caracteres")]
      public string Password { get; set; }

   }
}
