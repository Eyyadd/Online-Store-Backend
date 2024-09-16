using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Repository.Migrations
{
    /// <inheritdoc />
    public partial class EditTableProductVariants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalQuantity",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecentageOfSales",
                table: "ProductVariants",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "(([TotalQuantity]-[Quantity])/[TotalQuantity])*100");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecentageOfSales",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "ProductVariants");
        }
    }
}
