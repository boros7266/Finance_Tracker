using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Finance_Tracker.Migrations
{
    public partial class initialmigrations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CostInvoiceId",
                table: "Invoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostInvoiceId",
                table: "Invoices");
        }
    }
}
