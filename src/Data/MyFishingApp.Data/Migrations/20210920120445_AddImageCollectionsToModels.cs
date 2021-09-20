using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFishingApp.Data.Migrations
{
    public partial class AddImageCollectionsToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_ApplicationUserId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Images",
                newName: "AddedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ApplicationUserId",
                table: "Images",
                newName: "IX_Images_AddedByUserId");

            migrationBuilder.AddColumn<string>(
                name: "MainImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImageUrls",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FishId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    KnotId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReservoirId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageUrls_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageUrls_Fish_FishId",
                        column: x => x.FishId,
                        principalTable: "Fish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageUrls_Knots_KnotId",
                        column: x => x.KnotId,
                        principalTable: "Knots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageUrls_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageUrls_ApplicationUserId",
                table: "ImageUrls",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageUrls_FishId",
                table: "ImageUrls",
                column: "FishId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageUrls_IsDeleted",
                table: "ImageUrls",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ImageUrls_KnotId",
                table: "ImageUrls",
                column: "KnotId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageUrls_ReservoirId",
                table: "ImageUrls",
                column: "ReservoirId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_AddedByUserId",
                table: "Images",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_AddedByUserId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "ImageUrls");

            migrationBuilder.DropColumn(
                name: "MainImageUrl",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AddedByUserId",
                table: "Images",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_AddedByUserId",
                table: "Images",
                newName: "IX_Images_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_ApplicationUserId",
                table: "Images",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
