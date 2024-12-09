using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obligatorio2.Migrations
    {
    public partial class removedFechas : Migration
        {
        protected override void Up(MigrationBuilder migrationBuilder)
            {
            migrationBuilder.DropTable(
                name: "FechasReservadas");
            }

        protected override void Down(MigrationBuilder migrationBuilder)
            {
            migrationBuilder.CreateTable(
                name: "FechasReservadas",
                columns: table => new
                    {
                    FechaReservaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservaId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FechasReservadas", x => x.FechaReservaId);
                    table.ForeignKey(
                        name: "FK_FechasReservadas_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "ReservaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FechasReservadas_ReservaId",
                table: "FechasReservadas",
                column: "ReservaId");
            }
        }
    }