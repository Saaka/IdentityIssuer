using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class CreateTenantAllowedOriginsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenantAllowedOrigins",
                schema: "identityiss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(nullable: false),
                    AllowedOrigin = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantAllowedOrigins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantAllowedOrigins_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "identityiss",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantAllowedOrigins_TenantId",
                schema: "identityiss",
                table: "TenantAllowedOrigins",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantAllowedOrigins",
                schema: "identityiss");
        }
    }
}
