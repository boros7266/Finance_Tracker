using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Finance_Tracker.Migrations
{
    public partial class initialmigrations4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_CostInvoices_CostInvoiceId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CostInvoiceId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CostInvoiceId",
                table: "Invoices");

            migrationBuilder.AddColumn<Guid>(
                name: "CostInvoiceId",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CostInvoiceId",
                table: "Projects",
                column: "CostInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_CostInvoices_CostInvoiceId",
                table: "Projects",
                column: "CostInvoiceId",
                principalTable: "CostInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_CostInvoices_CostInvoiceId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CostInvoiceId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CostInvoiceId",
                table: "Projects");

            migrationBuilder.AddColumn<Guid>(
                name: "CostInvoiceId",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
