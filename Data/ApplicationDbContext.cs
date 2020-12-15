using ApiTiemposDeAlimentacion.Models;
using ApiTiemposDeAlimentacion.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Data
{
   public class ApplicationDbContext: DbContext
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
      {

      }
      public DbSet<ModulosHabitacionales> ClsModuloHabitacional { get; set; }
      public DbSet<TiemposDeAlimentacion> TiemposDeAlimentacion { get; set; }
      public DbSet<Usuario> Usuario { get; set; }
   }
}
