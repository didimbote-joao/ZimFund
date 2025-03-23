using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZimFund.Migrations
{
    public partial class AddAddressToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36ed8abc-e2cf-422d-92f1-85e6f60a8e44");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fea12ab-d5c0-48bf-9836-856fea38e90e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf6054fb-5303-4310-9481-6d436245f6aa");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "36ed8abc-e2cf-422d-92f1-85e6f60a8e44", "eaf3e3af-d585-4b56-85fd-a71cbad2593e", "client", "client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6fea12ab-d5c0-48bf-9836-856fea38e90e", "df6a6c61-e367-465b-88be-086705294c51", "organizer", "organizer" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cf6054fb-5303-4310-9481-6d436245f6aa", "8bdb8702-a21f-4b3e-a6f1-121930677f23", "admin", "admin" });
        }
    }
}
