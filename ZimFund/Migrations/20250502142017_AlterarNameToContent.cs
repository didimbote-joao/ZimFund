using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZimFund.Migrations
{
    public partial class AlterarNameToContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Comments",
                newName: "Content");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1f78cdce-adad-4ed9-a725-f987b2393a02", "4813cd82-49b0-44e3-84c8-127a43aad77e", "organizer", "organizer" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4264c144-0e98-4e85-a5d7-2dc306945feb", "c9d0a1a6-5336-49b8-a112-91f2cb9a51cf", "client", "client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "59e62e5a-d69a-4146-ab75-cf323d77f853", "e526462b-92ce-4fd1-a0bf-a9941bb06f59", "admin", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f78cdce-adad-4ed9-a725-f987b2393a02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4264c144-0e98-4e85-a5d7-2dc306945feb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59e62e5a-d69a-4146-ab75-cf323d77f853");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "Name");

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
    }
}
