using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Models
{
   public class ModulosHabitacionales
   {
      public ModulosHabitacionales()
      {

      }
      public ModulosHabitacionales(string ModuloHabitacionalID)
      {
         ModuloHabitacional = ModuloHabitacionalID;
         GetModuloHabitacional(ModuloHabitacionalID);
      }

      public void GetModuloHabitacional(string ModuloHabitacionalID)
      {
         DatabaseProviderFactory databaseProviderFactory = new DatabaseProviderFactory();
         Microsoft.Practices.EnterpriseLibrary.Data.Database database = databaseProviderFactory.Create("DefaultConnection");
         StringBuilder stringBuilder = new StringBuilder();
         stringBuilder.AppendLine("SELECT  Nombre FROM TblModulosHabitacionales WHERE ModuloHabitacional=@PModuloHabitacional");
         DbCommand command = database.GetSqlStringCommand(stringBuilder.ToString());
         database.AddInParameter(command, "PModuloHabitacional",System.Data.DbType.String, ModuloHabitacionalID);
         Nombre = database.ExecuteScalar(command).ToString();
         
      }


      [Key]
      public string ModuloHabitacional { get; set; }
      public string Nombre { get; set; }
   }
}
