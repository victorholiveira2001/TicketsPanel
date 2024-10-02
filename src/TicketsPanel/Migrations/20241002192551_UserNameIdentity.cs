using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsPanel.Migrations
{
    /// <inheritdoc />
    public partial class UserNameIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OpenTime",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "02/10/2024 16:25:49",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "27/09/2024 21:17:40");

            migrationBuilder.AlterColumn<string>(
                name: "CloseTime",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SentTime",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "OpenTime",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "27/09/2024 21:17:40",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "02/10/2024 16:25:49");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CloseTime",
                table: "Tickets",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "SentTime",
                table: "Messages",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
