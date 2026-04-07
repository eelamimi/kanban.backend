using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class IssueProjectRelationAndSoftDeleteAndIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Issue");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "Issue",
                newName: "IssuePriority");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Issue",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberInProject",
                table: "Issue",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Issue",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Attachment",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8);

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "Attachment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Issue_ProjectId_NumberInProject_IsDeleted",
                table: "Issue",
                columns: new[] { "ProjectId", "NumberInProject", "IsDeleted" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Project_ProjectId",
                table: "Issue",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Project_ProjectId",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Issue_ProjectId_NumberInProject_IsDeleted",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "NumberInProject",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Attachment");

            migrationBuilder.RenameColumn(
                name: "IssuePriority",
                table: "Issue",
                newName: "Priority");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Issue",
                type: "character varying(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Attachment",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);
        }
    }
}
