using ApiTiemposDeAlimentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Repository.IRepository
{
  public interface IPersonalTiemposDeAlimentacionRepository
   {
      ICollection<PersonalTiemposDeAlimentacion> GetPersonalTiempos();

      IEnumerable<PersonalTiemposDeAlimentacion> BuscarPersonalTiempos(string Nombre);
      PersonalTiemposDeAlimentacion GetPersonalTiempo(string NumeroDeEmpleado, DateTime? Fecha =null, int? TiemposDeAlimentacionID  = null);
      PersonalTiemposDeAlimentacion GetPersonalTiempoByID(int PersonalTiemposDeAlimentacionID);
      bool ExistePersonalTiemposDeAlimentacion(string NumeroDeEmpleado, DateTime Fecha, int TiemposDeAlimentacionID);
      bool CrearPersonalTiemposDeAlimentacion(PersonalTiemposDeAlimentacion personalTiemposDeAlimentacion);
      bool ActualizarPersonalTiemposDeAlimentacion(PersonalTiemposDeAlimentacion personalTiemposDeAlimentacion);
      bool BorrarPersonalTiemposDeAlimentacion(int PersonalTiemposDeAlimentacionID);
      bool Guardar();


   }
}
