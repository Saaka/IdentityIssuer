using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class AddTenantTenantProviderSettingsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                schema: "identityiss",
                table: "TenantProviderSettings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantProviderSettings_TenantId",
                schema: "identityiss",
                table: "TenantProviderSettings",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_TenantProviderSettings_Tenants_TenantId",
                schema: "identityiss",
                table: "TenantProviderSettings",
                column: "TenantId",
                principalSchema: "identityiss",
                principalTable: "Tenants",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantProviderSettings_Tenants_TenantId",
                schema: "identityiss",
                table: "TenantProviderSettings");

            migrationBuilder.DropIndex(
                name: "IX_TenantProviderSettings_TenantId",
                schema: "identityiss",
                table: "TenantProviderSettings");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "identityiss",
                table: "TenantProviderSettings");
        }
    }
}
