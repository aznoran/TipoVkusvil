using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TipoVkusvil.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Orders1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_CartItems_CartId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.RenameTable(
                name: "OrderProducts",
                newName: "OrderCarts");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_CartId",
                table: "OrderCarts",
                newName: "IX_OrderCarts_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderCarts",
                table: "OrderCarts",
                columns: new[] { "OrderId", "CartId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCarts_CartItems_CartId",
                table: "OrderCarts",
                column: "CartId",
                principalTable: "CartItems",
                principalColumn: "CartItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCarts_Orders_OrderId",
                table: "OrderCarts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCarts_CartItems_CartId",
                table: "OrderCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderCarts_Orders_OrderId",
                table: "OrderCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderCarts",
                table: "OrderCarts");

            migrationBuilder.RenameTable(
                name: "OrderCarts",
                newName: "OrderProducts");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCarts_CartId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                columns: new[] { "OrderId", "CartId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_CartItems_CartId",
                table: "OrderProducts",
                column: "CartId",
                principalTable: "CartItems",
                principalColumn: "CartItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
