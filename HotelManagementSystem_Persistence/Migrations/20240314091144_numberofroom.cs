﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class numberofroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfRooms",
                table: "RoomTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfRooms",
                table: "RoomTypes");
        }
    }
}
