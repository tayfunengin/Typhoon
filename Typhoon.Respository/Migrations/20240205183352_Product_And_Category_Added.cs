using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Typhoon.Respository.Migrations
{
    /// <inheritdoc />
    public partial class Product_And_Category_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Deleted", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 5, 21, 33, 51, 950, DateTimeKind.Local).AddTicks(7531), false, "Television Category", "Television", null },
                    { 2, new DateTime(2024, 2, 5, 21, 33, 51, 950, DateTimeKind.Local).AddTicks(7545), false, "Mobile Phone Category", "Mobile Phone", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "CreatedDate", "Deleted", "Description", "Name", "Price", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Samsung", 2, new DateTime(2024, 2, 5, 21, 33, 51, 950, DateTimeKind.Local).AddTicks(7681), false, "Samsung Galaxy A04s 64 GB 4 GB Ram (Samsung Türkiye Garantili)", "Samsung Galaxy A04S", 5300m, null },
                    { 2, "Samsung", 2, new DateTime(2024, 2, 5, 21, 33, 51, 950, DateTimeKind.Local).AddTicks(7688), false, "Samsung Galaxy A54 256 GB 8 GB Ram (Samsung Türkiye Garantili)", "Samsung Galaxy A54", 16660m, null },
                    { 3, "Samsung", 2, new DateTime(2024, 2, 5, 21, 33, 51, 950, DateTimeKind.Local).AddTicks(7689), false, "Samsung Galaxy S24 Ultra 512 GB 12 GB Ram (Samsung Türkiye Garantili)", "Samsung Galaxy S24 Ultra", 69999m, null },
                    { 4, "Samsung", 2, new DateTime(2024, 2, 5, 21, 33, 51, 950, DateTimeKind.Local).AddTicks(7691), false, "Samsung Galaxy S23 Ultra 512 GB 12 GB Ram (Samsung Türkiye Garantili)", "Samsung Galaxy S23 Ultra", 57599m, null },
                    { 5, "Apple", 2, new DateTime(2024, 2, 5, 21, 33, 51, 950, DateTimeKind.Local).AddTicks(7692), false, "iPhone 15 Pro Max 512 GB", "iPhone 15 Pro", 85699m, null },
                    { 6, "Apple", 2, new DateTime(2024, 2, 5, 21, 33, 51, 950, DateTimeKind.Local).AddTicks(7694), false, "iPhone 13 Pro Max 512 GB", "iPhone 13 Pro Max", 74999m, null },
                    { 7, "Apple", 2, new DateTime(2024, 2, 5, 21, 33, 51, 950, DateTimeKind.Local).AddTicks(7695), false, "iPhone 14 Plus 128 GB", "iPhone 14 Plus", 48749m, null },
                    { 8, "Samsung", 1, new DateTime(2024, 2, 5, 21, 33, 51, 950, DateTimeKind.Local).AddTicks(7696), false, "Samsung 65QN85C 65\" 163 Ekran Uydu Alıcılı 4K Ultra HD Smart Neo QLED TV", "Samsung 65QN85C", 61099m, null },
                    { 9, "Samsung", 1, new DateTime(2024, 2, 5, 21, 33, 51, 950, DateTimeKind.Local).AddTicks(7698), false, "Samsung 55CU8000 55\" 138 Ekran Uydu Alıcılı Crystal 4K Ultra HD Smart LED TV", "Samsung 55CU8000", 25379m, null },
                    { 10, "LG", 1, new DateTime(2024, 2, 5, 21, 33, 51, 950, DateTimeKind.Local).AddTicks(7699), false, "LG OLED65CS3VA 65\" 165 Ekran Uydu Alıcılı 4K Ultra HD webOS Smart OLED TV ", "LG OLED65CS3VA", 69799m, null },
                    { 11, "LG", 1, new DateTime(2024, 2, 5, 21, 33, 51, 950, DateTimeKind.Local).AddTicks(7701), false, "LG 65QNED756 65\" 165 Ekran Uydu Alıcılı 4K Ultra HD webOS Smart QNED TV", "LG 65QNED756", 44513m, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
