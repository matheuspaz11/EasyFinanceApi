using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyFinanceApi.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreationUserId",
                table: "tb_expense",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                table: "tb_expense",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationUserId",
                table: "tb_expense");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "tb_expense");
        }
    }
}
