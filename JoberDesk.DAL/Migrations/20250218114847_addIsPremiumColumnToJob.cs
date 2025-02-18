using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoberDesk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addIsPremiumColumnToJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PremiumEndTime",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPremium",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "PremiumEndTime",
                table: "Jobs");
        }
    }
}
