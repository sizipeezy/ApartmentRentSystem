using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApartmentRentSystem.Infrastructure.Migrations
{
    public partial class AppUpdateEntitiesAndShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.ApartmentId });
                    table.ForeignKey(
                        name: "FK_OrderItems_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96df6307-68b5-4113-9257-3c9ef12e8caa", "AQAAAAEAACcQAAAAEBxKmvSPRobYh0HvCEf5EvsTEpCrg9VvDHgs4a0VYd+UnzXiMPsprsimbNB7sDIoDg==", "d80b634f-7a01-462b-8897-c2e76a4553d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07d849c0-51aa-46f5-8deb-c80e1667f9c0", "AQAAAAEAACcQAAAAEFaCyp2d7RXSG2pPL2JVoIDLSyXREsGwBzOYXRJUpWN3DA4tHM+1kjWzLk15g1neHA==", "0ec84042-468a-47aa-a663-67e6f7480a55" });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ApartmentId",
                table: "Items",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ApartmentId",
                table: "OrderItems",
                column: "ApartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3247aff8-1ac8-4d63-88b8-6c9d78a86af5", "AQAAAAEAACcQAAAAEAuu+C9apu8y0ym+DY2yTBA3+gfQ2t1zkAMPFKJMdyXKkocpumrNaO6mFKl1TukKpQ==", "44c370a1-6204-4360-b77c-94b9461b8aa5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b10cbf6-2b31-40a1-a9c2-885d20a8c350", "AQAAAAEAACcQAAAAEA13eNjNES5FCC7f768Zipd8RQszkNcyhyoIcCsmlB0mWVxlNZtREiKNN8LBEZJ3xw==", "2463ac8c-27dd-4046-a873-5163480791ef" });
        }
    }
}
