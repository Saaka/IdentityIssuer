using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class RemoveTenantSettingsTenantProviderSettingsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantProviderSettings_Tenants_TenantId",
                schema: "identityiss",
                table: "TenantProviderSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantProviderSettings_TenantSettings_TenantSettingsId",
                schema: "identityiss",
                table: "TenantProviderSettings");

            migrationBuilder.DropIndex(
                name: "IX_TenantProviderSettings_TenantSettingsId",
                schema: "identityiss",
                table: "TenantProviderSettings");

            migrationBuilder.AlterColumn<int>(
                name: "TenantSettingsId",
                schema: "identityiss",
                table: "TenantProviderSettings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                schema: "identityiss",
                table: "TenantProviderSettings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantProviderSettings_Tenants_TenantId",
                schema: "identityiss",
                table: "TenantProviderSettings",
                column: "TenantId",
                principalSchema: "identityiss",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantProviderSettings_Tenants_TenantId",
                schema: "identityiss",
                table: "TenantProviderSettings");

            migrationBuilder.AlterColumn<int>(
                name: "TenantSettingsId",
                schema: "identityiss",
                table: "TenantProviderSettings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                schema: "identityiss",
                table: "TenantProviderSettings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_TenantProviderSettings_TenantSettingsId",
                schema: "identityiss",
                table: "TenantProviderSettings",
                column: "TenantSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TenantProviderSettings_Tenants_TenantId",
                schema: "identityiss",
                table: "TenantProviderSettings",
                column: "TenantId",
                principalSchema: "identityiss",
                principalTable: "Tenants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TenantProviderSettings_TenantSettings_TenantSettingsId",
                schema: "identityiss",
                table: "TenantProviderSettings",
                column: "TenantSettingsId",
                principalSchema: "identityiss",
                principalTable: "TenantSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
