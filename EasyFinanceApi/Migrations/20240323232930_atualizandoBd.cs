using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyFinanceApi.Migrations
{
    /// <inheritdoc />
    public partial class atualizandoBd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses");

            migrationBuilder.RenameTable(
                name: "Expenses",
                newName: "tb_expense");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_expense",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_expense",
                table: "tb_expense",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_expense",
                table: "tb_expense");

            migrationBuilder.RenameTable(
                name: "tb_expense",
                newName: "Expenses");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Expenses",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses",
                column: "Id");
        }
    }
}
