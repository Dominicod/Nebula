using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nebula.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddActionItemType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionItemTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionItemTypes", x => x.Id);
                });

            migrationBuilder.AddColumn<Guid>(
                name: "ActionItemTypeId",
                table: "ActionItems",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.InsertData(
                table: "ActionItemTypes",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Home", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b2c3d4e5-f6a7-8901-bcde-f12345678901"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Work", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionItems_ActionItemTypeId",
                table: "ActionItems",
                column: "ActionItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionItems_ActionItemTypes_ActionItemTypeId",
                table: "ActionItems",
                column: "ActionItemTypeId",
                principalTable: "ActionItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionItems_ActionItemTypes_ActionItemTypeId",
                table: "ActionItems");

            migrationBuilder.DropIndex(
                name: "IX_ActionItems_ActionItemTypeId",
                table: "ActionItems");

            migrationBuilder.DropColumn(
                name: "ActionItemTypeId",
                table: "ActionItems");

            migrationBuilder.DropTable(
                name: "ActionItemTypes");
        }
    }
}
