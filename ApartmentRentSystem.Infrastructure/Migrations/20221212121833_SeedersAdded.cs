using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApartmentRentSystem.Infrastructure.Migrations
{
    public partial class SeedersAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, "fb0ba65a-3167-4598-a778-7d1a60dba5f6", "guest@mail.com", false, false, null, "guest@mail.com", "guest@mail.com", "AQAAAAEAACcQAAAAEBRJI7e3ZNAbAz2V8fZFKykS/N0ptYRTAm5rGLmtsDz4miKql0G8lEwFhJk2QfLGww==", null, false, "97bc7d9d-aa8b-4c00-969e-8b59a355d0bb", false, "guest@mail.com" },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "7a53ce77-6b4e-49e2-a71d-ef50cc0ef559", "agent@mail.com", false, false, null, "agent@mail.com", "agent@mail.com", "AQAAAAEAACcQAAAAEJiXtdgByIKeewWobh2/zGhiPY15KTV4deIYdxjUxRDgIWEFyipuG9YbfdIZBpycFA==", null, false, "0de3f291-deaf-4b79-94ca-042687238446", false, "agent@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Single-Room" },
                    { 2, "Double" },
                    { 3, "Superior" },
                    { 4, "Studio" },
                    { 5, "Deluxe" }
                });

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { 1, "+359888888888", "dea12856-c198-4129-b3f3-b893d8395082" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");
        }
    }
}
