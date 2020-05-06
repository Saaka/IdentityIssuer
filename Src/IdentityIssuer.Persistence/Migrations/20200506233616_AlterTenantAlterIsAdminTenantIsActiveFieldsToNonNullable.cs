using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class AlterTenantAlterIsAdminTenantIsActiveFieldsToNonNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AddAdminTenant(migrationBuilder);
        }

        private static void AddAdminTenant(MigrationBuilder migrationBuilder)
        {
            const string schema = PersistenceConstants.DefaultIdentitySchema;
            migrationBuilder.Sql($"UPDATE	T " +
                                 $"SET		T.IsAdminTenant = 1 " +
                                 $"FROM	    {schema}.Tenants T " +
                                 $"WHERE	T.Id IN " +
                                 $"(SELECT	TOP 1 FT.Id " +
                                 $"FROM	{schema}.Tenants FT " +
                                 $"ORDER BY FT.Id ASC)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            RemoveAdminTenant(migrationBuilder);
        }

        private static void RemoveAdminTenant(MigrationBuilder migrationBuilder)
        {
            const string schema = PersistenceConstants.DefaultIdentitySchema;
            migrationBuilder.Sql($"UPDATE	{schema}.Tenants" +
                                 $"SET		IsAdminTenant = 0");
        }
    }
}