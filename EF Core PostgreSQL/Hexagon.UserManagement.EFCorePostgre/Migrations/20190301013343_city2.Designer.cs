﻿// <auto-generated />
using System;
using Hexagon.UserManagement.EFCorePostgre;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hexagon.UserManagement.EFCorePostgre.Migrations
{
    [DbContext(typeof(UserManagementDbContext))]
    [Migration("20190301013343_city2")]
    partial class city2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Hexagon.UserManagement.EFCorePostgre.Entity.AdminUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("Age")
                        .HasColumnName("age");

                    b.Property<string>("Gender")
                        .HasColumnName("gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(255);

                    b.Property<int?>("RealmId")
                        .HasColumnName("realmid");

                    b.Property<DateTime?>("TokenExpireTime")
                        .HasColumnName("tokenexpiretime");

                    b.HasKey("Id")
                        .HasName("pk_adminuser");

                    b.HasIndex("RealmId")
                        .HasName("ix_adminuser_realmid");

                    b.ToTable("adminuser");
                });

            modelBuilder.Entity("Hexagon.UserManagement.EFCorePostgre.Entity.Capital", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("other")
                        .HasColumnName("other");

                    b.HasKey("Id")
                        .HasName("pk_capital");

                    b.ToTable("capital");
                });

            modelBuilder.Entity("Hexagon.UserManagement.EFCorePostgre.Entity.City", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("CityName")
                        .HasColumnName("cityname");

                    b.HasKey("Id")
                        .HasName("pk_city");

                    b.ToTable("city");
                });

            modelBuilder.Entity("Hexagon.UserManagement.EFCorePostgre.Entity.Realm", b =>
                {
                    b.Property<int>("RlmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("rlmid");

                    b.Property<string>("RealmName")
                        .IsRequired()
                        .HasColumnName("realmname")
                        .HasMaxLength(50);

                    b.HasKey("RlmId")
                        .HasName("pk_realm");

                    b.ToTable("realm");
                });

            modelBuilder.Entity("Hexagon.UserManagement.EFCorePostgre.Entity.RealmSolution", b =>
                {
                    b.Property<int>("RlmId")
                        .HasColumnName("rlmid");

                    b.Property<int>("SlnId")
                        .HasColumnName("slnid");

                    b.HasKey("RlmId", "SlnId")
                        .HasName("pk_realmsolution");

                    b.HasIndex("SlnId")
                        .HasName("ix_realmsolution_slnid");

                    b.ToTable("realmsolution");
                });

            modelBuilder.Entity("Hexagon.UserManagement.EFCorePostgre.Entity.Solution", b =>
                {
                    b.Property<int>("SlnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("slnid");

                    b.Property<string>("SolutionName")
                        .IsRequired()
                        .HasColumnName("solutionname")
                        .HasMaxLength(200);

                    b.HasKey("SlnId")
                        .HasName("pk_solution");

                    b.ToTable("solution");
                });

            modelBuilder.Entity("Hexagon.UserManagement.EFCorePostgre.Entity.AdminUser", b =>
                {
                    b.HasOne("Hexagon.UserManagement.EFCorePostgre.Entity.Realm", "Realm")
                        .WithMany("Users")
                        .HasForeignKey("RealmId")
                        .HasConstraintName("fk_adminuser_realms_realmid");
                });

            modelBuilder.Entity("Hexagon.UserManagement.EFCorePostgre.Entity.RealmSolution", b =>
                {
                    b.HasOne("Hexagon.UserManagement.EFCorePostgre.Entity.Realm", "Realm")
                        .WithMany("RealmSolutions")
                        .HasForeignKey("RlmId")
                        .HasConstraintName("fk_realmsolution_realm_rlmid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hexagon.UserManagement.EFCorePostgre.Entity.Solution", "Solution")
                        .WithMany("RealmSolutions")
                        .HasForeignKey("SlnId")
                        .HasConstraintName("fk_realmsolution_solutions_slnid")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
