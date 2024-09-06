using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TipoVkusvil.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Product_ImgURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgURL",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgURL",
                table: "Product",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgURL",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImgURL",
                table: "Product");
        }
    }
}
