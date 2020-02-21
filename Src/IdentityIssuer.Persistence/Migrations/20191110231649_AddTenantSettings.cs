using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class AddTenantSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenantSettings",
                schema: "identityiss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    TokenSecret = table.Column<string>(maxLength: 256, nullable: false),
                    TokenExpirationInMinutes = table.Column<int>(nullable: false),
                    EnableCredentialsLogin = table.Column<bool>(nullable: false),
                    EnableGoogleLogin = table.Column<bool>(nullable: false),
                    EnableFacebookLogin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantSettings_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "identityiss",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantProviderSettings",
                schema: "identityiss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantSettingsId = table.Column<int>(nullable: false),
                    ProviderType = table.Column<byte>(nullable: false),
                    Identifier = table.Column<string>(maxLength: 128, nullable: false),
                    Key = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantProviderSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantProviderSettings_TenantSettings_TenantSettingsId",
                        column: x => x.TenantSettingsId,
                        principalSchema: "identityiss",
                        principalTable: "TenantSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantProviderSettings_TenantSettingsId",
                schema: "identityiss",
                table: "TenantProviderSettings",
                column: "TenantSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSettings_TenantId",
                schema: "identityiss",
                table: "TenantSettings",
                column: "TenantId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantProviderSettings",
                schema: "identityiss");

            migrationBuilder.DropTable(
                name: "TenantSettings",
                schema: "identityiss");
        }
    }
}
