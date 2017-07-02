using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoxMoney.Migrations
{
    public partial class AddIdFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holding_Portfolios_PortfolioId1",
                table: "Holding");

            migrationBuilder.DropForeignKey(
                name: "FK_Holding_Security_SecurityId",
                table: "Holding");

            migrationBuilder.DropForeignKey(
                name: "FK_HoldingTransaction_Holding_HoldingId1",
                table: "HoldingTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_Holding_HoldingId",
                table: "Parcel");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityPrices_Security_SecurityId1",
                table: "SecurityPrices");

            migrationBuilder.DropIndex(
                name: "IX_SecurityPrices_SecurityId1",
                table: "SecurityPrices");

            migrationBuilder.DropIndex(
                name: "IX_HoldingTransaction_HoldingId1",
                table: "HoldingTransaction");

            migrationBuilder.DropIndex(
                name: "IX_Holding_PortfolioId1",
                table: "Holding");

            migrationBuilder.DropColumn(
                name: "SecurityId1",
                table: "SecurityPrices");

            migrationBuilder.DropColumn(
                name: "HoldingId1",
                table: "HoldingTransaction");

            migrationBuilder.DropColumn(
                name: "PortfolioId1",
                table: "Holding");

            migrationBuilder.AlterColumn<int>(
                name: "SecurityId",
                table: "SecurityPrices",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "HoldingId",
                table: "Parcel",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HoldingId",
                table: "HoldingTransaction",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "SecurityId",
                table: "Holding",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PortfolioId",
                table: "Holding",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_SecurityPrices_SecurityId",
                table: "SecurityPrices",
                column: "SecurityId");

            migrationBuilder.CreateIndex(
                name: "IX_HoldingTransaction_HoldingId",
                table: "HoldingTransaction",
                column: "HoldingId");

            migrationBuilder.CreateIndex(
                name: "IX_Holding_PortfolioId",
                table: "Holding",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holding_Portfolios_PortfolioId",
                table: "Holding",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holding_Security_SecurityId",
                table: "Holding",
                column: "SecurityId",
                principalTable: "Security",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoldingTransaction_Holding_HoldingId",
                table: "HoldingTransaction",
                column: "HoldingId",
                principalTable: "Holding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_Holding_HoldingId",
                table: "Parcel",
                column: "HoldingId",
                principalTable: "Holding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityPrices_Security_SecurityId",
                table: "SecurityPrices",
                column: "SecurityId",
                principalTable: "Security",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holding_Portfolios_PortfolioId",
                table: "Holding");

            migrationBuilder.DropForeignKey(
                name: "FK_Holding_Security_SecurityId",
                table: "Holding");

            migrationBuilder.DropForeignKey(
                name: "FK_HoldingTransaction_Holding_HoldingId",
                table: "HoldingTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_Holding_HoldingId",
                table: "Parcel");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityPrices_Security_SecurityId",
                table: "SecurityPrices");

            migrationBuilder.DropIndex(
                name: "IX_SecurityPrices_SecurityId",
                table: "SecurityPrices");

            migrationBuilder.DropIndex(
                name: "IX_HoldingTransaction_HoldingId",
                table: "HoldingTransaction");

            migrationBuilder.DropIndex(
                name: "IX_Holding_PortfolioId",
                table: "Holding");

            migrationBuilder.AlterColumn<long>(
                name: "SecurityId",
                table: "SecurityPrices",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "SecurityId1",
                table: "SecurityPrices",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HoldingId",
                table: "Parcel",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "HoldingId",
                table: "HoldingTransaction",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "HoldingId1",
                table: "HoldingTransaction",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SecurityId",
                table: "Holding",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "PortfolioId",
                table: "Holding",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PortfolioId1",
                table: "Holding",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecurityPrices_SecurityId1",
                table: "SecurityPrices",
                column: "SecurityId1");

            migrationBuilder.CreateIndex(
                name: "IX_HoldingTransaction_HoldingId1",
                table: "HoldingTransaction",
                column: "HoldingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Holding_PortfolioId1",
                table: "Holding",
                column: "PortfolioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Holding_Portfolios_PortfolioId1",
                table: "Holding",
                column: "PortfolioId1",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Holding_Security_SecurityId",
                table: "Holding",
                column: "SecurityId",
                principalTable: "Security",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HoldingTransaction_Holding_HoldingId1",
                table: "HoldingTransaction",
                column: "HoldingId1",
                principalTable: "Holding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_Holding_HoldingId",
                table: "Parcel",
                column: "HoldingId",
                principalTable: "Holding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityPrices_Security_SecurityId1",
                table: "SecurityPrices",
                column: "SecurityId1",
                principalTable: "Security",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
