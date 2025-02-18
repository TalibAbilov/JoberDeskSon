using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoberDesk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangingColumnsOfCompanyEnty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PremiumUntil",
                table: "Companies",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PremiumUntil",
                table: "Companies");
        }
    }
}
