using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlexandruMaries.Data.Migrations
{
    public partial class IsVisiblePropertyReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Reference",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Reference");
        }
    }
}
