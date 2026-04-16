using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeVuelosProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class PaymentMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    payment_method_type_id = table.Column<int>(type: "int", nullable: false),
                    card_type_id = table.Column<int>(type: "int", nullable: true),
                    card_issuer_id = table.Column<int>(type: "int", nullable: true),
                    display_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_methods", x => x.id);
                    table.CheckConstraint("CK_PaymentMethods_CardLogic", "(card_type_id IS NULL AND card_issuer_id IS NULL) OR (card_type_id IS NOT NULL AND card_issuer_id IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_payment_methods_card_issuers_card_issuer_id",
                        column: x => x.card_issuer_id,
                        principalTable: "card_issuers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payment_methods_card_types_card_type_id",
                        column: x => x.card_type_id,
                        principalTable: "card_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payment_methods_payment_method_types_payment_method_type_id",
                        column: x => x.payment_method_type_id,
                        principalTable: "payment_method_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_payment_methods_card_issuer_id",
                table: "payment_methods",
                column: "card_issuer_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_methods_card_type_id",
                table: "payment_methods",
                column: "card_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_methods_display_name",
                table: "payment_methods",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payment_methods_payment_method_type_id",
                table: "payment_methods",
                column: "payment_method_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_methods");
        }
    }
}
