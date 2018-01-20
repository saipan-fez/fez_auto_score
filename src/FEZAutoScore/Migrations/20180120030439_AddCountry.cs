using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FEZAutoScore.Migrations
{
    public partial class AddCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "攻撃側国名",
                table: "Score",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "防衛側国名",
                table: "Score",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "攻撃側国名",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "防衛側国名",
                table: "Score");
        }
    }
}
