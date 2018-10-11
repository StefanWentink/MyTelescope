using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTelescope.Api.DataLayer.Migrations
{
    public partial class CelestialObjectDataExpand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Declination",
                table: "CelestialObjectPosition",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "EclipticLatitude",
                table: "CelestialObjectPosition",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "EclipticLongitude",
                table: "CelestialObjectPosition",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LargeDeltaEarthDistance",
                table: "CelestialObjectPosition",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RightAscension",
                table: "CelestialObjectPosition",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Declination",
                table: "CelestialObjectPosition");

            migrationBuilder.DropColumn(
                name: "EclipticLatitude",
                table: "CelestialObjectPosition");

            migrationBuilder.DropColumn(
                name: "EclipticLongitude",
                table: "CelestialObjectPosition");

            migrationBuilder.DropColumn(
                name: "LargeDeltaEarthDistance",
                table: "CelestialObjectPosition");

            migrationBuilder.DropColumn(
                name: "RightAscension",
                table: "CelestialObjectPosition");
        }
    }
}