using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpfApp_D_NrimanProject.Migrations
{
    public partial class CreateTheDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(nullable: true),
                    OutTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Admin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InTime = table.Column<DateTime>(nullable: false),
                    OutTime = table.Column<DateTime>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    ItemType = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Sold = table.Column<bool>(nullable: false),
                    BillID = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    FoodType = table.Column<string>(nullable: true),
                    Width = table.Column<double>(nullable: true),
                    Capacity = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Items_Bills_BillID",
                        column: x => x.BillID,
                        principalTable: "Bills",
                        principalColumn: "BillID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_BillID",
                table: "Items",
                column: "BillID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Bills");
        }
    }
}
