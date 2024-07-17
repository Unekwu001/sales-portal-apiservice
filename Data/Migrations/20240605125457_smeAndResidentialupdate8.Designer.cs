﻿// <auto-generated />
using System;
using Data.ipNXContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(IpNxDbContext))]
    [Migration("20240605125457_smeAndResidentialupdate8")]
    partial class smeAndResidentialupdate8
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Data.Models.AgentModel.Agent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Agents");
                });

            modelBuilder.Entity("Data.Models.CoverageModels.CoverageLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CoverageName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Lga")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("CoverageLocations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c4b893be-19c3-4e87-857a-d5cc6247e165"),
                            CoverageName = "Parkview Coverage Gap",
                            Latitude = 6.4499999999999993,
                            Lga = "Ikoyi",
                            Longitude = 3.4333330000000002,
                            State = "Lagos"
                        },
                        new
                        {
                            Id = new Guid("6d93a872-af4e-4885-96d9-05ee8a42fa0f"),
                            CoverageName = "victoria garden city",
                            Latitude = 6.464587400000001,
                            Lga = "Ajah",
                            Longitude = 3.5725243999999998,
                            State = "Lagos"
                        },
                        new
                        {
                            Id = new Guid("240ca88e-5dc2-44d9-a127-81f8821089f1"),
                            CoverageName = "Oral Estate",
                            Latitude = 6.4466677000000008,
                            Lga = "Lekki",
                            Longitude = 3.5465458000000001,
                            State = "Lagos"
                        },
                        new
                        {
                            Id = new Guid("3bb5cb95-1d22-4913-b223-c1b1c1e60fa4"),
                            CoverageName = "Northern Foreshore",
                            Latitude = 6.4582094999999997,
                            Lga = "lekki",
                            Longitude = 3.5218280000000002,
                            State = "Lagos"
                        },
                        new
                        {
                            Id = new Guid("026f6a2d-364a-4deb-8ab2-0b48e7cbb8c1"),
                            CoverageName = "Ikota GRA",
                            Latitude = 6.4460404999999996,
                            Lga = "lekki",
                            Longitude = 3.5577774,
                            State = "Lagos"
                        },
                        new
                        {
                            Id = new Guid("036e4557-20bf-4715-98c3-13855f2ad711"),
                            CoverageName = "Lekki Phase 1",
                            Latitude = 6.4478092999999994,
                            Lga = "lekki",
                            Longitude = 3.4723495,
                            State = "Lagos"
                        },
                        new
                        {
                            Id = new Guid("2b339b7a-8db8-4c1a-b1fa-821d2bbfd3fe"),
                            CoverageName = "Ammsco Platinum",
                            Latitude = 8.9982132000000004,
                            Lga = "Galadimawa",
                            Longitude = 7.4244746999999993,
                            State = "Abuja"
                        },
                        new
                        {
                            Id = new Guid("1d8ab9e2-91fc-492a-b80c-400ca0db1bfa"),
                            CoverageName = "Fafu Estate",
                            Latitude = 8.9786175000000004,
                            Lga = "Lokogoma",
                            Longitude = 7.4581913999999996,
                            State = "Abuja"
                        },
                        new
                        {
                            Id = new Guid("ac8b00ee-0fb5-42f7-9381-425c45f6d279"),
                            CoverageName = "Wonderland Estate",
                            Latitude = 9.017571199999999,
                            Lga = "Kukwaba",
                            Longitude = 7.4339159000000006,
                            State = "Abuja"
                        },
                        new
                        {
                            Id = new Guid("f92432b9-bb9e-48a6-a719-0df126aad830"),
                            CoverageName = "Aminas Court",
                            Latitude = 8.9775469999999995,
                            Lga = "Apo-Dutse",
                            Longitude = 7.4790486999999999,
                            State = "Abuja"
                        },
                        new
                        {
                            Id = new Guid("c4291aa4-892c-4347-bc21-17c34e4c1a74"),
                            CoverageName = "Sticks and Stones",
                            Latitude = 8.9775469999999995,
                            Lga = "Apo-Dutse",
                            Longitude = 7.4790486999999999,
                            State = "Abuja"
                        },
                        new
                        {
                            Id = new Guid("21aa6c5d-e3e2-40f3-9b6a-9b55f416dc8d"),
                            CoverageName = "Pleasant Places",
                            Latitude = 8.9775469999999995,
                            Lga = "Apo-Dutse",
                            Longitude = 7.4790486999999999,
                            State = "Abuja"
                        },
                        new
                        {
                            Id = new Guid("b15f3290-4ef3-4c3a-8ae6-cf930f1efee6"),
                            CoverageName = "Standard Estate",
                            Latitude = 8.9737823999999993,
                            Lga = "Galadimawa",
                            Longitude = 7.4202883999999987,
                            State = "Abuja"
                        },
                        new
                        {
                            Id = new Guid("10de5e22-ce58-421d-9867-5f645e935842"),
                            CoverageName = "Trademoore",
                            Latitude = 8.9775469999999995,
                            Lga = "Apo-Dutse",
                            Longitude = 7.4790486999999999,
                            State = "Abuja"
                        },
                        new
                        {
                            Id = new Guid("71a6df47-25f4-4a08-91bb-6d71cfcccf69"),
                            CoverageName = "Jubilation Comfort(Yoruba Estate)",
                            Latitude = 8.9610229999999991,
                            Lga = "Lokogoma",
                            Longitude = 7.4405290000000006,
                            State = "Abuja"
                        });
                });

            modelBuilder.Entity("Data.Models.CoverageModels.GcpGeoCodingApiKey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.ToTable("GcpGeoCodingApiKeys");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c8419e10-0b0a-4885-a5fe-22430fdab645"),
                            Key = "AIzaSyDdN2yR9ooX0Glo7oMHFmBZGVYniVl71Bk"
                        });
                });

            modelBuilder.Entity("Data.Models.CustomerRequestsModels.RequestCallBack", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LastUpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("CallBackRequests");
                });

            modelBuilder.Entity("Data.Models.CustomerRequestsModels.RequestForInstallation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("InstallationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LastUpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("InstallationRequests");
                });

            modelBuilder.Entity("Data.Models.DiscountModel.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LastUpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("numeric");

                    b.Property<Guid>("PlanTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Data.Models.EmailModels.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("SmtpHost")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("SmtpPassword")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("SmtpPort")
                        .HasColumnType("integer");

                    b.Property<string>("SmtpUsername")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("EmailSettings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ca7f1bcf-f42f-4a4d-aab4-362bb811d9e8"),
                            SmtpHost = "smtp-mail.outlook.com",
                            SmtpPassword = "Otusegwa360@",
                            SmtpPort = 587,
                            SmtpUsername = "unekwutheophilus@outlook.com"
                        });
                });

            modelBuilder.Entity("Data.Models.FileUploadModel.FileUpload", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("FileUploads");
                });

            modelBuilder.Entity("Data.Models.OrderModels.ResidentialOrder", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("AlternativePhoneNumber")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("City")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("EstateName")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("FlatNumber")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("GovernmentId")
                        .HasColumnType("text");

                    b.Property<string>("HouseNumber")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("IsBillingAddressSameAsResidentialAddress")
                        .IsRequired()
                        .HasColumnType("character varying(1)");

                    b.Property<bool>("IsFormCompleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("LastUpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nationality")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Occupation")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("PassportPhotograph")
                        .HasColumnType("text");

                    b.Property<string>("PaymentReferenceNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("PlanTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ResidentialBillingDetailsId")
                        .HasColumnType("uuid");

                    b.Property<string>("State")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("StreetName")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("TypeOfBuilding")
                        .HasColumnType("text");

                    b.Property<string>("UtilityBill")
                        .HasColumnType("text");

                    b.Property<string>("networkCoverageAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PlanTypeId");

                    b.HasIndex("ResidentialBillingDetailsId");

                    b.ToTable("ResidentialOrders");
                });

            modelBuilder.Entity("Data.Models.OrderModels.ResidentialOrderBillingDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("LastUpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ResidentialId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("StreetName")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("ResidentialOrderBillingDetails");
                });

            modelBuilder.Entity("Data.Models.OrderModels.SmeOrder", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("CertificateOfIncorporation")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("City")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("CompanyStreetName")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("ContactPersonAlternativePhoneNumber")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("ContactPersonEmail")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ContactPersonFirstName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ContactPersonLastName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ContactPersonPhoneNumber")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GovernmentId")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("IsBillingAddressSameAsResidentialAddress")
                        .IsRequired()
                        .HasColumnType("character varying(1)");

                    b.Property<bool>("IsFormCompleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LastUpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LetterOfIntroduction")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("PassportPhotograph")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("PaymentReferenceNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer");

                    b.Property<Guid>("PlanTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SmeBillingDetailsId")
                        .HasColumnType("uuid");

                    b.Property<string>("State")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("TypeOfBuilding")
                        .HasColumnType("text");

                    b.Property<string>("TypeOfBusiness")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("UtilityBill")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("networkCoverageAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PlanTypeId");

                    b.HasIndex("SmeBillingDetailsId");

                    b.ToTable("SmeOrders");
                });

            modelBuilder.Entity("Data.Models.OrderModels.SmeOrderBillingDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("CompanyStreetName")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("ContactPersonEmail")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ContactPersonFirstName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ContactPersonLastName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ContactPersonPhoneNumber")
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LastUpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SmeId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("SmeOrderBillingDetails");
                });

            modelBuilder.Entity("Data.Models.OtpModel.UserOtp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LastUpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Otp")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("UserOtps");
                });

            modelBuilder.Entity("Data.Models.PlanModels.Plan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LastUpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PlanName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("Data.Models.PlanModels.PlanType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BandSpeedUnit")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("BandSpeedValue")
                        .HasColumnType("integer");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("KeyFeature1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("KeyFeature2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("KeyFeature3")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("LastUpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PaymentCycle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PlanId")
                        .HasColumnType("uuid");

                    b.Property<string>("PlanTypeName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<decimal>("SetUpCharge")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("PlanTypes");
                });

            modelBuilder.Entity("Data.Models.UserModels.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyName")
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("LastUpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.Models.WebsiteVisitModel.WebsiteVisit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("VisitDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("WebsiteVisits");
                });

            modelBuilder.Entity("Data.Models.OrderModels.ResidentialOrder", b =>
                {
                    b.HasOne("Data.Models.PlanModels.PlanType", "PlanType")
                        .WithMany()
                        .HasForeignKey("PlanTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.OrderModels.ResidentialOrderBillingDetail", "ResidentialBillingDetails")
                        .WithMany()
                        .HasForeignKey("ResidentialBillingDetailsId");

                    b.Navigation("PlanType");

                    b.Navigation("ResidentialBillingDetails");
                });

            modelBuilder.Entity("Data.Models.OrderModels.SmeOrder", b =>
                {
                    b.HasOne("Data.Models.PlanModels.PlanType", "PlanType")
                        .WithMany()
                        .HasForeignKey("PlanTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.OrderModels.SmeOrderBillingDetail", "SmeBillingDetails")
                        .WithMany()
                        .HasForeignKey("SmeBillingDetailsId");

                    b.Navigation("PlanType");

                    b.Navigation("SmeBillingDetails");
                });

            modelBuilder.Entity("Data.Models.PlanModels.PlanType", b =>
                {
                    b.HasOne("Data.Models.PlanModels.Plan", "Plan")
                        .WithMany("PlanTypes")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("Data.Models.PlanModels.Plan", b =>
                {
                    b.Navigation("PlanTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
