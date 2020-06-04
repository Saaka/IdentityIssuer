using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class AddLanguageAndMailTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "DefaultLanguage",
                schema: "identityiss",
                table: "TenantSettings",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "identityiss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailTemplates",
                schema: "identityiss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageType = table.Column<byte>(nullable: false),
                    Language = table.Column<byte>(nullable: false),
                    Template = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Language_Code",
                schema: "identityiss",
                table: "Languages",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Languages",
                schema: "identityiss");

            migrationBuilder.DropTable(
                name: "MailTemplates",
                schema: "identityiss");

            migrationBuilder.DropColumn(
                name: "DefaultLanguage",
                schema: "identityiss",
                table: "TenantSettings");
        }
    }
}
