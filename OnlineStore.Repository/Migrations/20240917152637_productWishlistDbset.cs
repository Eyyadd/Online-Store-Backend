using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Repository.Migrations
{
    /// <inheritdoc />
    public partial class productWishlistDbset : Migration
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
                oldComputedColumnSql: "((CAST[TotalQuantity]-[Quantity] AS DECIMAL)/[TotalQuantity])*100");

            migrationBuilder.CreateTable(
                name: "ProductWishlist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    wishlistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWishlist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductWishlist_ProductVariants_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductWishlist_Wishlist_wishlistId",
                        column: x => x.wishlistId,
                        principalTable: "Wishlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductWishlist_ProductId",
                table: "ProductWishlist",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWishlist_wishlistId",
                table: "ProductWishlist",
                column: "wishlistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductWishlist");

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
