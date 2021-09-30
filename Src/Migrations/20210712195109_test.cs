using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aduaba.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "OrderSimis",
                columns: table => new
                {
                    OrderId = table.Column<string>(nullable: false),
                    TotalNoOfCartItem = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    PaystackRefNo = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    StatusOfDelivery = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSimis", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderSimis_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderSimis_ApplicationUserId",
                table: "OrderSimis",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderSimis");

            
        }
    }
}
