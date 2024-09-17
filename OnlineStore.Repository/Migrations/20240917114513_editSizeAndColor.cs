using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Repository.Migrations
{
    /// <inheritdoc />
    public partial class editSizeAndColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "ProductVariants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "ProductVariants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecentageOfSales",
                table: "ProductVariants",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "((CAST([TotalQuantity] - [Quantity] AS DECIMAL) / [TotalQuantity]) * 100)",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "((CAST[TotalQuantity]-[Quantity] AS DECIMAL)/[TotalQuantity])*100");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Color",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecentageOfSales",
                table: "ProductVariants",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "((CAST[TotalQuantity]-[Quantity] AS DECIMAL)/[TotalQuantity])*100",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "((CAST([TotalQuantity] - [Quantity] AS DECIMAL) / [TotalQuantity]) * 100)");
        }
    }
}
