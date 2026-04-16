using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeVuelosProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class ReservationStateTransitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reservation_state_transitions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    from_status_id = table.Column<int>(type: "int", nullable: false),
                    to_status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation_state_transitions", x => x.id);
                    table.CheckConstraint("CK_ReservationStatusTransitions_NoSelf", "from_status_id <> to_status_id");
                    table.ForeignKey(
                        name: "FK_reservation_state_transitions_ReserveStates_from_status_id",
                        column: x => x.from_status_id,
                        principalTable: "ReserveStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_reservation_state_transitions_ReserveStates_to_status_id",
                        column: x => x.to_status_id,
                        principalTable: "ReserveStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_state_transitions_from_status_id_to_status_id",
                table: "reservation_state_transitions",
                columns: new[] { "from_status_id", "to_status_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reservation_state_transitions_to_status_id",
                table: "reservation_state_transitions",
                column: "to_status_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservation_state_transitions");
        }
    }
}
