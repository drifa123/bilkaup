using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Bilkaup.Migrations
{
    public partial class FixedWheelCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WheelID",
                table: "WheelCars",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WheelID",
                table: "WheelCars",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
