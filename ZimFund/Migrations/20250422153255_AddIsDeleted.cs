using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZimFund.Migrations
{
    public partial class AddIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Donations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "16216224-39bc-4136-8d9b-4688fd62211d", "9df893cd-8896-4d48-b14a-d0651845161b", "organizer", "organizer" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "65bad67f-3ef5-49e1-909c-f1050cda4f25", "c9cbf43e-de78-4002-8cf9-c94b84589fb6", "client", "client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9af407ec-36fc-42a5-b5f5-c79bee3fa210", "2603f704-605f-4831-8233-d81be6f14540", "admin", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16216224-39bc-4136-8d9b-4688fd62211d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65bad67f-3ef5-49e1-909c-f1050cda4f25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9af407ec-36fc-42a5-b5f5-c79bee3fa210");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

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
    }
}
