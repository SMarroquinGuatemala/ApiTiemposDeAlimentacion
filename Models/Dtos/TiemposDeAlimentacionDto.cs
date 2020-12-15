using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Models.Dtos
{
   public class TiemposDeAlimentacionDto
   {
      [Key]
      public int TiempoDeAlimentacionID { get; set; }
      public string Nombre { get; set; }

   }
}
