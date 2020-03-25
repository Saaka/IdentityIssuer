using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class JoinProvidersWithTenants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AddTenantIdToProviderSettings(migrationBuilder);
        }

        private static void AddTenantIdToProviderSettings(MigrationBuilder migrationBuilder)
        {
            const string schema = PersistenceConstants.DefaultIdentitySchema;
            migrationBuilder.Sql($"UPDATE TPS " +
                                 $"SET TPS.TenantId = " +
                                     $"(SELECT TOP 1 TS.TenantId " +
                                     $"FROM {schema}.TenantSettings TS " +
                                     $"WHERE TS.Id = TPS.TenantSettingsId) " +
                                 $"FROM {schema}.TenantProviderSettings TPS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}