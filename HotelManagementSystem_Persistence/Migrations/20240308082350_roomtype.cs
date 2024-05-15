
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class roomtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingModels_Reviews_ReviewId",
                table: "BookingModels");

            migrationBuilder.DropIndex(
                name: "IX_BookingModels_ReviewId",
                table: "BookingModels");

            migrationBuilder.DropColumn(
                name: "CustomerPaymentIntentId",
                table: "BookingModels");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "BookingModels");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "BookingModels");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "BookingModels");

            migrationBuilder.DropColumn(
                name: "RoomTypeByRoomId",
                table: "BookingModels");

            migrationBuilder.DropColumn(
                name: "TxId",
                table: "BookingModels");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerAddress",
                table: "BookingModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerAddress",
                table: "BookingModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPaymentIntentId",
                table: "BookingModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "BookingModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "BookingModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "BookingModels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomTypeByRoomId",
                table: "BookingModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TxId",
                table: "BookingModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BookingModels_ReviewId",
                table: "BookingModels",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingModels_Reviews_ReviewId",
                table: "BookingModels",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id");
        }
    }
}
