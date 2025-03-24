using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZimFund.Migrations
{
    public partial class RemoveDatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "373df2ce-db59-42bb-872a-7bbf87b924f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55e31f4a-3d6f-4931-bb8f-9c9a95da163e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d673605-b614-42cc-9094-202caf6a2cee");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "05981a7c-992b-4e0d-96d5-f651d04480e7", "0141dd77-b12f-4e7c-a5c9-a2e4229ac5f4", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8f0cda67-3f52-4115-857f-77a4d315abd1", "183ba3c6-b243-4cbc-84a9-7a6504baf7c1", "client", "client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "afa51afe-90d9-425b-929d-4819133d8b5f", "59bb7684-27b3-40f0-b939-1095e4ffe866", "organizer", "organizer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05981a7c-992b-4e0d-96d5-f651d04480e7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f0cda67-3f52-4115-857f-77a4d315abd1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afa51afe-90d9-425b-929d-4819133d8b5f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "373df2ce-db59-42bb-872a-7bbf87b924f1", "4f722735-ff0d-4ed5-93e8-b9a20fe56392", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "55e31f4a-3d6f-4931-bb8f-9c9a95da163e", "30e5231d-0f96-44ed-a61b-c3ec447bdf61", "client", "client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9d673605-b614-42cc-9094-202caf6a2cee", "7b2165a3-79b8-4fe4-a028-e8c546cc0b2d", "organizer", "organizer" });
        }
    }
}
