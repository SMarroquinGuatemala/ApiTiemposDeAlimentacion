using ApiTiemposDeAlimentacion.Models;
using ApiTiemposDeAlimentacion.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTiemposDeAlimentacion.Repository
{
   public class PersonalTiemposDeAlimentacionRepository : IPersonalTiemposDeAlimentacionRepository

   {
      private  DatabaseProviderFactory Factory;
      private Microsoft.Practices.EnterpriseLibrary.Data.Database Database;
      private StringBuilder stringBuilder;

      public PersonalTiemposDeAlimentacionRepository()
      {
         Factory = new DatabaseProviderFactory();
         Database = Factory.Create("DefaultConnection");
         
      }
      public bool ActualizarPersonalTiemposDeAlimentacion(PersonalTiemposDeAlimentacion personalTiemposDeAlimentacion)
      {
         stringBuilder = new StringBuilder();
         stringBuilder.AppendLine("UPDATE TblPersonalTiemposDeAlimentacion SET NumeroDeEmpleado=@NumeroDeEmpleado,Fecha=@Fecha,TiemposDeAlimentacionID=@TiemposDeAlimentacionID,ModuloHabitacional=@ModuloHabitacional");
         stringBuilder.AppendLine(" WHERE PersonalTiemposDeAlimentacionID=@PersonalTiemposDeAlimentacionID");
         DbCommand cmd = Database.GetSqlStringCommand(stringBuilder.ToString());
         Database.AddInParameter(cmd, "NumeroDeEmpleado", DbType.String, personalTiemposDeAlimentacion.NumeroDeEmpleado);
         Database.AddInParameter(cmd, "Fecha", DbType.DateTime, personalTiemposDeAlimentacion.Fecha);
         Database.AddInParameter(cmd, "TiemposDeAlimentacionID", DbType.Int16, personalTiemposDeAlimentacion.TiemposDeAlimentacionID);
         Database.AddInParameter(cmd, "ModuloHabitacional", DbType.Int16, personalTiemposDeAlimentacion.ModuloHabitacional);
         Database.AddInParameter(cmd, "PersonalTiemposDeAlimentacionID", DbType.Int32, personalTiemposDeAlimentacion.PersonalTiemposDeAlimentacionID);
         if (Database.ExecuteNonQuery(cmd) > 0)
         {
            return true;
         }
         else
         {
            return false;
         }
      }

      public bool BorrarPersonalTiemposDeAlimentacion(int PersonalTiemposDeAlimentacionID)
      {
         stringBuilder = new StringBuilder();
         stringBuilder.AppendLine("DELETE TblPersonalTiemposDeAlimentacion WHERE PersonalTiemposDeAlimentacionID=@PersonalTiemposDeAlimentacionID");
         DbCommand cmd = Database.GetSqlStringCommand(stringBuilder.ToString());
         
         Database.AddInParameter(cmd, "PersonalTiemposDeAlimentacionID", DbType.String, PersonalTiemposDeAlimentacionID);
        
         if (Database.ExecuteNonQuery(cmd) > 0)
         {
            return true;
         }
         else
         {
            return false;
         }
      }

      public IEnumerable<PersonalTiemposDeAlimentacion> BuscarPersonalTiempos(string Nombre)
      {
         throw new NotImplementedException();
      }

      public bool CrearPersonalTiemposDeAlimentacion(PersonalTiemposDeAlimentacion personalTiemposDeAlimentacion)
      {
         //DatabaseProviderFactory databaseProviderFactory = new DatabaseProviderFactory();
         //Microsoft.Practices.EnterpriseLibrary.Data.Database database = databaseProviderFactory.Create("DefaultConnection");
         stringBuilder = new StringBuilder();
         stringBuilder.AppendLine("INSERT TblPersonalTiemposDeAlimentacion(NumeroDeEmpleado,Fecha,TiemposDeAlimentacionID,ModuloHabitacional) VALUES(@NumeroDeEmpleado,@Fecha,@TiemposDeAlimentacionID,@ModuloHabitacional)");
         DbCommand cmd = Database.GetSqlStringCommand(stringBuilder.ToString());
         Database.AddInParameter(cmd, "NumeroDeEmpleado", DbType.String, personalTiemposDeAlimentacion.NumeroDeEmpleado);
         Database.AddInParameter(cmd, "Fecha", DbType.DateTime, personalTiemposDeAlimentacion.Fecha);
         Database.AddInParameter(cmd, "TiemposDeAlimentacionID", DbType.Int16, personalTiemposDeAlimentacion.TiemposDeAlimentacionID);
         Database.AddInParameter(cmd, "ModuloHabitacional", DbType.Int16, personalTiemposDeAlimentacion.ModuloHabitacional);
         if (Database.ExecuteNonQuery(cmd) > 0)
         {
            return true;
         }
         else
         {
            return false;
         }
      }

      public bool ExistePersonalTiemposDeAlimentacion(string NumeroDeEmpleado, DateTime Fecha, int TiemposDeAlimentacionID)
      {
         stringBuilder = new StringBuilder();
         stringBuilder.AppendLine("SELECT COUNT(1) FROM TblPersonalTiemposDeAlimentacion WHERE NumeroDeEmpleado=@NumeroDeEmpleado and Fecha=@Fecha and TiemposDeAlimentacionID=@TiemposDeAlimentacionID");
         DbCommand cmd = Database.GetSqlStringCommand(stringBuilder.ToString());
         Database.AddInParameter(cmd, "NumeroDeEmpleado", DbType.String, NumeroDeEmpleado);
         Database.AddInParameter(cmd, "Fecha", DbType.DateTime, Fecha);
         Database.AddInParameter(cmd, "TiemposDeAlimentacionID", DbType.Int16, TiemposDeAlimentacionID);
         if ( (int)Database.ExecuteScalar(cmd) > 0)
         {
            return true;
         }
         else
         {
            return false;
         }
      }

      public PersonalTiemposDeAlimentacion GetPersonalTiempo(string NumeroDeEmpleado, DateTime? Fecha = null, int? TiemposDeAlimentacionID = null)
      {
         throw new NotImplementedException();
      }

       ICollection<PersonalTiemposDeAlimentacion>  IPersonalTiemposDeAlimentacionRepository.GetPersonalTiempos()
      {
         List<PersonalTiemposDeAlimentacion> personalTiempos = new List<PersonalTiemposDeAlimentacion>();
         //DatabaseProviderFactory databaseProviderFactory = new DatabaseProviderFactory();
         //Microsoft.Practices.EnterpriseLibrary.Data.Database database = databaseProviderFactory.Create("DefaultConnection");
          stringBuilder = new StringBuilder();
         stringBuilder.AppendLine("SELECT  A.PersonalTiemposDeAlimentacionID,ltrim(rtrim(A.NumeroDeEmpleado))NumeroDeEmpleado,CAST(A.Fecha AS DATE)Fecha,A.TiemposDeAlimentacionID, ");
         stringBuilder.AppendLine("A.ModuloHabitacional FROM TblPersonalTiemposDeAlimentacion A");
         
         using (IDataReader reader = Database.ExecuteReader(CommandType.Text,stringBuilder.ToString() ))
         {
            while (reader.Read())
            {
               PersonalTiemposDeAlimentacion personal = new PersonalTiemposDeAlimentacion();
               
               
               personal.PersonalTiemposDeAlimentacionID = (int)reader["PersonalTiemposDeAlimentacionID"];
               personal.NumeroDeEmpleado = reader["NumeroDeEmpleado"].ToString();
               personal.Fecha =(DateTime) reader["Fecha"];
               
               personal.TiemposDeAlimentacionID = (int)reader["TiemposDeAlimentacionID"];
               TiemposDeAlimentacion tiemposDeAlimentacion = new TiemposDeAlimentacion(personal.TiemposDeAlimentacionID);
               personal.TiemposDeAlimentacion = tiemposDeAlimentacion;               
               
               personal.ModuloHabitacional = reader["ModuloHabitacional"].ToString();
               ModulosHabitacionales modulosHabitacionales = new ModulosHabitacionales(personal.ModuloHabitacional);
               personal.ModulosHabitacionales = modulosHabitacionales;
               personalTiempos.Add(personal);
            }
         }
         return personalTiempos;
      }

      public bool Guardar()
      {
         throw new NotImplementedException();
      }

      public PersonalTiemposDeAlimentacion GetPersonalTiempoByID(int PersonalTiemposDeAlimentacionID)
      {

         PersonalTiemposDeAlimentacion personal = new PersonalTiemposDeAlimentacion();

         stringBuilder = new StringBuilder();
         stringBuilder.AppendLine("SELECT  A.PersonalTiemposDeAlimentacionID,ltrim(rtrim(A.NumeroDeEmpleado))NumeroDeEmpleado,CAST(A.Fecha AS DATE)Fecha,A.TiemposDeAlimentacionID, ");
         stringBuilder.AppendLine("A.ModuloHabitacional FROM TblPersonalTiemposDeAlimentacion A WHERE A.PersonalTiemposDeAlimentacionID=@PersonalTiemposDeAlimentacionID");
         DbCommand cmd = Database.GetSqlStringCommand(stringBuilder.ToString());
         Database.AddInParameter(cmd, "PersonalTiemposDeAlimentacionID", DbType.String, PersonalTiemposDeAlimentacionID);
         
         using (IDataReader reader = Database.ExecuteReader(cmd))
         {
            while (reader.Read())
           {

               personal.PersonalTiemposDeAlimentacionID = (int)reader["PersonalTiemposDeAlimentacionID"];
               personal.NumeroDeEmpleado = reader["NumeroDeEmpleado"].ToString();
               personal.Fecha = (DateTime)reader["Fecha"];

               personal.TiemposDeAlimentacionID = (int)reader["TiemposDeAlimentacionID"];
               TiemposDeAlimentacion tiemposDeAlimentacion = new TiemposDeAlimentacion(personal.TiemposDeAlimentacionID);
               personal.TiemposDeAlimentacion = tiemposDeAlimentacion;

               personal.ModuloHabitacional = reader["ModuloHabitacional"].ToString();
               ModulosHabitacionales modulosHabitacionales = new ModulosHabitacionales(personal.ModuloHabitacional);
               personal.ModulosHabitacionales = modulosHabitacionales;
              
            }
         }
         return personal;
      }
   }
}
