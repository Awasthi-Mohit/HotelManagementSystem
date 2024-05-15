using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class hoteltoreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "imagemultiples");

            migrationBuilder.DropColumn(
                name: "OccupiedTypeRooms",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "TotalTypeRooms",
                table: "RoomTypes");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "OccupiedTypeRooms",
                table: "RoomTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalTypeRooms",
                table: "RoomTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "imagemultiples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imagemultiples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_imagemultiples_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_imagemultiples_RoomId",
                table: "imagemultiples",
                column: "RoomId");
        }
    }
}
