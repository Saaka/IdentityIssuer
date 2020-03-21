using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class UpdateUserAddIsAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                schema: "identityiss",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                schema: "identityiss",
                table: "AspNetUsers");
        }
    }
}
