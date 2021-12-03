using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Finance_Tracker.Migrations
{
    public partial class secondmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_InvoiceTypes_InvoiceTypeId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Partners_PartnerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Projects_ProjectId",
                table: "Invoices");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "Invoices",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Invoices",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PartnerId",
                table: "Invoices",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "InvoiceTypeId",
                table: "Invoices",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Invoices",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_InvoiceTypes_InvoiceTypeId",
                table: "Invoices",
                column: "InvoiceTypeId",
                principalTable: "InvoiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Partners_PartnerId",
                table: "Invoices",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Projects_ProjectId",
                table: "Invoices",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_InvoiceTypes_InvoiceTypeId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Partners_PartnerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Projects_ProjectId",
                table: "Invoices");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PartnerId",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InvoiceTypeId",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_InvoiceTypes_InvoiceTypeId",
                table: "Invoices",
                column: "InvoiceTypeId",
                principalTable: "InvoiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Partners_PartnerId",
                table: "Invoices",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Projects_ProjectId",
                table: "Invoices",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
