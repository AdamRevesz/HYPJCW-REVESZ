using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ver3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Contents_ContentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Contents_Id",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Contents_Id",
                table: "Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesItems_Contents_Id",
                table: "SalesItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Contents_Id",
                table: "Videos");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Videos",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreateDate",
                table: "Videos",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLikes",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Videos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Videos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "SalesItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "SalesItems",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreateDate",
                table: "SalesItems",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLikes",
                table: "SalesItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "SalesItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "SalesItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Pictures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Pictures",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreateDate",
                table: "Pictures",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLikes",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Pictures",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Pictures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Courses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreateDate",
                table: "Courses",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLikes",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Courses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_OwnerId",
                table: "Videos",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItems_OwnerId",
                table: "SalesItems",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_OwnerId",
                table: "Pictures",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_OwnerId",
                table: "Courses",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_OwnerId",
                table: "Courses",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_AspNetUsers_OwnerId",
                table: "Pictures",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesItems_AspNetUsers_OwnerId",
                table: "SalesItems",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_OwnerId",
                table: "Videos",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_OwnerId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_AspNetUsers_OwnerId",
                table: "Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesItems_AspNetUsers_OwnerId",
                table: "SalesItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_OwnerId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_OwnerId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_SalesItems_OwnerId",
                table: "SalesItems");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_OwnerId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Courses_OwnerId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "NumberOfLikes",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "SalesItems");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "SalesItems");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "SalesItems");

            migrationBuilder.DropColumn(
                name: "NumberOfLikes",
                table: "SalesItems");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "SalesItems");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "SalesItems");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "NumberOfLikes",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "NumberOfLikes",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: false),
                    NumberOfLikes = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contents_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_OwnerId",
                table: "Contents",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Contents_ContentId",
                table: "Comments",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Contents_Id",
                table: "Courses",
                column: "Id",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Contents_Id",
                table: "Pictures",
                column: "Id",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesItems_Contents_Id",
                table: "SalesItems",
                column: "Id",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Contents_Id",
                table: "Videos",
                column: "Id",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
