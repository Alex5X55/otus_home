using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class PartnerPromoCodeLimitForeignKeyExcange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartnerPromoCodeLimits_Partners_PartnerId1",
                table: "PartnerPromoCodeLimits");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Partners_Name",
                table: "Partners");

            migrationBuilder.DropIndex(
                name: "IX_PartnerPromoCodeLimits_PartnerId1",
                table: "PartnerPromoCodeLimits");

            migrationBuilder.DropColumn(
                name: "PartnerName",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "PartnerId1",
                table: "PartnerPromoCodeLimits");

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerId",
                table: "PromoCodes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodes_PartnerId",
                table: "PromoCodes",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_Partners_PartnerId",
                table: "PromoCodes",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_Partners_PartnerId",
                table: "PromoCodes");

            migrationBuilder.DropIndex(
                name: "IX_PromoCodes_PartnerId",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "PromoCodes");

            migrationBuilder.AddColumn<string>(
                name: "PartnerName",
                table: "PromoCodes",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerId1",
                table: "PartnerPromoCodeLimits",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Partners_Name",
                table: "Partners",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPromoCodeLimits_PartnerId1",
                table: "PartnerPromoCodeLimits",
                column: "PartnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerPromoCodeLimits_Partners_PartnerId1",
                table: "PartnerPromoCodeLimits",
                column: "PartnerId1",
                principalTable: "Partners",
                principalColumn: "Id");
        }
    }
}
