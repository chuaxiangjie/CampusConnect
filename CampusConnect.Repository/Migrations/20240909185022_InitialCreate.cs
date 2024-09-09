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
                    CountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                    { new Guid("af2c2357-0c2d-42fe-ab8c-a8000d6c32e1"), "SGP", "Singapore" },
                    { new Guid("caf8f361-237b-43c6-bdd9-5444595a5910"), "MYS", "Malaysia" }
                });

            migrationBuilder.InsertData(
                table: "Universities",
                columns: new[] { "Id", "CountryId", "Created", "DeletedAt", "IsActive", "IsDeleted", "LastModified", "Name", "Webpages" },
                values: new object[,]
                {
                    { new Guid("7cf69bce-952f-4209-b919-da909c86671f"), new Guid("caf8f361-237b-43c6-bdd9-5444595a5910"), new DateTime(2024, 9, 9, 18, 50, 22, 78, DateTimeKind.Utc).AddTicks(4122), null, true, false, null, "Universiti Malaya", "htp://www.website3.com,http://www.register.website3.com" },
                    { new Guid("c2ec79fc-04a6-49b8-8f7c-eb3adec9f771"), new Guid("af2c2357-0c2d-42fe-ab8c-a8000d6c32e1"), new DateTime(2024, 9, 9, 18, 50, 22, 78, DateTimeKind.Utc).AddTicks(4115), null, true, false, null, "National University of Singapore", "http://www.website1.com,http://www.register.website1.com" },
                    { new Guid("c9f4b1db-2e16-452b-b401-52e1f27c8840"), new Guid("af2c2357-0c2d-42fe-ab8c-a8000d6c32e1"), new DateTime(2024, 9, 9, 18, 50, 22, 78, DateTimeKind.Utc).AddTicks(4119), null, true, false, null, "Nanyang Technological University", "http://www.website2.com,http://www.register.website2.com" }
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
