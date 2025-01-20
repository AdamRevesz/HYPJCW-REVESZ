using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ver5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Videos",
                newName: "Tags");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "SalesItems",
                newName: "Tags");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Pictures",
                newName: "Tags");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Courses",
                newName: "Tags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "Videos",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "SalesItems",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "Pictures",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "Courses",
                newName: "Category");
        }
    }
}
