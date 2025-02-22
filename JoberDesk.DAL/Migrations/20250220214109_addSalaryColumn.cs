using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoberDesk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addSalaryColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Salary",
                table: "Jobs",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Jobs");
        }
    }
}
