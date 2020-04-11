using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class CreateTenantApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenantApplications",
                schema: "identityiss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantApplicationGuid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    TenantCode = table.Column<string>(maxLength: 3, nullable: false),
                    OwnerEmail = table.Column<string>(maxLength: 256, nullable: false),
                    AllowedOrigin = table.Column<string>(maxLength: 128, nullable: false),
                    TokenSecret = table.Column<string>(maxLength: 256, nullable: false),
                    TokenExpirationInMinutes = table.Column<int>(nullable: false),
                    EnableCredentialsLogin = table.Column<bool>(nullable: false),
                    EnableGoogleLogin = table.Column<bool>(nullable: false),
                    EnableFacebookLogin = table.Column<bool>(nullable: false),
                    TenantCreated = table.Column<bool>(nullable: false, defaultValue: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantApplications_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "identityiss",
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantApplication_TenantApplicationGuid",
                schema: "identityiss",
                table: "TenantApplications",
                column: "TenantApplicationGuid",
                unique: true)
                .Annotation("SqlServer:Include", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_TenantApplications_TenantId",
                schema: "identityiss",
                table: "TenantApplications",
                column: "TenantId",
                unique: true,
                filter: "[TenantId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantApplications",
                schema: "identityiss");
        }
    }
}
