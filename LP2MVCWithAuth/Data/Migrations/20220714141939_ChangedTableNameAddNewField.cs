using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LP2MVCWithAuth.Data.Migrations
{
    public partial class ChangedTableNameAddNewField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "ListaDeTarefas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "ListaDeTarefas");
        }
    }
}
