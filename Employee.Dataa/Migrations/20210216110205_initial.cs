using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Employee.Dataa.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyModels",
                columns: table => new
                {
                    CompanyId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyCity = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyModels", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "personModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    CompanyModelsCompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_personModels_CompanyModels_CompanyModelsCompanyId",
                        column: x => x.CompanyModelsCompanyId,
                        principalTable: "CompanyModels",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_personModels_CompanyModelsCompanyId",
                table: "personModels",
                column: "CompanyModelsCompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "personModels");

            migrationBuilder.DropTable(
                name: "CompanyModels");
        }
    }
}
