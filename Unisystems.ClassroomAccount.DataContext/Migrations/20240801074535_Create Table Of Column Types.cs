using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unisystems.ClassroomAccount.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableOfColumnTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Columns_ColumnType_ColumnTypeId",
                table: "Columns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ColumnType",
                table: "ColumnType");

            migrationBuilder.RenameTable(
                name: "ColumnType",
                newName: "ColumnTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ColumnTypes",
                table: "ColumnTypes",
                column: "ColumnTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Columns_ColumnTypes_ColumnTypeId",
                table: "Columns",
                column: "ColumnTypeId",
                principalTable: "ColumnTypes",
                principalColumn: "ColumnTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Columns_ColumnTypes_ColumnTypeId",
                table: "Columns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ColumnTypes",
                table: "ColumnTypes");

            migrationBuilder.RenameTable(
                name: "ColumnTypes",
                newName: "ColumnType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ColumnType",
                table: "ColumnType",
                column: "ColumnTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Columns_ColumnType_ColumnTypeId",
                table: "Columns",
                column: "ColumnTypeId",
                principalTable: "ColumnType",
                principalColumn: "ColumnTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
