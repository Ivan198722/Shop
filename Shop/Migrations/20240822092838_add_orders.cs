using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class add_orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinishedOrders_Customers_customerId",
                table: "FinishedOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_FinishedOrders_Products_productID",
                table: "FinishedOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Customers_customerId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopCartItems_Images_imagesId",
                table: "ShopCartItems");

            migrationBuilder.DropIndex(
                name: "IX_FinishedOrders_customerId",
                table: "FinishedOrders");

            migrationBuilder.DropIndex(
                name: "IX_FinishedOrders_productID",
                table: "FinishedOrders");

            migrationBuilder.DropColumn(
                name: "customerId",
                table: "FinishedOrders");

            migrationBuilder.DropColumn(
                name: "price",
                table: "FinishedOrders");

            migrationBuilder.DropColumn(
                name: "productID",
                table: "FinishedOrders");

            migrationBuilder.DropColumn(
                name: "orderTime",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "OrderDetails",
                newName: "orderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_customerId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_orderId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "FinishedOrders",
                newName: "orderId");

            migrationBuilder.AlterColumn<int>(
                name: "imagesId",
                table: "ShopCartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    customerId = table.Column<int>(type: "int", nullable: false),
                    paymentStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_customerId",
                        column: x => x.customerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinishedOrders_orderId",
                table: "FinishedOrders",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_customerId",
                table: "Orders",
                column: "customerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedOrders_Orders_orderId",
                table: "FinishedOrders",
                column: "orderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_orderId",
                table: "OrderDetails",
                column: "orderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopCartItems_Images_imagesId",
                table: "ShopCartItems",
                column: "imagesId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinishedOrders_Orders_orderId",
                table: "FinishedOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_orderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopCartItems_Images_imagesId",
                table: "ShopCartItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_FinishedOrders_orderId",
                table: "FinishedOrders");

          

            migrationBuilder.RenameColumn(
                name: "orderId",
                table: "OrderDetails",
                newName: "customerId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_orderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_customerId");

            migrationBuilder.RenameColumn(
                name: "orderId",
                table: "FinishedOrders",
                newName: "quantity");

            migrationBuilder.AlterColumn<int>(
                name: "imagesId",
                table: "ShopCartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "customerId",
                table: "FinishedOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "FinishedOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "productID",
                table: "FinishedOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "orderTime",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_FinishedOrders_customerId",
                table: "FinishedOrders",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedOrders_productID",
                table: "FinishedOrders",
                column: "productID");

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedOrders_Customers_customerId",
                table: "FinishedOrders",
                column: "customerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedOrders_Products_productID",
                table: "FinishedOrders",
                column: "productID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Customers_customerId",
                table: "OrderDetails",
                column: "customerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopCartItems_Images_imagesId",
                table: "ShopCartItems",
                column: "imagesId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
