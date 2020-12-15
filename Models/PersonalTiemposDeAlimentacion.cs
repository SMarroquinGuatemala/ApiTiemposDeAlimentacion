using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Models
{
   public class PersonalTiemposDeAlimentacion
   {
      [Key]
      public int PersonalTiemposDeAlimentacionID { get; set; }
      public string NumeroDeEmpleado { get; set; }
      public DateTime Fecha { get; set; }
      public int TiemposDeAlimentacionID { get; set; }

      public TiemposDeAlimentacion TiemposDeAlimentacion { get; set; }
      public string ModuloHabitacional { get; set; }

      public ModulosHabitacionales ModulosHabitacionales { get; set; }
   }
}
