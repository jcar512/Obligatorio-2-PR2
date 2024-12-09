using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obligatorio2.Migrations
    {
    public partial class addedPaises : Migration
        {
        protected override void Up(MigrationBuilder migrationBuilder)
            {
            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Hoteles");

            migrationBuilder.AddColumn<int>(
                name: "PaisId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "NombreHotel",
                table: "Hoteles",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Hoteles",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Ciudad",
                table: "Hoteles",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "PaisId",
                table: "Hoteles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                    {
                    PaisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePais = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.PaisId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PaisId",
                table: "Usuarios",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteles_PaisId",
                table: "Hoteles",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Paises_NombrePais",
                table: "Paises",
                column: "NombrePais",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hoteles_Paises_PaisId",
                table: "Hoteles",
                column: "PaisId",
                principalTable: "Paises",
                principalColumn: "PaisId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Paises_PaisId",
                table: "Usuarios",
                column: "PaisId",
                principalTable: "Paises",
                principalColumn: "PaisId");
            }

        protected override void Down(MigrationBuilder migrationBuilder)
            {
            migrationBuilder.DropForeignKey(
                name: "FK_Hoteles_Paises_PaisId",
                table: "Hoteles");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Paises_PaisId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_PaisId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Hoteles_PaisId",
                table: "Hoteles");

            migrationBuilder.DropColumn(
                name: "PaisId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PaisId",
                table: "Hoteles");

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "NombreHotel",
                table: "Hoteles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Hoteles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Ciudad",
                table: "Hoteles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Hoteles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
            }
        }
    }