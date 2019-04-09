﻿// <auto-generated />
using System;
using DebtProjectMVC.DebtDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DebtProjectMVC.Migrations
{
    [DbContext(typeof(DebtDatabaseContext))]
    [Migration("20190321163213_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DebtProjectMVC.Entities.Borrower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("PESEL");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.HasIndex("PESEL")
                        .IsUnique()
                        .HasFilter("[PESEL] IS NOT NULL");

                    b.ToTable("Borrower");
                });

            modelBuilder.Entity("DebtProjectMVC.Entities.Creditor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Creditor");
                });

            modelBuilder.Entity("DebtProjectMVC.Entities.DebtEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BorrowerId");

                    b.Property<DateTime>("Created");

                    b.Property<int>("CreditorId");

                    b.Property<decimal?>("ReturnedValue");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<DateTime?>("Updated");

                    b.Property<decimal>("Value");

                    b.HasKey("Id");

                    b.HasIndex("BorrowerId");

                    b.HasIndex("CreditorId");

                    b.ToTable("Debt");
                });

            modelBuilder.Entity("DebtProjectMVC.Entities.DebtEntry", b =>
                {
                    b.HasOne("DebtProjectMVC.Entities.Borrower", "Borrower")
                        .WithMany()
                        .HasForeignKey("BorrowerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DebtProjectMVC.Entities.Creditor", "Creditor")
                        .WithMany()
                        .HasForeignKey("CreditorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
