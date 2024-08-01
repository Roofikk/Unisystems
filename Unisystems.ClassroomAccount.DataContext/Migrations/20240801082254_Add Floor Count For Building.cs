using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unisystems.ClassroomAccount.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class AddFloorCountForBuilding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FloorCount",
                table: "PartialBuildings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorCount",
                table: "PartialBuildings");
        }
    }
}
