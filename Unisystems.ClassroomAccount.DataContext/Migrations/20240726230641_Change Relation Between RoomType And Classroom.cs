using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unisystems.ClassroomAccount.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationBetweenRoomTypeAndClassroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_RoomTypes_RoomTypeId",
                table: "Classrooms");

            migrationBuilder.AlterColumn<string>(
                name: "RoomTypeId",
                table: "Classrooms",
                type: "varchar(24)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(24)");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_RoomTypes_RoomTypeId",
                table: "Classrooms",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "KeyName",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_RoomTypes_RoomTypeId",
                table: "Classrooms");

            migrationBuilder.AlterColumn<string>(
                name: "RoomTypeId",
                table: "Classrooms",
                type: "varchar(24)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(24)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_RoomTypes_RoomTypeId",
                table: "Classrooms",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "KeyName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
