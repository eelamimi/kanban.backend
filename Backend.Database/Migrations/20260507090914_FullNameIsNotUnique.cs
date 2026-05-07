using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class FullNameIsNotUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX__UserProfile__FullName",
                table: "UserProfile");

            migrationBuilder.CreateIndex(
                name: "IX__UserProfile__FullName",
                table: "UserProfile",
                columns: new[] { "FirstName", "SecondName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX__UserProfile__FullName",
                table: "UserProfile");

            migrationBuilder.CreateIndex(
                name: "IX__UserProfile__FullName",
                table: "UserProfile",
                columns: new[] { "FirstName", "SecondName" },
                unique: true);
        }
    }
}
