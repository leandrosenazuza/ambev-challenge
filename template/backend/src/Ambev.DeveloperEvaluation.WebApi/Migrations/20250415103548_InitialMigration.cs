using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ambev.DeveloperEvaluation.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    sale_number = table.Column<Guid>(type: "uuid", nullable: false),
                    sale_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    customer = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    total_sale_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    branch = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    is_cancelled = table.Column<bool>(type: "boolean", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.sale_number);
                });

            migrationBuilder.CreateTable(
                name: "SaleItems",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric", nullable: false),
                    discount = table.Column<decimal>(type: "numeric", nullable: false),
                    total_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    SaleNumber = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItems", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_SaleItems_Sales_SaleNumber",
                        column: x => x.SaleNumber,
                        principalTable: "Sales",
                        principalColumn: "sale_number");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SaleNumber",
                table: "SaleItems",
                column: "SaleNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleItems");

            migrationBuilder.DropTable(
                name: "Sales");
        }
    }
}
