using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class AlterTenantAddIsAdminTenantIsActiveFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "identityiss",
                table: "Tenants",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdminTenant",
                schema: "identityiss",
                table: "Tenants",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "identityiss",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "IsAdminTenant",
                schema: "identityiss",
                table: "Tenants");
        }
    }
}
