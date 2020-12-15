using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTiemposDeAlimentacion.Migrations
{
    public partial class MigrationInicial : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClsModuloHabitacional");
        }
    }
}
