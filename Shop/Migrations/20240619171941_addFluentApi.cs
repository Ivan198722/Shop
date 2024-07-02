using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class addFluentApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperties_Categories_categoryId",
                table: "ProductProperties");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProperties_Categories_categoryId",
                table: "ProductProperties",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperties_Categories_categoryId",
                table: "ProductProperties");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProperties_Categories_categoryId",
                table: "ProductProperties",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
