using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeVuelosProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class FlightAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "flight_assignments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    flight_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    staff_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    flight_role_id = table.Column<int>(type: "int", nullable: false),
                    FlightRolesEntityId = table.Column<int>(type: "int", nullable: true),
                    FlightsEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flight_assignments", x => x.id);
                    table.ForeignKey(
                        name: "FK_flight_assignments_flight_roles_FlightRolesEntityId",
                        column: x => x.FlightRolesEntityId,
                        principalTable: "flight_roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_flight_assignments_flight_roles_flight_role_id",
                        column: x => x.flight_role_id,
                        principalTable: "flight_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_flight_assignments_flights_FlightsEntityId",
                        column: x => x.FlightsEntityId,
                        principalTable: "flights",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_flight_assignments_flights_flight_id",
                        column: x => x.flight_id,
                        principalTable: "flights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_flight_assignments_flight_id_staff_id",
                table: "flight_assignments",
                columns: new[] { "flight_id", "staff_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flight_assignments_flight_role_id",
                table: "flight_assignments",
                column: "flight_role_id");

            migrationBuilder.CreateIndex(
                name: "IX_flight_assignments_FlightRolesEntityId",
                table: "flight_assignments",
                column: "FlightRolesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_flight_assignments_FlightsEntityId",
                table: "flight_assignments",
                column: "FlightsEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flight_assignments");
        }
    }
}
