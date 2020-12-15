using ApiTiemposDeAlimentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Repository.IRepository
{
   public interface IModuloHabitacionalRepository
   {
      ICollection<ModulosHabitacionales> GetModulosHabitacionales();
      
   }
}
