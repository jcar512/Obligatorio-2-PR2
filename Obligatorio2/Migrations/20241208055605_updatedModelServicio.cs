using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obligatorio2.Migrations
    {
    public partial class updatedModelServicio : Migration
        {
        protected override void Up(MigrationBuilder migrationBuilder)
            {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Servicios",
                newName: "NombreServicio");

            migrationBuilder.AlterColumn<string>(
                name: "TipoDocumento",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Usuarios",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Usuarios",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
            }

        protected override void Down(MigrationBuilder migrationBuilder)
            {
            migrationBuilder.RenameColumn(
                name: "NombreServicio",
                table: "Servicios",
                newName: "Nombre");

            migrationBuilder.AlterColumn<string>(
                name: "TipoDocumento",
                table: "Usuarios",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);
            }
        }
    }