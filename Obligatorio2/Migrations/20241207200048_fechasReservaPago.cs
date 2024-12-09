using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obligatorio2.Migrations
    {
    public partial class fechasReservaPago : Migration
        {
        protected override void Up(MigrationBuilder migrationBuilder)
            {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaReserva",
                table: "Reservas",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "Reservas",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "Reservas",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaPago",
                table: "Pagos",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
            }

        protected override void Down(MigrationBuilder migrationBuilder)
            {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaReserva",
                table: "Reservas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "Reservas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "Reservas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaPago",
                table: "Pagos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");
            }
        }
    }