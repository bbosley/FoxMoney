using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FoxMoney.Migrations
{
    public partial class AllCurrentEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Active = table.Column<bool>(nullable: false, defaultValue: true),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    CostBase = table.Column<decimal>(nullable: false),
                    CurrentIncome = table.Column<decimal>(nullable: false),
                    CurrentRawValue = table.Column<decimal>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portfolios_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Active = table.Column<bool>(nullable: false, defaultValue: true),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    YahooCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecurityPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ClosingPrice = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    SecurityId = table.Column<long>(nullable: false),
                    SecurityId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityPrices_Security_SecurityId1",
                        column: x => x.SecurityId1,
                        principalTable: "Security",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Holding",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    CustomName = table.Column<string>(nullable: true),
                    HoldingClosed = table.Column<bool>(nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    PortfolioId = table.Column<long>(nullable: false),
                    PortfolioId1 = table.Column<int>(nullable: true),
                    SecurityId = table.Column<int>(nullable: true),
                    SecurityPriceId = table.Column<int>(nullable: true),
                    TotalCostBase = table.Column<decimal>(nullable: false),
                    TotalUnits = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holding_Portfolios_PortfolioId1",
                        column: x => x.PortfolioId1,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Holding_Security_SecurityId",
                        column: x => x.SecurityId,
                        principalTable: "Security",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Holding_SecurityPrices_SecurityPriceId",
                        column: x => x.SecurityPriceId,
                        principalTable: "SecurityPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parcel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    Brokerage = table.Column<decimal>(nullable: false),
                    HoldingId = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    ParcelExhausted = table.Column<bool>(nullable: false, defaultValue: false),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    PurchasePrice = table.Column<decimal>(nullable: false),
                    UnitsPurchased = table.Column<int>(nullable: false),
                    UnitsSold = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcel_Holding_HoldingId",
                        column: x => x.HoldingId,
                        principalTable: "Holding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoldingTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    Brokerage = table.Column<decimal>(nullable: false),
                    GeneratedParcelId = table.Column<long>(nullable: false),
                    GeneratedParcelId1 = table.Column<int>(nullable: true),
                    HoldingId = table.Column<long>(nullable: false),
                    HoldingId1 = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    TransactionType = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    Units = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoldingTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoldingTransaction_Parcel_GeneratedParcelId1",
                        column: x => x.GeneratedParcelId1,
                        principalTable: "Parcel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoldingTransaction_Holding_HoldingId1",
                        column: x => x.HoldingId1,
                        principalTable: "Holding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoldingIncome",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    HoldingId = table.Column<long>(nullable: false),
                    HoldingId1 = table.Column<int>(nullable: true),
                    Income = table.Column<decimal>(nullable: false),
                    IncomeDate = table.Column<DateTime>(nullable: false),
                    IncomeReinvested = table.Column<bool>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    ReinvestmentHoldingTransactionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoldingIncome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoldingIncome_Holding_HoldingId1",
                        column: x => x.HoldingId1,
                        principalTable: "Holding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoldingIncome_HoldingTransaction_ReinvestmentHoldingTransactionId",
                        column: x => x.ReinvestmentHoldingTransactionId,
                        principalTable: "HoldingTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Holding_PortfolioId1",
                table: "Holding",
                column: "PortfolioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Holding_SecurityId",
                table: "Holding",
                column: "SecurityId");

            migrationBuilder.CreateIndex(
                name: "IX_Holding_SecurityPriceId",
                table: "Holding",
                column: "SecurityPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_HoldingIncome_HoldingId1",
                table: "HoldingIncome",
                column: "HoldingId1");

            migrationBuilder.CreateIndex(
                name: "IX_HoldingIncome_ReinvestmentHoldingTransactionId",
                table: "HoldingIncome",
                column: "ReinvestmentHoldingTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_HoldingTransaction_GeneratedParcelId1",
                table: "HoldingTransaction",
                column: "GeneratedParcelId1");

            migrationBuilder.CreateIndex(
                name: "IX_HoldingTransaction_HoldingId1",
                table: "HoldingTransaction",
                column: "HoldingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Parcel_HoldingId",
                table: "Parcel",
                column: "HoldingId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_OwnerId",
                table: "Portfolios",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityPrices_Date",
                table: "SecurityPrices",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityPrices_SecurityId1",
                table: "SecurityPrices",
                column: "SecurityId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoldingIncome");

            migrationBuilder.DropTable(
                name: "HoldingTransaction");

            migrationBuilder.DropTable(
                name: "Parcel");

            migrationBuilder.DropTable(
                name: "Holding");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropTable(
                name: "SecurityPrices");

            migrationBuilder.DropTable(
                name: "Security");
        }
    }
}
