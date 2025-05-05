using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZimFund.Migrations
{
    public partial class AddIsDeletedInComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4f59138d-b52c-4d10-b395-f689ff1be964", "0a5c87af-2957-4ff9-a3eb-5f2b453187fa", "organizer", "organizer" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "814367bf-8817-4c92-b9f0-fd93ec98ed6c", "b3eb694d-dc80-4277-9c31-5dc03a93f8e1", "client", "client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "da6deb3b-f92b-414d-b7cd-b97b843db195", "5fe2da50-6303-4a0c-998b-5bfcc7e89622", "admin", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f59138d-b52c-4d10-b395-f689ff1be964");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "814367bf-8817-4c92-b9f0-fd93ec98ed6c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da6deb3b-f92b-414d-b7cd-b97b843db195");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Comments");

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
    }
}
