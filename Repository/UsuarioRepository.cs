using ApiTiemposDeAlimentacion.Models;
using ApiTiemposDeAlimentacion.Repository.IRepository;
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
   public class UsuarioRepository : IUsuarioRepository
   {
      private DatabaseProviderFactory Factory;
      private Microsoft.Practices.EnterpriseLibrary.Data.Database Database;
      private StringBuilder stringBuilder;
      public UsuarioRepository()
      {
         Factory = new DatabaseProviderFactory();
         Database = Factory.Create("DefaultConnection");
      }
      public bool ExisteUsuario(string Usuario)
      {
         stringBuilder = new StringBuilder();
         stringBuilder.Append("SELECT  COUNT(1) FROM DbSistemas.dbo.TBLSECUSUARIOS WHERE USUARIO = @USUARIO");
         DbCommand commad = Database.GetSqlStringCommand(stringBuilder.ToString());
         Database.AddInParameter(commad, "USUARIO", System.Data.DbType.String, Usuario);
         if ( (int)Database.ExecuteScalar(commad) > 0)
         {
            return true;
         }
         else
         {
            return false;
         }

      }
      public Usuario GetUsuario(int IdUsuario)
      {
         //stringBuilder = new StringBuilder();
         //stringBuilder.Append("   SELECT IdUsuario,NumeroDeEmpleado,Usuario,Descripcion ,PasswordHash,PasswordSalt FROM TBLSECUSUARIOS WHERE IdUsuario = @IdUsuario");
         //DbCommand commad = Database.GetSqlStringCommand(stringBuilder.ToString());
         //Database.AddInParameter(commad, "USUARIO", System.Data.DbType.String, IdUsuario);
         //var usuario = Database.ExecuteSprocAccessor<Usuario>("UPSUsuarioByID", IdUsuario);
         //return usuario.SingleOrDefault();
         List<Usuario> usuarios = new List<Usuario>();
         stringBuilder = new StringBuilder();
         stringBuilder.AppendLine("SELECT IdUsuario,NumeroDeEmpleado,Usuario UsuarioAcceso,Descripcion ,PasswordHash ,PasswordSalt FROM DbSistemas.dbo.TBLSECUSUARIOS WHERE IdUsuario = @PIdUsuario ");
         DbCommand dbCommand = Database.GetSqlStringCommand(stringBuilder.ToString());
         Database.AddInParameter(dbCommand, "PIdUsuario", DbType.Int64, IdUsuario);
         using (IDataReader reader = Database.ExecuteReader(dbCommand))
         {
            while (reader.Read())
            {
               Usuario usuario = new Usuario();
               usuario.IdUsuario = (int)reader["IdUsuario"];
               usuario.NumeroDeEmpleado = reader["NumeroDeEmpleado"].ToString();
               usuario.UsuarioAcceso = reader["UsuarioAcceso"].ToString();
               usuario.Descripcion = reader["Descripcion"].ToString();
               usuario.PasswordHash = (byte[])reader["PasswordHash"];
               usuario.PasswordSalt = (byte[])reader["PasswordSalt"];
               usuarios.Add(usuario);
            }
         }
         return usuarios.FirstOrDefault();
      }

      private Usuario GetUsuarioByName(string Usuario)
      {
         List<Usuario> usuarios = new List<Usuario>();
         stringBuilder = new StringBuilder();
         stringBuilder.AppendLine("SELECT IdUsuario,NumeroDeEmpleado,Usuario UsuarioAcceso,Descripcion ,PasswordHash ,PasswordSalt FROM DbSistemas.dbo.TBLSECUSUARIOS WHERE Usuario = @PUsuario ");
         DbCommand dbCommand = Database.GetSqlStringCommand(stringBuilder.ToString());
         Database.AddInParameter(dbCommand, "PUsuario", DbType.String, Usuario);
         using (IDataReader reader = Database.ExecuteReader(dbCommand))
         {
            while (reader.Read())
            {
               Usuario usuario = new Usuario();
               usuario.IdUsuario = (int)reader["IdUsuario"];
               usuario.NumeroDeEmpleado = reader["NumeroDeEmpleado"].ToString();
               usuario.UsuarioAcceso = reader["UsuarioAcceso"].ToString();
               usuario.Descripcion = reader["Descripcion"].ToString();
               usuario.PasswordHash = (byte[])reader["PasswordHash"];
               usuario.PasswordSalt = (byte[])reader["PasswordSalt"];
               usuarios.Add(usuario);
            }
         }
         return usuarios.FirstOrDefault();
      }


      public ICollection<Usuario> GetUsuarios()
      {
         List<Usuario> usuarios = new List<Usuario>();
         //usuarios = Database.ExecuteSqlStringAccessor<Usuario>("SELECT IdUsuario,NumeroDeEmpleado,Usuario UsuarioAcceso,Descripcion ,PasswordHash,PasswordSalt FROM TBLSECUSUARIOS WHERE idUsuario>=306 ").ToList();
         using (IDataReader reader = Database.ExecuteReader(CommandType.Text, "SELECT IdUsuario,NumeroDeEmpleado,Usuario UsuarioAcceso,Descripcion ,PasswordHash,PasswordSalt FROM DbSistemas.dbo.TBLSECUSUARIOS WHERE idUsuario>=306 "))
         {
            while (reader.Read())
            {
               Usuario usuario = new Usuario();
               usuario.IdUsuario = (int)reader["IdUsuario"];
               usuario.NumeroDeEmpleado = reader["NumeroDeEmpleado"].ToString();
               usuario.UsuarioAcceso = reader["UsuarioAcceso"].ToString();
               usuario.Descripcion = reader["Descripcion"].ToString();
               usuario.PasswordHash =(byte[]) reader["PasswordHash"];
               usuario.PasswordSalt = (byte[])reader["PasswordSalt"];
               usuarios.Add(usuario);
            }

         }
         return usuarios;
          
      }

      public bool Guardar()
      {
         throw new NotImplementedException();
      }

      public Usuario Login(string usuario, string password)
      {
         //var listaUsuario = Database.ExecuteSprocAccessor<Usuario>("UPSUsuario", usuario);
         var user = GetUsuarioByName(usuario);
         if (user == null)
         {
            return null;
         }
         if (!VerificaPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
            return null;
            }

         return user;
      }

      public Usuario Registro(Usuario usuario, string password)
      {
         byte[] passwordHash, passwordSalt;
         CreatePasswordHash(password, out passwordHash, out passwordSalt);
         usuario.PasswordHash = passwordHash;
         usuario.PasswordSalt = passwordSalt;
         stringBuilder = new StringBuilder();
         Database.ExecuteNonQuery("DbSistemas.dbo.UPIUsuario", usuario.IdUsuario, usuario.NumeroDeEmpleado, usuario.UsuarioAcceso, usuario.Descripcion, usuario.PasswordHash, usuario.PasswordSalt);
         return usuario;
      }

      private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
      {
         using (System.Security.Cryptography.HMACSHA512 hMACSHA512 = new System.Security.Cryptography.HMACSHA512())
         {
            passwordSalt = hMACSHA512.Key;
            passwordHash = hMACSHA512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
         }
      }

      private bool VerificaPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
      {
         using (System.Security.Cryptography.HMACSHA512 hMACSHA512 = new System.Security.Cryptography.HMACSHA512(passwordSalt))
         {
            var hashComputo = hMACSHA512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < hashComputo.Length; i++)
            {
               if (hashComputo[i] != passwordHash[i]) return false;
            }
         }
         return true;
      }
   }
}
