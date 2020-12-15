using ApiTiemposDeAlimentacion.Data;
using ApiTiemposDeAlimentacion.Models;
using ApiTiemposDeAlimentacion.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Repository
{
   public class ModuloHabitacionalRepository : IModuloHabitacionalRepository

   {
      private readonly ApplicationDbContext _db;
      private readonly IConfiguration _configuration;
      public ModuloHabitacionalRepository(ApplicationDbContext db, IConfiguration configuration)
      {
         _db = db;
         _configuration = configuration;
      }

     

      ICollection<ModulosHabitacionales> IModuloHabitacionalRepository.GetModulosHabitacionales()
      {
         List<ModulosHabitacionales> clsModuloHabitacionals = new List<ModulosHabitacionales>();
         DatabaseProviderFactory databaseProviderFactory = new DatabaseProviderFactory();
         Microsoft.Practices.EnterpriseLibrary.Data.Database database = databaseProviderFactory.Create("DefaultConnection");
         using (IDataReader reader = database.ExecuteReader(CommandType.Text, "SELECT  ModuloHabitacional,Nombre  FROM TblModulosHabitacionales WHERE ModuloHabitacional>0"))
         {
            while (reader.Read())
            {
               ModulosHabitacionales ModuloHabitacional = new ModulosHabitacionales();
               ModuloHabitacional.ModuloHabitacional = reader["ModuloHabitacional"].ToString();
               ModuloHabitacional.Nombre = reader["Nombre"].ToString();
               clsModuloHabitacionals.Add(ModuloHabitacional);
            }
         }
      return clsModuloHabitacionals;      
      }
   }
}

//DatabaseProviderFactory dataBaseProviderFactory = new DatabaseProviderFactory();
//Database namedDB = dataBaseProviderFactory.Create("DefaultConnection");
//Microsoft.Practices.EnterpriseLibrary.Data.Database namedDB = dataBaseProviderFactory.Create(_configuration.GetSection("Data").GetSection("ConnectionString").Value.ToString());
//string connection = "Data Source=TFSSDFCA2;Initial Catalog=DbRecursosHumanos;Integrated Security=True";
//Database nameDB = dataBaseProviderFactory.Create("Desarrollo");

//DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(new SystemConfigurationSource(false).GetSection), false);
//Database db = dataBaseProviderFactory.Create(ConfigurationManager.ConnectionStrings["Desarrollo"].ConnectionString.ToString());

//using (SqlConnection sqlConnection = new SqlConnection(connection))
//{
//   using (SqlCommand sqlCommand = new SqlCommand("SELECT  ModuloHabitacional,Nombre  FROM TblModulosHabitacionales WHERE ModuloHabitacional>0"))
//   {
//      using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
//      {
//         sqlCommand.Connection = sqlConnection;
//         sqlConnection.Open();
//         sqlDataAdapter.SelectCommand = sqlCommand;
//         SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
//         while (sqlDataReader.Read())
//         {
//            ClsModuloHabitacional ModuloHabitacional = new ClsModuloHabitacional();
//            ModuloHabitacional.ModuloHabitacional = sqlDataReader["ModuloHabitacional"].ToString();
//            ModuloHabitacional.Nombre = sqlDataReader["Nombre"].ToString();
//            clsModuloHabitacionals.Add(ModuloHabitacional);

//         }
//      }
//   }
//}
//return _db.ClsModuloHabitacional.OrderBy(mh => mh.ModuloHabitacional).ToList();
