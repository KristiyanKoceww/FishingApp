using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFishingApp.Data.Migrations
{
    public partial class RemoveImageCollectionFromUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageUrls_AspNetUsers_ApplicationUserId",
                table: "ImageUrls");

            migrationBuilder.DropIndex(
                name: "IX_ImageUrls_ApplicationUserId",
                table: "ImageUrls");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ImageUrls");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ImageUrls",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageUrls_ApplicationUserId",
                table: "ImageUrls",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageUrls_AspNetUsers_ApplicationUserId",
                table: "ImageUrls",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
