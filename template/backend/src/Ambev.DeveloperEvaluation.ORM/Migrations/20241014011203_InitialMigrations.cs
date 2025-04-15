using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
               name: "Sales",
               columns: table => new
               {
                   SaleNumber = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                   SaleDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                   Customer = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                   TotalSaleAmount = table.Column<decimal>(type: "numeric", nullable: false),
                   Branch = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                   IsCancelled = table.Column<bool>(type: "boolean", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Sales", x => x.SaleNumber);
               });

            migrationBuilder.CreateTable(
                name: "SaleItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    SaleNumber = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleItems_Sales_SaleNumber",
                        column: x => x.SaleNumber,
                        principalTable: "Sales",
                        principalColumn: "SaleNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SaleNumber",
                table: "SaleItems",
                column: "SaleNumber");


            migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                description = table.Column<string>(type: "TEXT", nullable: true),
                category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                rating_rate = table.Column<decimal>(type: "numeric(3,2)", precision: 3, scale: 2, nullable: false, defaultValue: 0m),
                rating_count = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.id);
            });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
            migrationBuilder.DropTable(
              name: "Products");
            migrationBuilder.DropTable(
               name: "SaleItems");
            migrationBuilder.DropTable(
                name: "Sales");
        }
    }
}
