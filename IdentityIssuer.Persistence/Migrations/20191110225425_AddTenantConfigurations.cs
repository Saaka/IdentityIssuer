using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class AddTenantConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenantConfigurations",
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
                    table.PrimaryKey("PK_TenantConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantConfigurations_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "identityiss",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantProviders",
                schema: "identityiss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantConfigurationId = table.Column<int>(nullable: false),
                    ProviderType = table.Column<byte>(nullable: false),
                    Identifier = table.Column<string>(maxLength: 128, nullable: false),
                    Key = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantProviders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantProviders_TenantConfigurations_TenantConfigurationId",
                        column: x => x.TenantConfigurationId,
                        principalSchema: "identityiss",
                        principalTable: "TenantConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantConfigurations_TenantId",
                schema: "identityiss",
                table: "TenantConfigurations",
                column: "TenantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantProviders_TenantConfigurationId",
                schema: "identityiss",
                table: "TenantProviders",
                column: "TenantConfigurationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantProviders",
                schema: "identityiss");

            migrationBuilder.DropTable(
                name: "TenantConfigurations",
                schema: "identityiss");
        }
    }
}
