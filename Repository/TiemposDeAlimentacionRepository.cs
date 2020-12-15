using ApiTiemposDeAlimentacion.Data;
using ApiTiemposDeAlimentacion.Models;
using ApiTiemposDeAlimentacion.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Repository
{
   public class TiemposDeAlimentacionRepository : ITiemposDeAlimentacionRepository
   {
      private readonly ApplicationDbContext _db;
      private readonly IConfiguration _configuration;
      public TiemposDeAlimentacionRepository(ApplicationDbContext db, IConfiguration configuration)
      {
         _db = db;
         _configuration = configuration;
      }
      //public ICollection<TiemposDeAlimentacion> GetTiempos()
      ICollection<TiemposDeAlimentacion> ITiemposDeAlimentacionRepository.GetTiempos()
      {
         //throw new NotImplementedException();

         List<TiemposDeAlimentacion> tiemposDeAlimentacions = new List<TiemposDeAlimentacion>();
         DatabaseProviderFactory databaseProviderFactory = new DatabaseProviderFactory();
         Microsoft.Practices.EnterpriseLibrary.Data.Database database = databaseProviderFactory.Create("DefaultConnection");
         using (IDataReader reader = database.ExecuteReader(CommandType.Text, "SELECT  TiemposDeAlimentacionID,Nombre FROM  TblTiemposDeAlimentacion --WHERE  CAST(GETDATE() AS TIME) BETWEEN HoraInicial AND HoraFinal "))
         {
            while (reader.Read())
            {
               TiemposDeAlimentacion tiempos = new TiemposDeAlimentacion();
               tiempos.TiempoDeAlimentacionID = (int)reader["TiemposDeAlimentacionID"];
               tiempos.Nombre = reader["Nombre"].ToString();
               tiemposDeAlimentacions.Add(tiempos);
            }
         }
         return tiemposDeAlimentacions;
      }
   }
}
