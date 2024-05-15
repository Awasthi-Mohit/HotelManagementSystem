using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class user1234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateId",
                table: "AspNetUsers");
        }
    }
}
