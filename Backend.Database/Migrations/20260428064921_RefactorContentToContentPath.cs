using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class RefactorContentToContentPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Attachment");

            migrationBuilder.AddColumn<string>(
                name: "ContentPath",
                table: "Attachment",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentPath",
                table: "Attachment");

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "Attachment",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
