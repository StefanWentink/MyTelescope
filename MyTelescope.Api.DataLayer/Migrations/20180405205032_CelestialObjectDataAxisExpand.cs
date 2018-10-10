using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyTelescope.Api.DataLayer.Migrations
{
    public partial class CelestialObjectDataAxisExpand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CelestialObject_Order",
                table: "CelestialObject");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CelestialObject");

            migrationBuilder.RenameColumn(
                name: "VolumetricMeanRadius",
                table: "CelestialObject",
                newName: "SynodicPeriod");

            migrationBuilder.RenameColumn(
                name: "Ellipticity",
                table: "CelestialObject",
                newName: "SiderealRotationPeriod");

            migrationBuilder.AddColumn<double>(
                name: "Aphelion",
                table: "CelestialObject",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "CelestialObjectId",
                table: "CelestialObject",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LengthOfDay",
                table: "CelestialObject",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OrbitalEccentricity",
                table: "CelestialObject",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Perihelion",
                table: "CelestialObject",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SemiMajorAxis",
                table: "CelestialObject",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SiderealOrbitPeriod",
                table: "CelestialObject",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_CelestialObject_CelestialObjectId",
                table: "CelestialObject",
                column: "CelestialObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_CelestialObject_CelestialObject_CelestialObjectId",
                table: "CelestialObject",
                column: "CelestialObjectId",
                principalTable: "CelestialObject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CelestialObject_CelestialObject_CelestialObjectId",
                table: "CelestialObject");

            migrationBuilder.DropIndex(
                name: "IX_CelestialObject_CelestialObjectId",
                table: "CelestialObject");

            migrationBuilder.DropColumn(
                name: "Aphelion",
                table: "CelestialObject");

            migrationBuilder.DropColumn(
                name: "CelestialObjectId",
                table: "CelestialObject");

            migrationBuilder.DropColumn(
                name: "LengthOfDay",
                table: "CelestialObject");

            migrationBuilder.DropColumn(
                name: "OrbitalEccentricity",
                table: "CelestialObject");

            migrationBuilder.DropColumn(
                name: "Perihelion",
                table: "CelestialObject");

            migrationBuilder.DropColumn(
                name: "SemiMajorAxis",
                table: "CelestialObject");

            migrationBuilder.DropColumn(
                name: "SiderealOrbitPeriod",
                table: "CelestialObject");

            migrationBuilder.RenameColumn(
                name: "SynodicPeriod",
                table: "CelestialObject",
                newName: "VolumetricMeanRadius");

            migrationBuilder.RenameColumn(
                name: "SiderealRotationPeriod",
                table: "CelestialObject",
                newName: "Ellipticity");

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
    }
}
