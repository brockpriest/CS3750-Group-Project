using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment13750.Migrations
{
    public partial class UpdateSubmissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Submissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Submissions");
        }
    }
}
