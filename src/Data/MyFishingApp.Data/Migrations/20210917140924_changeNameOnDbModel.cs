namespace MyFishingApp.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class changeNameOnDbModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fish_Dams_DamId",
                table: "Fish");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Dams_DamId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Dams");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.RenameColumn(
                name: "DamId",
                table: "Fish",
                newName: "ReservoirId");

            migrationBuilder.RenameIndex(
                name: "IX_Fish_DamId",
                table: "Fish",
                newName: "IX_Fish_ReservoirId");

            migrationBuilder.CreateTable(
                name: "Reservoirs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    WeatherId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservoirs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservoirs_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservoirs_Weather_WeatherId",
                        column: x => x.WeatherId,
                        principalTable: "Weather",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_CityId",
                table: "Reservoirs",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_IsDeleted",
                table: "Reservoirs",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_WeatherId",
                table: "Reservoirs",
                column: "WeatherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fish_Reservoirs_ReservoirId",
                table: "Fish",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Reservoirs_DamId",
                table: "Images",
                column: "DamId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fish_Reservoirs_ReservoirId",
                table: "Fish");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Reservoirs_DamId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Reservoirs");

            migrationBuilder.RenameColumn(
                name: "ReservoirId",
                table: "Fish",
                newName: "DamId");

            migrationBuilder.RenameIndex(
                name: "IX_Fish_ReservoirId",
                table: "Fish",
                newName: "IX_Fish_DamId");

            migrationBuilder.CreateTable(
                name: "Dams",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dams_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dams_Weather_WeatherId",
                        column: x => x.WeatherId,
                        principalTable: "Weather",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dams_CityId",
                table: "Dams",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Dams_IsDeleted",
                table: "Dams",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Dams_WeatherId",
                table: "Dams",
                column: "WeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_IsDeleted",
                table: "Settings",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Fish_Dams_DamId",
                table: "Fish",
                column: "DamId",
                principalTable: "Dams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Dams_DamId",
                table: "Images",
                column: "DamId",
                principalTable: "Dams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
