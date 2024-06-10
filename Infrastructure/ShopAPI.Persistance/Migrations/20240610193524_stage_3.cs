using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopAPI.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class stage_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "FileBases",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "FileBases",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "FileBases");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "FileBases");
        }
    }
}
