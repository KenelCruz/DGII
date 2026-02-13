using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DGII.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FinalCleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Taxpayers",
                columns: table => new
                {
                    RncCedula = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxpayers", x => x.RncCedula);
                });

            migrationBuilder.CreateTable(
                name: "TaxReceipt",
                columns: table => new
                {
                    NCF = table.Column<string>(type: "TEXT", nullable: false),
                    RncCedula = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Itbis18 = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxReceipt", x => x.NCF);
                    table.ForeignKey(
                        name: "FK_TaxReceipt_Taxpayers_RncCedula",
                        column: x => x.RncCedula,
                        principalTable: "Taxpayers",
                        principalColumn: "RncCedula",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Taxpayers",
                columns: new[] { "RncCedula", "CreationDate", "Name", "Status", "Type", "UpdateDate" },
                values: new object[,]
                {
                    { "123456789", new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "FARMACIA TU SALUD", 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "98754321012", new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "JUAN PEREZ", 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "TaxReceipt",
                columns: new[] { "NCF", "Amount", "Itbis18", "RncCedula" },
                values: new object[,]
                {
                    { "E310000000001", 200.00m, 36.00m, "98754321012" },
                    { "E310000000002", 1000.00m, 180.00m, "98754321012" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxReceipt_RncCedula",
                table: "TaxReceipt",
                column: "RncCedula");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxReceipt");

            migrationBuilder.DropTable(
                name: "Taxpayers");
        }
    }
}
