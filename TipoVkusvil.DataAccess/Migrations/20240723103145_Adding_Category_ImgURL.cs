using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TipoVkusvil.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Category_ImgURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgURL",
                table: "Category",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgURL",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgURL",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ImgURL",
                table: "Categories");
        }
    }
}
