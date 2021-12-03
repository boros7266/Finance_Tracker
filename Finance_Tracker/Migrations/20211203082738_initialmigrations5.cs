using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Finance_Tracker.Migrations
{
    public partial class initialmigrations5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Invoices",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CostInvoiceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companys_CostInvoices_CostInvoiceId",
                        column: x => x.CostInvoiceId,
                        principalTable: "CostInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CompanyId",
                table: "Invoices",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Companys_CostInvoiceId",
                table: "Companys",
                column: "CostInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Companys_CompanyId",
                table: "Invoices",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Companys_CompanyId",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "Companys");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CompanyId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Invoices");
        }
    }
}
