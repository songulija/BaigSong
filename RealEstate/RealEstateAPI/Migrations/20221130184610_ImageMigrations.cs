using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAPI.Migrations
{
    public partial class ImageMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "69924c0f-5d0d-46ae-870d-e1758eb49012");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "711f9640-6593-4217-8759-1e7f9f18cc7f");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Properties");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: true),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "da7343bf-98ca-474d-b1aa-8d963f8fa712", "906ec2b5-3904-44db-b675-fdc6ab4f81e8", "User", "USER" },
                    { "51c46ace-4659-4720-a651-1c97be568529", "067e437c-e7de-4916-8d83-2ab5fcad9737", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 11, 30, 20, 46, 9, 739, DateTimeKind.Local).AddTicks(5172));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 11, 30, 20, 46, 9, 744, DateTimeKind.Local).AddTicks(352));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2022, 11, 30, 20, 46, 9, 744, DateTimeKind.Local).AddTicks(385));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$GdFpHlCacmk1agBuSJLEWOLh3HkhPf.dHlsI5yRG/esMABCeAlnKu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$T5e/llVSJEZdydLHricBW.GNZSRZDZ.VlFGsxG0IedXUStbKx9kem");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PropertyId",
                table: "Images",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "51c46ace-4659-4720-a651-1c97be568529");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "da7343bf-98ca-474d-b1aa-8d963f8fa712");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "69924c0f-5d0d-46ae-870d-e1758eb49012", "89ab7daa-69c0-469d-8b3d-44f56f83e967", "User", "USER" },
                    { "711f9640-6593-4217-8759-1e7f9f18cc7f", "6459696a-135a-49dd-b793-fd3b1f31ef46", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Photo" },
                values: new object[] { new DateTime(2022, 11, 29, 13, 59, 40, 541, DateTimeKind.Local).AddTicks(5481), "https://cf.bstatic.com/xdata/images/hotel/270x200/344742578.jpg?k=fd7593cb20d8fc876b5e4525e2338486872957c1d77d97df1f9f846de8ffc171&o=" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "Photo" },
                values: new object[] { new DateTime(2022, 11, 29, 13, 59, 40, 544, DateTimeKind.Local).AddTicks(125), "https://cf.bstatic.com/xdata/images/hotel/270x200/402244075.jpg?k=6d4bdee1710675d96af2fbeeedab63fbde64b897a90eb277aee25f55d546ea15&o=" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "Photo" },
                values: new object[] { new DateTime(2022, 11, 29, 13, 59, 40, 544, DateTimeKind.Local).AddTicks(161), "https://cf.bstatic.com/xdata/images/hotel/270x200/183672191.jpg?k=1bc046ed0234d7cc23c30b61d3ff2f2ae1cc88f178fafcf8dd0895c4c88514f8&o=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$mH.ZZT7OPZo5b4goiMLjPeqfoDT6eeUSkJojNFyPMYDxqdtuPVySG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$B/aNsuBWL0eFmgPMEG9H5Oh14GDuLfYuW2y0rG2VwrizWjxytuQvq");
        }
    }
}
