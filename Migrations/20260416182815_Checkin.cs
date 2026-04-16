using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeVuelosProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class Checkin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "checkins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ticket_id = table.Column<int>(type: "int", nullable: false),
                    staff_id = table.Column<int>(type: "int", nullable: false),
                    flight_seat_id = table.Column<int>(type: "int", nullable: false),
                    checkin_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    checkin_status_id = table.Column<int>(type: "int", nullable: false),
                    boarding_pass_number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    has_checked_baggage = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    baggage_weight_kg = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkins", x => x.id);
                    table.CheckConstraint("CK_Checkins_Logic", "(has_checked_baggage = 0 AND baggage_weight_kg IS NULL) OR (has_checked_baggage = 1 AND baggage_weight_kg >= 0)");
                    table.CheckConstraint("CK_Checkins_Weight", "baggage_weight_kg IS NULL OR baggage_weight_kg >= 0");
                    table.ForeignKey(
                        name: "FK_checkins_checkin_statuses_checkin_status_id",
                        column: x => x.checkin_status_id,
                        principalTable: "checkin_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_checkins_flight_seats_flight_seat_id",
                        column: x => x.flight_seat_id,
                        principalTable: "flight_seats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_checkins_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_checkins_boarding_pass_number",
                table: "checkins",
                column: "boarding_pass_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_checkins_checkin_status_id",
                table: "checkins",
                column: "checkin_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_checkins_flight_seat_id",
                table: "checkins",
                column: "flight_seat_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_checkins_ticket_id",
                table: "checkins",
                column: "ticket_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkins");
        }
    }
}
