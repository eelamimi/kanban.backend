using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class IssueAssigneeAndAuthorNotUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX__Issue__AssigneeId",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX__Issue__AuthorId",
                table: "Issue");

            migrationBuilder.CreateIndex(
                name: "IX__Issue__AssigneeId",
                table: "Issue",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX__Issue__AuthorId",
                table: "Issue",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX__Issue__AssigneeId",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX__Issue__AuthorId",
                table: "Issue");

            migrationBuilder.CreateIndex(
                name: "IX__Issue__AssigneeId",
                table: "Issue",
                column: "AssigneeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__Issue__AuthorId",
                table: "Issue",
                column: "AuthorId",
                unique: true);
        }
    }
}
