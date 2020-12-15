using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Models.Dtos
{
   public class UsuarioDto
   {
    
    
      public string UsuarioAcceso { get; set; }
     
      public byte[] PasswordHash { get; set; }
    
   }
}
