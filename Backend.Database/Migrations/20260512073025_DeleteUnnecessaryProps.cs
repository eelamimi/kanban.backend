using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUnnecessaryProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "UserProfile");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Issue",
                newName: "IsClosed");

            migrationBuilder.RenameIndex(
                name: "IX_Issue_ProjectId_NumberInProject_IsDeleted",
                table: "Issue",
                newName: "IX_Issue_ProjectId_NumberInProject_IsClosed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsClosed",
                table: "Issue",
                newName: "IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Issue_ProjectId_NumberInProject_IsClosed",
                table: "Issue",
                newName: "IX_Issue_ProjectId_NumberInProject_IsDeleted");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "UserProfile",
                type: "character varying(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");
        }
    }
}
