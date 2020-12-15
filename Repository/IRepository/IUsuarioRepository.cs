using ApiTiemposDeAlimentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Repository.IRepository
{
 public  interface IUsuarioRepository
   {
      ICollection<Usuario> GetUsuarios();
      Usuario GetUsuario(int IdUsuario);
      bool ExisteUsuario(string Usuario);
      Usuario Registro(Usuario usuario, string password);
      Usuario Login(string usuario, string password);

      bool Guardar();

   }
}
