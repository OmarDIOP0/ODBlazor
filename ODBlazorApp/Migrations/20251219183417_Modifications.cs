using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODBlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class Modifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 19, 18, 34, 17, 24, DateTimeKind.Local).AddTicks(678));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 19, 18, 21, 57, 657, DateTimeKind.Local).AddTicks(6675));
        }
    }
}
