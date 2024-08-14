using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class newShopCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopCartItems_Products_ProductId",
                table: "ShopCartItems");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ShopCartItems",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopCartItems_ProductId",
                table: "ShopCartItems",
                newName: "IX_ShopCartItems_productId");

            migrationBuilder.AlterColumn<string>(
                name: "ShopCartId",
                table: "ShopCartItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "imagesId",
                table: "ShopCartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShopCartItems_imagesId",
                table: "ShopCartItems",
                column: "imagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopCartItems_Images_imagesId",
                table: "ShopCartItems",
                column: "imagesId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopCartItems_Products_productId",
                table: "ShopCartItems",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopCartItems_Images_imagesId",
                table: "ShopCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopCartItems_Products_productId",
                table: "ShopCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ShopCartItems_imagesId",
                table: "ShopCartItems");

            migrationBuilder.DropColumn(
                name: "imagesId",
                table: "ShopCartItems");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "ShopCartItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopCartItems_productId",
                table: "ShopCartItems",
                newName: "IX_ShopCartItems_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "ShopCartId",
                table: "ShopCartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopCartItems_Products_ProductId",
                table: "ShopCartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
