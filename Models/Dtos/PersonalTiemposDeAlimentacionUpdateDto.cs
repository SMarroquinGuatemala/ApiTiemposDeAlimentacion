using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Models.Dtos
{
   public class PersonalTiemposDeAlimentacionUpdateDto
   {
      public int PersonalTiemposDeAlimentacionID { get; set; }

      [Required(ErrorMessage = "El campo numero de empleado es obligatorio")]
      public string NumeroDeEmpleado { get; set; }

      [Required(ErrorMessage = "El campo fecha es obligatorio")]
      public DateTime Fecha { get; set; }

      [Required(ErrorMessage = "El campo tiempo de alimentacion es obligatorio")]
      public int TiemposDeAlimentacionID { get; set; }

      [Required(ErrorMessage = "El campo modulo habitacional es obligatorio")]
      public string ModuloHabitacional { get; set; }
   }
}
