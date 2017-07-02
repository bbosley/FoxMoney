using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoxMoney.Migrations
{
    public partial class AddSoldCostBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SoldCostBase",
                table: "Portfolios",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSoldCostBase",
                table: "Holding",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoldCostBase",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "TotalSoldCostBase",
                table: "Holding");
        }
    }
}
