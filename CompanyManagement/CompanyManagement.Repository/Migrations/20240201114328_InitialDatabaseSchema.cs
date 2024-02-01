using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyManagement.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "industry",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    industry_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_industry", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_no = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    company_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    industry_id = table.Column<int>(type: "integer", nullable: false),
                    TotalEmployees = table.Column<int>(type: "integer", nullable: false),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    parent_company = table.Column<string>(type: "text", nullable: false, defaultValue: "None"),
                    level = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.id);
                    table.ForeignKey(
                        name: "FK_company_industry_industry_id",
                        column: x => x.industry_id,
                        principalTable: "industry",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "industry",
                columns: new[] { "id", "industry_name" },
                values: new object[,]
                {
                    { 1, "Meat processing" },
                    { 2, "Gardening and landscaping" },
                    { 3, "IT services" },
                    { 4, "Aerospace technology" },
                    { 5, "Consumer electronics" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_company_industry_id",
                table: "company",
                column: "industry_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "industry");
        }
    }
}
