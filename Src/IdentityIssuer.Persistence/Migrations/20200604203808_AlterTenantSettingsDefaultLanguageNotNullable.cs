using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class AlterTenantSettingsDefaultLanguageNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "DefaultLanguage",
                schema: "identityiss",
                table: "TenantSettings",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "DefaultLanguage",
                schema: "identityiss",
                table: "TenantSettings",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte));
        }
    }
}
