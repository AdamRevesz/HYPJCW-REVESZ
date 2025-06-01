using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UserDislikedContents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDislikedContents",
                columns: table => new
                {
                    DislikedByUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DislikedContentsId = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDislikedContents", x => new { x.DislikedByUsersId, x.DislikedContentsId });
                    table.ForeignKey(
                        name: "FK_UserDislikedContents_AspNetUsers_DislikedByUsersId",
                        column: x => x.DislikedByUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDislikedContents_DislikedContentsId",
                table: "UserDislikedContents",
                column: "DislikedContentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDislikedContents");
        }
    }
}
