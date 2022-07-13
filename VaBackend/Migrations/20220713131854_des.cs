using Microsoft.EntityFrameworkCore.Migrations;

namespace VaBackend.Migrations
{
    public partial class des : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieDescription",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieDescription",
                table: "Movie");
        }
    }
}
