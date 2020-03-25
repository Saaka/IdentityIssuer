using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class RemoveTenantSettingsIdFromTenantProviderSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantSettingsId",
                schema: "identityiss",
                table: "TenantProviderSettings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantSettingsId",
                schema: "identityiss",
                table: "TenantProviderSettings",
                type: "int",
                nullable: true);

            AddTenantSettingsIdToProviderSettings(migrationBuilder);
        }

        private static void AddTenantSettingsIdToProviderSettings(MigrationBuilder migrationBuilder)
        {
            const string schema = PersistenceConstants.DefaultIdentitySchema;
            migrationBuilder.Sql($"UPDATE TPS " +
                                 $"SET TPS.TenantSettingsId = " +
                                 $"(SELECT TOP 1 TS.Id " +
                                 $"FROM {schema}.TenantSettings TS " +
                                 $"WHERE TS.TenantId = TPS.TenantId) " +
                                 $"FROM {schema}.TenantProviderSettings TPS");
        }
    }
}
