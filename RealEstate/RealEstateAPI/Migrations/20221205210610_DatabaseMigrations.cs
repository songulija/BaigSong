using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAPI.Migrations
{
    public partial class DatabaseMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PropertyTypeId = table.Column<int>(type: "int", nullable: true),
                    RentTypeId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomNumber = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_RentTypes_RentTypeId",
                        column: x => x.RentTypeId,
                        principalTable: "RentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PropertyId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PropertyId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouriteProperties_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavouriteProperties_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "Journals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PropertyId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journals_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Journals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PropertyId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAdress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Date", "Title" },
                values: new object[] { 1, new DateTime(2022, 12, 5, 23, 6, 9, 678, DateTimeKind.Local).AddTicks(9423), "Lithuania" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12133fdb-1529-4886-98da-301fd3571a65", "6aa443cb-5b58-42ba-b3cf-a19c3aeafc23", "User", "USER" },
                    { "65390373-7cc0-455a-83eb-0926b8896272", "be3c204f-b0b6-48b0-a76c-297806d0fe6b", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "Date", "Photo", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 5, 23, 6, 9, 675, DateTimeKind.Local).AddTicks(482), "https://cf.bstatic.com/xdata/images/xphoto/square300/57584488.webp?k=bf724e4e9b9b75480bbe7fc675460a089ba6414fe4693b83ea3fdd8e938832a6&o=", "Hotels" },
                    { 2, new DateTime(2022, 12, 5, 23, 6, 9, 678, DateTimeKind.Local).AddTicks(2493), "https://cf.bstatic.com/static/img/theme-index/carousel_320x240/card-image-apartments_300/9f60235dc09a3ac3f0a93adbc901c61ecd1ce72e.jpg", "Apartments" },
                    { 3, new DateTime(2022, 12, 5, 23, 6, 9, 678, DateTimeKind.Local).AddTicks(2535), "https://cf.bstatic.com/static/img/theme-index/carousel_320x240/bg_resorts/6f87c6143fbd51a0bb5d15ca3b9cf84211ab0884.jpg", "Resorts" },
                    { 4, new DateTime(2022, 12, 5, 23, 6, 9, 678, DateTimeKind.Local).AddTicks(2540), "https://cf.bstatic.com/static/img/theme-index/carousel_320x240/card-image-villas_300/dd0d7f8202676306a661aa4f0cf1ffab31286211.jpg", "Houses" },
                    { 5, new DateTime(2022, 12, 5, 23, 6, 9, 678, DateTimeKind.Local).AddTicks(2543), "https://cf.bstatic.com/xdata/images/city/square250/777085.webp?k=b95bc65ec83682e7aafc89112ff398b1081be9696ef92556ffd4fb9648a6b807&o=", "Lands" }
                });

            migrationBuilder.InsertData(
                table: "RentTypes",
                columns: new[] { "Id", "Date", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 5, 23, 6, 9, 678, DateTimeKind.Local).AddTicks(6297), "Long Term" },
                    { 2, new DateTime(2022, 12, 5, 23, 6, 9, 678, DateTimeKind.Local).AddTicks(6630), "Short Term" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "ADMINISTRATOR" },
                    { 2, "USER" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CountryId", "Date", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 12, 5, 23, 6, 9, 679, DateTimeKind.Local).AddTicks(5290), "Vilnius" },
                    { 2, 1, new DateTime(2022, 12, 5, 23, 6, 9, 679, DateTimeKind.Local).AddTicks(5928), "Kaunas" },
                    { 3, 1, new DateTime(2022, 12, 5, 23, 6, 9, 679, DateTimeKind.Local).AddTicks(5941), "Klaipėda" },
                    { 4, 1, new DateTime(2022, 12, 5, 23, 6, 9, 679, DateTimeKind.Local).AddTicks(5944), "Palanga" },
                    { 5, 1, new DateTime(2022, 12, 5, 23, 6, 9, 679, DateTimeKind.Local).AddTicks(5947), "Šiauliai" },
                    { 6, 1, new DateTime(2022, 12, 5, 23, 6, 9, 679, DateTimeKind.Local).AddTicks(5950), "Druskininkai" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "TypeId" },
                values: new object[,]
                {
                    { 1, "lsongulija@gmail.com", "Lukas", "Songulija", "$2a$11$ITWoCkAI3lE2tERFI.O9U.Caje5UVJV99O3x2IH/SP86EbSsxYvjm", "+37061115217", 1 },
                    { 2, "kpigaga@gmail.com", "Karolis", "Pigaga", "$2a$11$.lCdZlsc3zVtGj/W1fbN6.sXmBaGsHhnA/h.xerRaEAXGUED1xVhi", "+37061115982", 1 }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Address", "CityId", "Date", "Description", "Price", "PropertyTypeId", "RentTypeId", "RoomNumber", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Gedimino g. 71", 1, new DateTime(2022, 12, 5, 23, 6, 9, 681, DateTimeKind.Local).AddTicks(1579), "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.", 350f, 2, 1, 2, "Vilnius G71", 1 },
                    { 2, "Gedimino g. 72", 1, new DateTime(2022, 12, 5, 23, 6, 9, 681, DateTimeKind.Local).AddTicks(2747), "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.", 369f, 2, 1, 2, "Vilnius G72", 1 },
                    { 3, "Gedimino g. 73", 1, new DateTime(2022, 12, 5, 23, 6, 9, 681, DateTimeKind.Local).AddTicks(2792), "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.", 400f, 2, 1, 2, "Vilnius K73", 1 },
                    { 4, "Gedimino g. 74", 1, new DateTime(2022, 12, 5, 23, 6, 9, 681, DateTimeKind.Local).AddTicks(2815), "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.", 350f, 2, 1, 2, "Vilnius K74", 1 },
                    { 5, "Gedimino g. 75", 1, new DateTime(2022, 12, 5, 23, 6, 9, 681, DateTimeKind.Local).AddTicks(2829), "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.", 500f, 2, 1, 2, "Vilnius K75", 1 },
                    { 6, "Rygos g. 10", 1, new DateTime(2022, 12, 5, 23, 6, 9, 681, DateTimeKind.Local).AddTicks(2839), "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.", 600f, 4, 1, 2, "Vilnius R10", 1 },
                    { 7, "Rygos g. 11", 1, new DateTime(2022, 12, 5, 23, 6, 9, 681, DateTimeKind.Local).AddTicks(2843), "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.", 550f, 4, 1, 2, "Vilnius R11", 1 },
                    { 8, "Rygos g. 12", 1, new DateTime(2022, 12, 5, 23, 6, 9, 681, DateTimeKind.Local).AddTicks(2848), "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.", 589f, 4, 1, 2, "Vilnius R12", 1 },
                    { 9, "Rygos g. 13", 1, new DateTime(2022, 12, 5, 23, 6, 9, 681, DateTimeKind.Local).AddTicks(2853), "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.", 600f, 4, 1, 2, "Vilnius R13", 1 },
                    { 10, "Rygos g. 14", 1, new DateTime(2022, 12, 5, 23, 6, 9, 681, DateTimeKind.Local).AddTicks(2857), "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.", 650f, 4, 1, 2, "Vilnius R14", 1 },
                    { 11, "Rygos g. 15", 1, new DateTime(2022, 12, 5, 23, 6, 9, 681, DateTimeKind.Local).AddTicks(2861), "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.", 550f, 4, 1, 2, "Vilnius R15", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PropertyId",
                table: "Comments",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteProperties_PropertyId",
                table: "FavouriteProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteProperties_UserId",
                table: "FavouriteProperties",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PropertyId",
                table: "Images",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_PropertyId",
                table: "Journals",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_UserId",
                table: "Journals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PropertyId",
                table: "Payments",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CityId",
                table: "Properties",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_RentTypeId",
                table: "Properties",
                column: "RentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserId",
                table: "Properties",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TypeId",
                table: "Users",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FavouriteProperties");

            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Journals");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropTable(
                name: "RentTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}
