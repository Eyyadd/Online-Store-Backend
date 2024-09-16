using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Edite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PrecentageOfSales",
                table: "ProductVariants",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "((CAST([TotalQuantity] - [Quantity] AS DECIMAL) / [TotalQuantity]) * 100)",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "(([TotalQuantity]-[Quantity])/[TotalQuantity])*100");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PrecentageOfSales",
                table: "ProductVariants",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "(([TotalQuantity]-[Quantity])/[TotalQuantity])*100",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "((CAST[TotalQuantity]-[Quantity] AS DECIMAL)/[TotalQuantity])*100");
        }
    }
}
