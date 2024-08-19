using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Partners_And_PartnerPromoCodeLimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    NumberIssuedPromoCodes = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                    table.UniqueConstraint("AK_Partners_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "PartnerPromoCodeLimits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PartnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CancelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Limit = table.Column<int>(type: "INTEGER", nullable: false),
                    PartnerId1 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerPromoCodeLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerPromoCodeLimits_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartnerPromoCodeLimits_Partners_PartnerId1",
                        column: x => x.PartnerId1,
                        principalTable: "Partners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPromoCodeLimits_PartnerId",
                table: "PartnerPromoCodeLimits",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPromoCodeLimits_PartnerId1",
                table: "PartnerPromoCodeLimits",
                column: "PartnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_Name",
                table: "Partners",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartnerPromoCodeLimits");

            migrationBuilder.DropTable(
                name: "Partners");
        }
    }
}
