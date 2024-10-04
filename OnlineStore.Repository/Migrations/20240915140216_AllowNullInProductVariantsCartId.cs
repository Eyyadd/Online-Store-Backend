using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullInProductVariantsCartId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_CartItems_CartItemsId",
                table: "ProductVariants");

            migrationBuilder.AlterColumn<int>(
                name: "CartItemsId",
                table: "ProductVariants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_CartItems_CartItemsId",
                table: "ProductVariants",
                column: "CartItemsId",
                principalTable: "CartItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_CartItems_CartItemsId",
                table: "ProductVariants");

            migrationBuilder.AlterColumn<int>(
                name: "CartItemsId",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_CartItems_CartItemsId",
                table: "ProductVariants",
                column: "CartItemsId",
                principalTable: "CartItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
