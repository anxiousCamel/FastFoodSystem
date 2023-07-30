using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyProduct.Migrations
{
    public partial class preparationTimePaymentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "PreparationTime",
                table: "PaymentInfo",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreparationTime",
                table: "PaymentInfo");
        }
    }
}
