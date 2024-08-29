using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsPanel.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserinactivation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InactivatedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Situation",
                table: "Users",
                type: "nvarchar(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserInactiveAction",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenTime",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 27, 19, 34, 36, 652, DateTimeKind.Utc).AddTicks(6312),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 26, 23, 0, 31, 979, DateTimeKind.Utc).AddTicks(1903));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InactivatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Situation",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserInactiveAction",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenTime",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 26, 23, 0, 31, 979, DateTimeKind.Utc).AddTicks(1903),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 27, 19, 34, 36, 652, DateTimeKind.Utc).AddTicks(6312));
        }
    }
}
