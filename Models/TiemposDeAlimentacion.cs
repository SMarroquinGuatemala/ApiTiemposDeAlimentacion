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
   public class TiemposDeAlimentacion
   {
     
      public TiemposDeAlimentacion()
      {

      }

      public TiemposDeAlimentacion(int TiempoDeAlimentacion)
      {
         TiempoDeAlimentacionID = TiempoDeAlimentacion;
         GetTiemposDeAlimentacion(TiempoDeAlimentacionID);
      }

      public void GetTiemposDeAlimentacion(int TiemposDeAlimentacionID)
      {
         DatabaseProviderFactory databaseProviderFactory = new DatabaseProviderFactory();
         Microsoft.Practices.EnterpriseLibrary.Data.Database database = databaseProviderFactory.Create("DefaultConnection");
         StringBuilder stringBuilder = new StringBuilder();
         stringBuilder.AppendLine("SELECT  Nombre FROM TblTiemposDeAlimentacion WHERE TiemposDeAlimentacionID=@PTiemposDeAlimentacionID");
         DbCommand command = database.GetSqlStringCommand(stringBuilder.ToString());
         database.AddInParameter(command, "PTiemposDeAlimentacionID", System.Data.DbType.String, TiemposDeAlimentacionID);
         Nombre = database.ExecuteScalar(command).ToString();

      }
      [Key]

      public int TiempoDeAlimentacionID { get; set; }
      public string Nombre { get; set; }


   }
}
