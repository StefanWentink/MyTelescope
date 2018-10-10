using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyTelescope.Api.DataLayer.Migrations
{
    public partial class CelestialObjectData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OppositionDistance",
                table: "CelestialObject",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "CelestialObjectPosition",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CelestialObjectId = table.Column<Guid>(nullable: false),
                    ReferenceDate = table.Column<DateTimeOffset>(nullable: false),
                    X = table.Column<double>(nullable: false),
                    Y = table.Column<double>(nullable: false),
                    Z = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelestialObjectPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CelestialObjectPosition_CelestialObject_CelestialObjectId",
                        column: x => x.CelestialObjectId,
                        principalTable: "CelestialObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CelestialObjectPosition_CelestialObjectId_ReferenceDate",
                table: "CelestialObjectPosition",
                columns: new[] { "CelestialObjectId", "ReferenceDate" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CelestialObjectPosition");

            migrationBuilder.DropColumn(
                name: "OppositionDistance",
                table: "CelestialObject");
        }
    }
}
