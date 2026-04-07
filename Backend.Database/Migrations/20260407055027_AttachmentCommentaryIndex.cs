using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class AttachmentCommentaryIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX__Attachment__CommentaryId",
                table: "Attachment");

            migrationBuilder.CreateIndex(
                name: "IX__Attachment__CommentaryId",
                table: "Attachment",
                column: "CommentaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX__Attachment__CommentaryId",
                table: "Attachment");

            migrationBuilder.CreateIndex(
                name: "IX__Attachment__CommentaryId",
                table: "Attachment",
                column: "CommentaryId",
                unique: true);
        }
    }
}
