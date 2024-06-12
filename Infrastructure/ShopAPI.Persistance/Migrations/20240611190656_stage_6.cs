using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopAPI.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class stage_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ProductProductImageFile",
                columns: table => new
                {
                    ProductImageFilesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductImageFile", x => new { x.ProductImageFilesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductProductImageFile_FileBases_ProductImageFilesId",
                        column: x => x.ProductImageFilesId,
                        principalTable: "FileBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductImageFile_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductImageFile_ProductsId",
                table: "ProductProductImageFile",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProductImageFile");

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
    }
}
