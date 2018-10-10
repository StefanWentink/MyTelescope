using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyTelescope.Api.DataLayer.Migrations
{
    public partial class CelestialObjectPositionCalculationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MeanAnomaly",
                table: "CelestialObjectPosition",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeanAnomaly",
                table: "CelestialObjectPosition");
        }
    }
}
