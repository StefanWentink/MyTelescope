using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyTelescope.Api.DataLayer.Migrations
{
    public partial class CelestialObjectDataOrderExpand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CelestialObject",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CelestialObject_Order",
                table: "CelestialObject",
                column: "Order",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CelestialObject_Order",
                table: "CelestialObject");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CelestialObject");
        }
    }
}
