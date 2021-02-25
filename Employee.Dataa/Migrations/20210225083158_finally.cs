    using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee.Dataa.Migrations
{
    public partial class @finally : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_personModels_CompanyModels_CompanyModelsCompanyId",
                table: "personModels");

            migrationBuilder.DropIndex(
                name: "IX_personModels_CompanyModelsCompanyId",
                table: "personModels");

            migrationBuilder.DropColumn(
                name: "CompanyModelsCompanyId",
                table: "personModels");

            migrationBuilder.CreateIndex(
                name: "IX_personModels_CompanyId",
                table: "personModels",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_personModels_CompanyModels_CompanyId",
                table: "personModels",
                column: "CompanyId",
                principalTable: "CompanyModels",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_personModels_CompanyModels_CompanyId",
                table: "personModels");

            migrationBuilder.DropIndex(
                name: "IX_personModels_CompanyId",
                table: "personModels");

            migrationBuilder.AddColumn<int>(
                name: "CompanyModelsCompanyId",
                table: "personModels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_personModels_CompanyModelsCompanyId",
                table: "personModels",
                column: "CompanyModelsCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_personModels_CompanyModels_CompanyModelsCompanyId",
                table: "personModels",
                column: "CompanyModelsCompanyId",
                principalTable: "CompanyModels",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
