using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initload323 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypes_HotelModels_HotelId",
                table: "RoomTypes");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "RoomTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypes_HotelModels_HotelId",
                table: "RoomTypes",
                column: "HotelId",
                principalTable: "HotelModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypes_HotelModels_HotelId",
                table: "RoomTypes");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "RoomTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypes_HotelModels_HotelId",
                table: "RoomTypes",
                column: "HotelId",
                principalTable: "HotelModels",
                principalColumn: "Id");
        }
    }
}
