namespace MyFishingApp.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddMorePropertiesToDbModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Weather",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Weather",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Main",
                table: "Weather",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Dams",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Dams",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Weather");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Weather");

            migrationBuilder.DropColumn(
                name: "Main",
                table: "Weather");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Dams");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Dams");
        }
    }
}
