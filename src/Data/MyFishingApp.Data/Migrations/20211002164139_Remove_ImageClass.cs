namespace MyFishingApp.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Remove_ImageClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Weather_WeatherId",
                table: "Reservoirs");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Weather");

            migrationBuilder.DropIndex(
                name: "IX_Reservoirs_WeatherId",
                table: "Reservoirs");

            migrationBuilder.DropColumn(
                name: "WeatherId",
                table: "Reservoirs");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "ImageUrls",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageUrls_PostId",
                table: "ImageUrls",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageUrls_Posts_PostId",
                table: "ImageUrls",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageUrls_Posts_PostId",
                table: "ImageUrls");

            migrationBuilder.DropIndex(
                name: "IX_ImageUrls_PostId",
                table: "ImageUrls");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "ImageUrls");

            migrationBuilder.AddColumn<string>(
                name: "WeatherId",
                table: "Reservoirs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FishId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    KnotId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoteImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReservoirId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_AspNetUsers_AddedByUserId",
                        column: x => x.AddedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_Fish_FishId",
                        column: x => x.FishId,
                        principalTable: "Fish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_Knots_KnotId",
                        column: x => x.KnotId,
                        principalTable: "Knots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cloudy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Humitity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Main = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pressure = table.Column<double>(type: "float", nullable: true),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Wind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weather_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_WeatherId",
                table: "Reservoirs",
                column: "WeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AddedByUserId",
                table: "Images",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_FishId",
                table: "Images",
                column: "FishId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_IsDeleted",
                table: "Images",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Images_KnotId",
                table: "Images",
                column: "KnotId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ReservoirId",
                table: "Images",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_Weather_CityId",
                table: "Weather",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Weather_IsDeleted",
                table: "Weather",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Weather_WeatherId",
                table: "Reservoirs",
                column: "WeatherId",
                principalTable: "Weather",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
