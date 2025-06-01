using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UserLikedContents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLikedContents",
                columns: table => new
                {
                    LikedByUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikedContentsId = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikedContents", x => new { x.LikedByUsersId, x.LikedContentsId });
                    table.ForeignKey(
                        name: "FK_UserLikedContents_AspNetUsers_LikedByUsersId",
                        column: x => x.LikedByUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLikedContents_LikedContentsId",
                table: "UserLikedContents",
                column: "LikedContentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLikedContents");
        }
    }
}
