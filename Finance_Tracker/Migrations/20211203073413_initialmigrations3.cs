using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Finance_Tracker.Migrations
{
    public partial class initialmigrations3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostInvoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostInvoices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CostInvoiceId",
                table: "Invoices",
                column: "CostInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_CostInvoices_CostInvoiceId",
                table: "Invoices",
                column: "CostInvoiceId",
                principalTable: "CostInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_CostInvoices_CostInvoiceId",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "CostInvoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CostInvoiceId",
                table: "Invoices");
        }
    }
}
