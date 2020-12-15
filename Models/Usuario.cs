using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Models
{ //Publicacion de Usuario
   public class Usuario
   {
      [Key]
      public int IdUsuario { get; set; }
      public string NumeroDeEmpleado { get; set; }
      public string UsuarioAcceso { get; set; }
      public string Descripcion { get; set; }

      public byte[] PasswordHash { get; set; }
      public byte[] PasswordSalt { get; set; }
   }
}
