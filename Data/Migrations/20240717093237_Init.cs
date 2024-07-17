using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Region = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Type = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CallBackRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallBackRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoverageLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CoverageName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Lga = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverageLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Street = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Percentage = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SmtpHost = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SmtpPort = table.Column<int>(type: "integer", nullable: false),
                    SmtpUsername = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SmtpPassword = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileUploads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUploads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GcpGeoCodingApiKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GcpGeoCodingApiKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstallationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstallationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallationRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    SetUpCharge = table.Column<decimal>(type: "numeric", nullable: true),
                    PhoneLine = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResidentialOrderBillingDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    LastName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    StreetName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    State = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ResidentialId = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialOrderBillingDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmeOrderBillingDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactPersonFirstName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ContactPersonLastName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ContactPersonEmail = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ContactPersonPhoneNumber = table.Column<string>(type: "text", nullable: true),
                    CompanyStreetName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    State = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SmeId = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmeOrderBillingDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOtps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Otp = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOtps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    HashedPassword = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteVisits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanTypeName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentCycle = table.Column<string>(type: "text", nullable: false),
                    BandSpeedValue = table.Column<int>(type: "integer", nullable: false),
                    BandSpeedUnit = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SetUpCharge = table.Column<decimal>(type: "numeric", nullable: false),
                    KeyFeature1 = table.Column<string>(type: "text", nullable: false),
                    KeyFeature2 = table.Column<string>(type: "text", nullable: false),
                    KeyFeature3 = table.Column<string>(type: "text", nullable: false),
                    DataAllowance = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DiscountId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanTypes_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanTypes_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResidentialOrders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    LastName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    networkCoverageAddress = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    AlternativePhoneNumber = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Occupation = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    Nationality = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    FlatNumber = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    HouseNumber = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    StreetName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    EstateName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    State = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    TypeOfBuilding = table.Column<string>(type: "text", nullable: true),
                    IsBillingAddressSameAsResidentialAddress = table.Column<string>(type: "character varying(1)", nullable: false),
                    ResidentialBillingDetailsId = table.Column<Guid>(type: "uuid", nullable: true),
                    PassportPhotograph = table.Column<string>(type: "text", nullable: true),
                    GovernmentId = table.Column<string>(type: "text", nullable: true),
                    UtilityBill = table.Column<string>(type: "text", nullable: true),
                    PlanTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsFormCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    PaymentReferenceNumber = table.Column<string>(type: "text", nullable: true),
                    HasRequestedInstallation = table.Column<bool>(type: "boolean", nullable: false),
                    HasRequestedToAddWifi = table.Column<bool>(type: "boolean", nullable: false),
                    AgentId = table.Column<string>(type: "text", nullable: true),
                    Discount = table.Column<decimal>(type: "numeric", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidentialOrders_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResidentialOrders_PlanTypes_PlanTypeId",
                        column: x => x.PlanTypeId,
                        principalTable: "PlanTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResidentialOrders_ResidentialOrderBillingDetails_Residentia~",
                        column: x => x.ResidentialBillingDetailsId,
                        principalTable: "ResidentialOrderBillingDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SmeOrders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ContactPersonFirstName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ContactPersonLastName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ContactPersonEmail = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    networkCoverageAddress = table.Column<string>(type: "text", nullable: false),
                    TypeOfBusiness = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ContactPersonPhoneNumber = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ContactPersonAlternativePhoneNumber = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CompanyStreetName = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    City = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    State = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    TypeOfBuilding = table.Column<string>(type: "text", nullable: true),
                    IsBillingAddressSameAsResidentialAddress = table.Column<string>(type: "character varying(1)", nullable: false),
                    SmeBillingDetailsId = table.Column<Guid>(type: "uuid", nullable: true),
                    PassportPhotograph = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LetterOfIntroduction = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    GovernmentId = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    UtilityBill = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CertificateOfIncorporation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PlanTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsFormCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    PaymentReferenceNumber = table.Column<string>(type: "text", nullable: true),
                    HasRequestedInstallation = table.Column<bool>(type: "boolean", nullable: false),
                    HasRequestedToAddWifi = table.Column<bool>(type: "boolean", nullable: false),
                    AgentId = table.Column<string>(type: "text", nullable: true),
                    Discount = table.Column<decimal>(type: "numeric", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmeOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmeOrders_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SmeOrders_PlanTypes_PlanTypeId",
                        column: x => x.PlanTypeId,
                        principalTable: "PlanTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmeOrders_SmeOrderBillingDetails_SmeBillingDetailsId",
                        column: x => x.SmeBillingDetailsId,
                        principalTable: "SmeOrderBillingDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "CoverageLocations",
                columns: new[] { "Id", "CoverageName", "Latitude", "Lga", "Longitude", "State" },
                values: new object[,]
                {
                    { new Guid("1d4373d4-0af9-4a17-b83b-0d7a904d714a"), "Northern Foreshore", 6.4582094999999997, "lekki", 3.5218280000000002, "Lagos" },
                    { new Guid("2e9fb6f7-4242-48be-96e6-ae7088aadf3b"), "victoria garden city", 6.464587400000001, "Ajah", 3.5725243999999998, "Lagos" },
                    { new Guid("42598047-6b14-4609-a525-c59821919703"), "Fafu Estate", 8.9786175000000004, "Lokogoma", 7.4581913999999996, "Abuja" },
                    { new Guid("47ad7c92-5a29-488f-a489-9fc33ee5e1e8"), "Standard Estate", 8.9737823999999993, "Galadimawa", 7.4202883999999987, "Abuja" },
                    { new Guid("4ffed639-8fa5-4549-8c46-19e8cb5e16ea"), "Sticks and Stones", 8.9775469999999995, "Apo-Dutse", 7.4790486999999999, "Abuja" },
                    { new Guid("5063a3d8-d98a-4f5d-9e32-0fdf8db5e5ee"), "Ikota GRA", 6.4460404999999996, "lekki", 3.5577774, "Lagos" },
                    { new Guid("669e01b9-0809-4642-bb6f-b95d6d4debd8"), "Parkview Coverage Gap", 6.4499999999999993, "Ikoyi", 3.4333330000000002, "Lagos" },
                    { new Guid("74c635d6-70df-45b7-8c37-4a026a739013"), "Lekki Phase 1", 6.4478092999999994, "lekki", 3.4723495, "Lagos" },
                    { new Guid("75b079cb-b4aa-4c50-b1ca-642190bde2b8"), "Trademoore", 8.9775469999999995, "Apo-Dutse", 7.4790486999999999, "Abuja" },
                    { new Guid("839e7099-fb32-4bb2-8197-7d1aff3fa4a3"), "Wonderland Estate", 9.017571199999999, "Kukwaba", 7.4339159000000006, "Abuja" },
                    { new Guid("92ce1467-643a-48ca-8fad-954fa7cadd4d"), "Aminas Court", 8.9775469999999995, "Apo-Dutse", 7.4790486999999999, "Abuja" },
                    { new Guid("955b3226-831a-4bc4-a26a-ef3a6505cbba"), "Oral Estate", 6.4466677000000008, "Lekki", 3.5465458000000001, "Lagos" },
                    { new Guid("aac866fa-72c7-4664-80b7-4c17720fb670"), "Ammsco Platinum", 8.9982132000000004, "Galadimawa", 7.4244746999999993, "Abuja" },
                    { new Guid("acfedd2d-c824-407d-ae86-737e6060f14c"), "Jubilation Comfort(Yoruba Estate)", 8.9610229999999991, "Lokogoma", 7.4405290000000006, "Abuja" },
                    { new Guid("f4749b08-dfd1-49b1-9758-f80734eb9470"), "Pleasant Places", 8.9775469999999995, "Apo-Dutse", 7.4790486999999999, "Abuja" }
                });

            migrationBuilder.InsertData(
                table: "EmailSettings",
                columns: new[] { "Id", "SmtpHost", "SmtpPassword", "SmtpPort", "SmtpUsername" },
                values: new object[] { new Guid("20b1f34c-cd48-47ee-a3b9-9a31a0175c5c"), "smtp-mail.outlook.com", "Otusegwa360@", 587, "unekwutheophilus@outlook.com" });

            migrationBuilder.InsertData(
                table: "GcpGeoCodingApiKeys",
                columns: new[] { "Id", "Key" },
                values: new object[] { new Guid("0abc3c9d-2315-4b50-857b-64465b47ba1f"), "AIzaSyDdN2yR9ooX0Glo7oMHFmBZGVYniVl71Bk" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanTypes_DiscountId",
                table: "PlanTypes",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanTypes_PlanId",
                table: "PlanTypes",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialOrders_AgentId",
                table: "ResidentialOrders",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialOrders_PlanTypeId",
                table: "ResidentialOrders",
                column: "PlanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialOrders_ResidentialBillingDetailsId",
                table: "ResidentialOrders",
                column: "ResidentialBillingDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_SmeOrders_AgentId",
                table: "SmeOrders",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_SmeOrders_PlanTypeId",
                table: "SmeOrders",
                column: "PlanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SmeOrders_SmeBillingDetailsId",
                table: "SmeOrders",
                column: "SmeBillingDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallBackRequests");

            migrationBuilder.DropTable(
                name: "CoverageLocations");

            migrationBuilder.DropTable(
                name: "EmailSettings");

            migrationBuilder.DropTable(
                name: "FileUploads");

            migrationBuilder.DropTable(
                name: "GcpGeoCodingApiKeys");

            migrationBuilder.DropTable(
                name: "InstallationRequests");

            migrationBuilder.DropTable(
                name: "ResidentialOrders");

            migrationBuilder.DropTable(
                name: "SmeOrders");

            migrationBuilder.DropTable(
                name: "UserOtps");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WebsiteVisits");

            migrationBuilder.DropTable(
                name: "ResidentialOrderBillingDetails");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "PlanTypes");

            migrationBuilder.DropTable(
                name: "SmeOrderBillingDetails");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
