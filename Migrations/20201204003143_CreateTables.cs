using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTiemposDeAlimentacion.Migrations
{
    public partial class CreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClsModuloHabitacional",
                columns: table => new
                {
                    ModuloHabitacional = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClsModuloHabitacional", x => x.ModuloHabitacional);
                });

            migrationBuilder.CreateTable(
                name: "TiemposDeAlimentacion",
                columns: table => new
                {
                    TiempoDeAlimentacionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiemposDeAlimentacion", x => x.TiempoDeAlimentacionID);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroDeEmpleado = table.Column<string>(nullable: true),
                    UsuarioAcceso = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClsModuloHabitacional");

            migrationBuilder.DropTable(
                name: "TiemposDeAlimentacion");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
