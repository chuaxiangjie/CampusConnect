using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CampusConnect.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Webpages = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Universities_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserUniversityBookmarks",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UniversityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUniversityBookmarks", x => new { x.UserId, x.UniversityId });
                    table.ForeignKey(
                        name: "FK_UserUniversityBookmarks_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "Name" },
                values: new object[,]
                {
                    { new Guid("48ed711d-1e45-4ec3-8008-f88de0e59f00"), "MYS", "Malaysia" },
                    { new Guid("8a3a2f5a-b820-4011-8cfc-3bf5a13c677b"), "SGP", "Singapore" }
                });

            migrationBuilder.InsertData(
                table: "Universities",
                columns: new[] { "Id", "CountryId", "Created", "DeletedAt", "IsActive", "IsDeleted", "LastModified", "Name", "Webpages" },
                values: new object[,]
                {
                    { new Guid("af4a11a6-b46b-4bd6-895b-8f71ed917cb6"), new Guid("8a3a2f5a-b820-4011-8cfc-3bf5a13c677b"), new DateTime(2024, 9, 7, 15, 29, 44, 385, DateTimeKind.Utc).AddTicks(327), null, true, false, null, "Nanyang Technological University", "www.website2.com,www.register.website2.com" },
                    { new Guid("f43cd618-f71e-4c27-85df-81f0071f13cd"), new Guid("8a3a2f5a-b820-4011-8cfc-3bf5a13c677b"), new DateTime(2024, 9, 7, 15, 29, 44, 385, DateTimeKind.Utc).AddTicks(321), null, true, false, null, "National University of Singapore", "www.website1.com,www.register.website1.com" },
                    { new Guid("fcc0f4d8-52ca-410c-8ed1-191f12c0ccb8"), new Guid("48ed711d-1e45-4ec3-8008-f88de0e59f00"), new DateTime(2024, 9, 7, 15, 29, 44, 385, DateTimeKind.Utc).AddTicks(343), null, true, false, null, "Universiti Malaya", "www.website3.com,www.register.website3.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Universities_CountryId",
                table: "Universities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUniversityBookmarks_UniversityId",
                table: "UserUniversityBookmarks",
                column: "UniversityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUniversityBookmarks");

            migrationBuilder.DropTable(
                name: "Universities");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
