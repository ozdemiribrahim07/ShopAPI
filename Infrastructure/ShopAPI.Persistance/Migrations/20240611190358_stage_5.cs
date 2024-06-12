using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopAPI.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class stage_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "FileBases",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileBases_ProductId",
                table: "FileBases",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileBases_Products_ProductId",
                table: "FileBases",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileBases_Products_ProductId",
                table: "FileBases");

            migrationBuilder.DropIndex(
                name: "IX_FileBases_ProductId",
                table: "FileBases");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "FileBases");
        }
    }
}
