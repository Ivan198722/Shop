using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class remoweProprty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CellphoneProperties");

            migrationBuilder.DropTable(
                name: "PhotoProperties");

            migrationBuilder.DropTable(
                name: "TVProperties");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ProductProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: true),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    propertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    propertyParameters = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductProperties_Categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProperties_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProperties_categoryId",
                table: "ProductProperties",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProperties_productId",
                table: "ProductProperties",
                column: "productId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProperties");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CellphoneProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    batteryCapacity = table.Column<double>(type: "float", nullable: true),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memory = table.Column<int>(type: "int", nullable: true),
                    operatingSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    port = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ram = table.Column<int>(type: "int", nullable: true),
                    screen = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CellphoneProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CellphoneProperties_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CellphoneProperties_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    port = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoProperties_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhotoProperties_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TVProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    energyClasse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pixelResolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    port = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    refreshRate = table.Column<int>(type: "int", nullable: true),
                    screen = table.Column<float>(type: "real", nullable: true),
                    screenResolution = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TVProperties_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TVProperties_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CellphoneProperties_CategoryId",
                table: "CellphoneProperties",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CellphoneProperties_productId",
                table: "CellphoneProperties",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoProperties_CategoryId",
                table: "PhotoProperties",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoProperties_productId",
                table: "PhotoProperties",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TVProperties_CategoryId",
                table: "TVProperties",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TVProperties_productId",
                table: "TVProperties",
                column: "productId");
        }
    }
}
