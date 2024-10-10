using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsPanel.Migrations
{
    /// <inheritdoc />
    public partial class OpenDataTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OpenTime",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                defaultValueSql: "FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm:ss')",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "02/10/2024 16:25:49");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                oldDefaultValueSql: "FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm:ss')");
        }
    }
}
