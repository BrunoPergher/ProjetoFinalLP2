using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LP2MVCWithAuth.Data.Migrations
{
    public partial class adjustsOnTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTarefa",
                table: "Location",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsInitical",
                table: "Location",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdTarefa",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "IsInitical",
                table: "Location");
        }
    }
}
