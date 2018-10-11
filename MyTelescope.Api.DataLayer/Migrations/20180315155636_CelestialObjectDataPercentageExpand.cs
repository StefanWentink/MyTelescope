using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTelescope.Api.DataLayer.Migrations
{
    public partial class CelestialObjectDataPercentageExpand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RatioEarthAuDistance",
                table: "CelestialObjectPosition",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RatioSunEarthDistance",
                table: "CelestialObjectPosition",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatioEarthAuDistance",
                table: "CelestialObjectPosition");

            migrationBuilder.DropColumn(
                name: "RatioSunEarthDistance",
                table: "CelestialObjectPosition");
        }
    }
}