using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixCommentaryIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX__Commentary__AuthorId",
                table: "Commentary");

            migrationBuilder.DropIndex(
                name: "IX__Commentary__IssueId",
                table: "Commentary");

            migrationBuilder.CreateIndex(
                name: "IX__Commentary__AuthorId",
                table: "Commentary",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX__Commentary__IssueId",
                table: "Commentary",
                column: "IssueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX__Commentary__AuthorId",
                table: "Commentary");

            migrationBuilder.DropIndex(
                name: "IX__Commentary__IssueId",
                table: "Commentary");

            migrationBuilder.CreateIndex(
                name: "IX__Commentary__AuthorId",
                table: "Commentary",
                column: "AuthorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__Commentary__IssueId",
                table: "Commentary",
                column: "IssueId",
                unique: true);
        }
    }
}
