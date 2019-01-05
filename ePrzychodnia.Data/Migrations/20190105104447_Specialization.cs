using Microsoft.EntityFrameworkCore.Migrations;

namespace ePrzychodnia.Data.Migrations
{
    public partial class Specialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Specialization",
                table: "Doctors",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Doctors");
        }
    }
}
