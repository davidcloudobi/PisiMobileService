using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PisiMobile.CoreObject.Migrations
{
    public partial class userToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidHours",
                table: "AccessTokens",
                newName: "ValidMinutes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidMinutes",
                table: "AccessTokens",
                newName: "ValidHours");
        }
    }
}
