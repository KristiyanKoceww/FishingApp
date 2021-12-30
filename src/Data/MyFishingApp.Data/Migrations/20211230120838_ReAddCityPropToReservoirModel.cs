using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFishingApp.Data.Migrations
{
    public partial class ReAddCityPropToReservoirModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityId",
                table: "Reservoirs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_CityId",
                table: "Reservoirs",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Cities_CityId",
                table: "Reservoirs",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Cities_CityId",
                table: "Reservoirs");

            migrationBuilder.DropIndex(
                name: "IX_Reservoirs_CityId",
                table: "Reservoirs");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Reservoirs");
        }
    }
}
