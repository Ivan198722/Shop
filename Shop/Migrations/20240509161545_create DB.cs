using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class createDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CellphoneProperties_Categories_categoryId",
                table: "CellphoneProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoProperties_Categories_categoryId",
                table: "PhotoProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_TVProperties_Categories_categoryId",
                table: "TVProperties");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "TVProperties",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TVProperties_categoryId",
                table: "TVProperties",
                newName: "IX_TVProperties_CategoryId");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "PhotoProperties",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoProperties_categoryId",
                table: "PhotoProperties",
                newName: "IX_PhotoProperties_CategoryId");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "CellphoneProperties",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CellphoneProperties_categoryId",
                table: "CellphoneProperties",
                newName: "IX_CellphoneProperties_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "TVProperties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "TVProperties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "PhotoProperties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "PhotoProperties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CellphoneProperties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "CellphoneProperties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TVProperties_productId",
                table: "TVProperties",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoProperties_productId",
                table: "PhotoProperties",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_CellphoneProperties_productId",
                table: "CellphoneProperties",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_CellphoneProperties_Categories_CategoryId",
                table: "CellphoneProperties",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CellphoneProperties_Products_productId",
                table: "CellphoneProperties",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoProperties_Categories_CategoryId",
                table: "PhotoProperties",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoProperties_Products_productId",
                table: "PhotoProperties",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TVProperties_Categories_CategoryId",
                table: "TVProperties",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TVProperties_Products_productId",
                table: "TVProperties",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CellphoneProperties_Categories_CategoryId",
                table: "CellphoneProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_CellphoneProperties_Products_productId",
                table: "CellphoneProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoProperties_Categories_CategoryId",
                table: "PhotoProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoProperties_Products_productId",
                table: "PhotoProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_TVProperties_Categories_CategoryId",
                table: "TVProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_TVProperties_Products_productId",
                table: "TVProperties");

            migrationBuilder.DropIndex(
                name: "IX_TVProperties_productId",
                table: "TVProperties");

            migrationBuilder.DropIndex(
                name: "IX_PhotoProperties_productId",
                table: "PhotoProperties");

            migrationBuilder.DropIndex(
                name: "IX_CellphoneProperties_productId",
                table: "CellphoneProperties");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "TVProperties");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "PhotoProperties");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "CellphoneProperties");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "TVProperties",
                newName: "categoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TVProperties_CategoryId",
                table: "TVProperties",
                newName: "IX_TVProperties_categoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "PhotoProperties",
                newName: "categoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoProperties_CategoryId",
                table: "PhotoProperties",
                newName: "IX_PhotoProperties_categoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "CellphoneProperties",
                newName: "categoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CellphoneProperties_CategoryId",
                table: "CellphoneProperties",
                newName: "IX_CellphoneProperties_categoryId");

            migrationBuilder.AlterColumn<int>(
                name: "categoryId",
                table: "TVProperties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "categoryId",
                table: "PhotoProperties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "categoryId",
                table: "CellphoneProperties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CellphoneProperties_Categories_categoryId",
                table: "CellphoneProperties",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoProperties_Categories_categoryId",
                table: "PhotoProperties",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TVProperties_Categories_categoryId",
                table: "TVProperties",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
