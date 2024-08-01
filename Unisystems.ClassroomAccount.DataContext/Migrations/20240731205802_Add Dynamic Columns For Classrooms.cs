using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Unisystems.ClassroomAccount.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class AddDynamicColumnsForClassrooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColumnType",
                columns: table => new
                {
                    ColumnTypeId = table.Column<string>(type: "varchar(36)", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnType", x => x.ColumnTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Columns",
                columns: table => new
                {
                    ColumnId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false),
                    ColumnTypeId = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Columns", x => x.ColumnId);
                    table.ForeignKey(
                        name: "FK_Columns_ColumnType_ColumnTypeId",
                        column: x => x.ColumnTypeId,
                        principalTable: "ColumnType",
                        principalColumn: "ColumnTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoubleColumnValues",
                columns: table => new
                {
                    ClassroomId = table.Column<int>(type: "integer", nullable: false),
                    ColumnId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoubleColumnValues", x => new { x.ClassroomId, x.ColumnId });
                    table.ForeignKey(
                        name: "FK_DoubleColumnValues_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoubleColumnValues_Columns_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "Columns",
                        principalColumn: "ColumnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InputColumnValues",
                columns: table => new
                {
                    ClassroomId = table.Column<int>(type: "integer", nullable: false),
                    ColumnId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "varchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputColumnValues", x => new { x.ClassroomId, x.ColumnId });
                    table.ForeignKey(
                        name: "FK_InputColumnValues_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InputColumnValues_Columns_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "Columns",
                        principalColumn: "ColumnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntColumnValues",
                columns: table => new
                {
                    ClassroomId = table.Column<int>(type: "integer", nullable: false),
                    ColumnId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntColumnValues", x => new { x.ClassroomId, x.ColumnId });
                    table.ForeignKey(
                        name: "FK_IntColumnValues_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntColumnValues_Columns_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "Columns",
                        principalColumn: "ColumnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextAreaColumnValues",
                columns: table => new
                {
                    ClassroomId = table.Column<int>(type: "integer", nullable: false),
                    ColumnId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextAreaColumnValues", x => new { x.ClassroomId, x.ColumnId });
                    table.ForeignKey(
                        name: "FK_TextAreaColumnValues_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TextAreaColumnValues_Columns_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "Columns",
                        principalColumn: "ColumnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Columns_ColumnTypeId",
                table: "Columns",
                column: "ColumnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DoubleColumnValues_ColumnId",
                table: "DoubleColumnValues",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_InputColumnValues_ColumnId",
                table: "InputColumnValues",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_IntColumnValues_ColumnId",
                table: "IntColumnValues",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_TextAreaColumnValues_ColumnId",
                table: "TextAreaColumnValues",
                column: "ColumnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoubleColumnValues");

            migrationBuilder.DropTable(
                name: "InputColumnValues");

            migrationBuilder.DropTable(
                name: "IntColumnValues");

            migrationBuilder.DropTable(
                name: "TextAreaColumnValues");

            migrationBuilder.DropTable(
                name: "Columns");

            migrationBuilder.DropTable(
                name: "ColumnType");
        }
    }
}
