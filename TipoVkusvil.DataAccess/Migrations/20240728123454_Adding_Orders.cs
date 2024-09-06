using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TipoVkusvil.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderProducts",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_CartItems_CartId",
                table: "OrderProducts",
                column: "CartId",
                principalTable: "CartItems",
                principalColumn: "CartItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_CartItems_CartId",
                table: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "OrderProducts",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_CartId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
