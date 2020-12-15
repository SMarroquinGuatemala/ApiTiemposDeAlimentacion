using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Models.Dtos
{
   public class ModuloHabitacionalDto
   {
      [Key]
      public string ModuloHabitacional { get; set; }
      public string Nombre { get; set; }
   }
}
