using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class CreatorAndTeamOfProjectIsNotUniqueSeparately : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX__Project__CreatorId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX__Project__TeamId",
                table: "Project");

            migrationBuilder.CreateIndex(
                name: "IX__Project__CreatorId",
                table: "Project",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX__Project__TeamId",
                table: "Project",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX__Project__CreatorId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX__Project__TeamId",
                table: "Project");

            migrationBuilder.CreateIndex(
                name: "IX__Project__CreatorId",
                table: "Project",
                column: "CreatorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__Project__TeamId",
                table: "Project",
                column: "TeamId",
                unique: true);
        }
    }
}
