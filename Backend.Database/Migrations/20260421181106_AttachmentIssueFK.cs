using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class AttachmentIssueFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IssueId",
                table: "Attachment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_IssueId",
                table: "Attachment",
                column: "IssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Issue_IssueId",
                table: "Attachment",
                column: "IssueId",
                principalTable: "Issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Issue_IssueId",
                table: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_IssueId",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "Attachment");
        }
    }
}
