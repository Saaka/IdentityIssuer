using IdentityIssuer.Common.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class UpdateTenantSettingsWithDefaultLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AddDefaultLanguages(migrationBuilder);
        }

        private static void AddDefaultLanguages(MigrationBuilder migrationBuilder)
        {
            const string schema = PersistenceConstants.DefaultIdentitySchema;
            byte defaultLanguage = (byte)LanguageCode.EN;
            migrationBuilder.Sql($"UPDATE {schema}.TenantSettings " +
                                 $"SET    DefaultLanguage = {defaultLanguage.ToString()}");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ResetDefaultLanguages(migrationBuilder);
        }

        private static void ResetDefaultLanguages(MigrationBuilder migrationBuilder)
        {
            const string schema = PersistenceConstants.DefaultIdentitySchema;
            migrationBuilder.Sql($"UPDATE {schema}.TenantSettings " +
                                 $"SET    DefaultLanguage = null");
        }
    }
}