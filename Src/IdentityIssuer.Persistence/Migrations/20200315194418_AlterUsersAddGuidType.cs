using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityIssuer.Persistence.Migrations
{
    public partial class AlterUsersAddGuidType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserGuid",
                schema: "identityiss",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserGuid",
                schema: "identityiss",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserGuid",
                schema: "identityiss",
                table: "AspNetUsers",
                column: "UserGuid",
                unique: true)
                .Annotation("SqlServer:Include", new[] { "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserGuid",
                schema: "identityiss",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserGuid",
                schema: "identityiss",
                table: "AspNetUsers",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_UserGuid",
                schema: "identityiss",
                table: "AspNetUsers",
                column: "UserGuid",
                unique: true);
        }
    }
}
