using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeVuelosProyectoFinal.Migrations
{
    public partial class BoardingPassesAlignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "boarding_gate",
                table: "flights",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "boarding_passes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    checkin_id = table.Column<int>(type: "int", nullable: false),
                    ticket_id = table.Column<int>(type: "int", nullable: false),
                    flight_id = table.Column<int>(type: "int", nullable: false),
                    gate = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    seat_code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    boarding_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boarding_passes", x => x.id);
                    table.ForeignKey(
                        name: "fk_boarding_pass_checkin",
                        column: x => x.checkin_id,
                        principalTable: "check_ins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_boarding_pass_flight",
                        column: x => x.flight_id,
                        principalTable: "flights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_boarding_pass_ticket",
                        column: x => x.ticket_id,
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_boarding_passes_checkin_id",
                table: "boarding_passes",
                column: "checkin_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_boarding_passes_code",
                table: "boarding_passes",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_boarding_passes_flight_id",
                table: "boarding_passes",
                column: "flight_id");

            migrationBuilder.CreateIndex(
                name: "IX_boarding_passes_ticket_id",
                table: "boarding_passes",
                column: "ticket_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "boarding_passes");

            migrationBuilder.DropColumn(
                name: "boarding_gate",
                table: "flights");
        }
    }
}
