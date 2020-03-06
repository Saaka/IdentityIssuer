using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class AddUserAvatarsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "SelectedAvatarType",
                schema: "identityiss",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: (byte)1);

            migrationBuilder.CreateTable(
                name: "UserAvatars",
                schema: "identityiss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantUserId = table.Column<int>(nullable: false),
                    AvatarType = table.Column<byte>(nullable: false),
                    Url = table.Column<string>(maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAvatars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAvatars_AspNetUsers_TenantUserId",
                        column: x => x.TenantUserId,
                        principalSchema: "identityiss",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantUserId_AvatarType",
                schema: "identityiss",
                table: "UserAvatars",
                columns: new[] { "TenantUserId", "AvatarType" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAvatars",
                schema: "identityiss");

            migrationBuilder.DropColumn(
                name: "SelectedAvatarType",
                schema: "identityiss",
                table: "AspNetUsers");
        }
    }
}
