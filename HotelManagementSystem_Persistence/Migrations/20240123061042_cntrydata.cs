using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cntrydata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.Sql("insert Countrys values('India')");
            migrationBuilder.Sql("insert Countrys values('Us')");
            migrationBuilder.Sql("insert Countrys values('UK')");

            migrationBuilder.Sql("insert States values('Punjab',1)");
            migrationBuilder.Sql("insert States values('Hp',1)");
            migrationBuilder.Sql("insert States values('Up',1)");
            migrationBuilder.Sql("insert States values('ABCP',1)");
            migrationBuilder.Sql("insert States values('Alabama ',2)");
            migrationBuilder.Sql("insert States values('California',2)");


            migrationBuilder.Sql("insert Citys values('Mohali',1)");
            migrationBuilder.Sql("insert Citys values('LDH',1)");
            migrationBuilder.Sql("insert Citys values('SRS',1)");
            migrationBuilder.Sql("insert Citys values('Shimla',2)");
            migrationBuilder.Sql("insert Citys values('Kangra',2)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
