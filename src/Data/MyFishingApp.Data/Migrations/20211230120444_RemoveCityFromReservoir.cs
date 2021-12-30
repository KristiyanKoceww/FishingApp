namespace MyFishingApp.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveCityFromReservoir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityId",
                table: "Reservoirs",
                type: "nvarchar(450)",
                nullable: true);

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
    }
}
