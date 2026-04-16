using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnRelationsForeignKeysBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColumnRelation_Column_NextColumnId",
                table: "ColumnRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ColumnRelation_Column_PrevColumnId",
                table: "ColumnRelation");

            migrationBuilder.AddForeignKey(
                name: "FK_ColumnRelation_Column_NextColumnId",
                table: "ColumnRelation",
                column: "NextColumnId",
                principalTable: "Column",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ColumnRelation_Column_PrevColumnId",
                table: "ColumnRelation",
                column: "PrevColumnId",
                principalTable: "Column",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColumnRelation_Column_NextColumnId",
                table: "ColumnRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ColumnRelation_Column_PrevColumnId",
                table: "ColumnRelation");

            migrationBuilder.AddForeignKey(
                name: "FK_ColumnRelation_Column_NextColumnId",
                table: "ColumnRelation",
                column: "NextColumnId",
                principalTable: "Column",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ColumnRelation_Column_PrevColumnId",
                table: "ColumnRelation",
                column: "PrevColumnId",
                principalTable: "Column",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
