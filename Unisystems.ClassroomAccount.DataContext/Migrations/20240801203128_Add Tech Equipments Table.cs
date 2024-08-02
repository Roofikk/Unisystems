using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Unisystems.ClassroomAccount.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class AddTechEquipmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechEquipments",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ClassroomId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechEquipments", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "FK_TechEquipments_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechEquipment_Name",
                table: "TechEquipments",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_TechEquipments_ClassroomId",
                table: "TechEquipments",
                column: "ClassroomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechEquipments");
        }
    }
}
