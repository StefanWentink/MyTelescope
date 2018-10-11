namespace MyTelescope.Api.DataLayer.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;

    public partial class CelestialObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CelestialObjectType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_CelestialObjectType", x => x.Id));

            migrationBuilder.CreateTable(
                name: "CelestialObject",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BlackBodyTemperature = table.Column<double>(nullable: false),
                    CelestialObjectTypeId = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Ellipticity = table.Column<double>(nullable: false),
                    EquatorialRadius = table.Column<double>(nullable: false),
                    EscapeVelocity = table.Column<double>(nullable: false),
                    Gravity = table.Column<double>(nullable: false),
                    InferiorOrbit = table.Column<bool>(nullable: false),
                    Mass = table.Column<double>(nullable: false),
                    MaximumApparentDiameter = table.Column<double>(nullable: false),
                    MaximumDistance = table.Column<double>(nullable: false),
                    MaximumVisualMagnitude = table.Column<double>(nullable: false),
                    MeanDensity = table.Column<double>(nullable: false),
                    MinimumApparentDiameter = table.Column<double>(nullable: false),
                    MinimumDistance = table.Column<double>(nullable: false),
                    PolarRadius = table.Column<double>(nullable: false),
                    RingSystem = table.Column<bool>(nullable: false),
                    Satellites = table.Column<int>(nullable: false),
                    SolarIrradiance = table.Column<double>(nullable: false),
                    SurfaceAcceleration = table.Column<double>(nullable: false),
                    TopographicRange = table.Column<double>(nullable: false),
                    Volume = table.Column<double>(nullable: false),
                    VolumetricMeanRadius = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelestialObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CelestialObject_CelestialObjectType_CelestialObjectTypeId",
                        column: x => x.CelestialObjectTypeId,
                        principalTable: "CelestialObjectType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CelestialObject_CelestialObjectTypeId",
                table: "CelestialObject",
                column: "CelestialObjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CelestialObject_Code",
                table: "CelestialObject",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CelestialObjectType_Code",
                table: "CelestialObjectType",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CelestialObject");

            migrationBuilder.DropTable(
                name: "CelestialObjectType");
        }
    }
}