using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoItem2.Model.Migrations
{
    public partial class ItemNewFieldsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Items",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DetetedDate",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DetetedDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Items");
        }
    }
}
