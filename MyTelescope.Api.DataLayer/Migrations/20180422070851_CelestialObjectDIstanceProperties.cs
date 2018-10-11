using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTelescope.Api.DataLayer.Migrations
{
    public partial class CelestialObjectDIstanceProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageCentricDistance",
                table: "CelestialObjectPosition",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CentricDistance",
                table: "CelestialObjectPosition",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageCentricDistance",
                table: "CelestialObjectPosition");

            migrationBuilder.DropColumn(
                name: "CentricDistance",
                table: "CelestialObjectPosition");
        }
    }
}