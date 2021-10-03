namespace MyFishingApp.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeNameOfModelProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Reservoirs_DamId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "DamId",
                table: "Images",
                newName: "ReservoirId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_DamId",
                table: "Images",
                newName: "IX_Images_ReservoirId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Reservoirs_ReservoirId",
                table: "Images",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Reservoirs_ReservoirId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ReservoirId",
                table: "Images",
                newName: "DamId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ReservoirId",
                table: "Images",
                newName: "IX_Images_DamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Reservoirs_DamId",
                table: "Images",
                column: "DamId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
