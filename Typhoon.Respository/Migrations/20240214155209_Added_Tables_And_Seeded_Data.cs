using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Typhoon.Respository.Migrations
{
    /// <inheritdoc />
    public partial class Added_Tables_And_Seeded_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a5c5e394-6007-4930-af25-b1a4e06f0f85", null, "User", "USER" },
                    { "ea6cdca9-7993-43f0-89e8-f3bae3cf1d8e", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Deleted", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 14, 18, 52, 9, 584, DateTimeKind.Local).AddTicks(8703), false, "Television Category", "Television", null },
                    { 2, new DateTime(2024, 2, 14, 18, 52, 9, 584, DateTimeKind.Local).AddTicks(8705), false, "Mobile Phone Category", "Mobile Phone", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "CreatedDate", "Deleted", "Description", "Name", "Price", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Samsung", 2, new DateTime(2024, 2, 14, 18, 52, 9, 584, DateTimeKind.Local).AddTicks(8849), false, "Samsung Galaxy A04s 64 GB 4 GB Ram (Samsung Türkiye Garantili)", "Samsung Galaxy A04S", 5300m, null },
                    { 2, "Samsung", 2, new DateTime(2024, 2, 14, 18, 52, 9, 584, DateTimeKind.Local).AddTicks(8852), false, "Samsung Galaxy A54 256 GB 8 GB Ram (Samsung Türkiye Garantili)", "Samsung Galaxy A54", 16660m, null },
                    { 3, "Samsung", 2, new DateTime(2024, 2, 14, 18, 52, 9, 584, DateTimeKind.Local).AddTicks(8854), false, "Samsung Galaxy S24 Ultra 512 GB 12 GB Ram (Samsung Türkiye Garantili)", "Samsung Galaxy S24 Ultra", 69999m, null },
                    { 4, "Samsung", 2, new DateTime(2024, 2, 14, 18, 52, 9, 584, DateTimeKind.Local).AddTicks(8856), false, "Samsung Galaxy S23 Ultra 512 GB 12 GB Ram (Samsung Türkiye Garantili)", "Samsung Galaxy S23 Ultra", 57599m, null },
                    { 5, "Apple", 2, new DateTime(2024, 2, 14, 18, 52, 9, 584, DateTimeKind.Local).AddTicks(8858), false, "iPhone 15 Pro Max 512 GB", "iPhone 15 Pro", 85699m, null },
                    { 6, "Apple", 2, new DateTime(2024, 2, 14, 18, 52, 9, 584, DateTimeKind.Local).AddTicks(8860), false, "iPhone 13 Pro Max 512 GB", "iPhone 13 Pro Max", 74999m, null },
                    { 7, "Apple", 2, new DateTime(2024, 2, 14, 18, 52, 9, 584, DateTimeKind.Local).AddTicks(8862), false, "iPhone 14 Plus 128 GB", "iPhone 14 Plus", 48749m, null },
                    { 8, "Samsung", 1, new DateTime(2024, 2, 14, 18, 52, 9, 584, DateTimeKind.Local).AddTicks(8864), false, "Samsung 65QN85C 65\" 163 Ekran Uydu Alıcılı 4K Ultra HD Smart Neo QLED TV", "Samsung 65QN85C", 61099m, null },
                    { 9, "Samsung", 1, new DateTime(2024, 2, 14, 18, 52, 9, 584, DateTimeKind.Local).AddTicks(8865), false, "Samsung 55CU8000 55\" 138 Ekran Uydu Alıcılı Crystal 4K Ultra HD Smart LED TV", "Samsung 55CU8000", 25379m, null },
                    { 10, "LG", 1, new DateTime(2024, 2, 14, 18, 52, 9, 584, DateTimeKind.Local).AddTicks(8867), false, "LG OLED65CS3VA 65\" 165 Ekran Uydu Alıcılı 4K Ultra HD webOS Smart OLED TV ", "LG OLED65CS3VA", 69799m, null },
                    { 11, "LG", 1, new DateTime(2024, 2, 14, 18, 52, 9, 584, DateTimeKind.Local).AddTicks(8869), false, "LG 65QNED756 65\" 165 Ekran Uydu Alıcılı 4K Ultra HD webOS Smart QNED TV", "LG 65QNED756", 44513m, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
