using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TipoVkusvil.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Table_CartItemFinished : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCarts_CartItems_CartId",
                table: "OrderCarts");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "OrderCarts",
                newName: "CartItemFinishedId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCarts_CartId",
                table: "OrderCarts",
                newName: "IX_OrderCarts_CartItemFinishedId");

            migrationBuilder.CreateTable(
                name: "CartItemsForOrders",
                columns: table => new
                {
                    CartItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemsForOrders", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItemsForOrders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItemsForOrders_ProductId",
                table: "CartItemsForOrders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCarts_CartItemsForOrders_CartItemFinishedId",
                table: "OrderCarts",
                column: "CartItemFinishedId",
                principalTable: "CartItemsForOrders",
                principalColumn: "CartItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCarts_CartItemsForOrders_CartItemFinishedId",
                table: "OrderCarts");

            migrationBuilder.DropTable(
                name: "CartItemsForOrders");

            migrationBuilder.RenameColumn(
                name: "CartItemFinishedId",
                table: "OrderCarts",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCarts_CartItemFinishedId",
                table: "OrderCarts",
                newName: "IX_OrderCarts_CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCarts_CartItems_CartId",
                table: "OrderCarts",
                column: "CartId",
                principalTable: "CartItems",
                principalColumn: "CartItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
