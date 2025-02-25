﻿// <auto-generated />
using HandBook.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Book.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241127100504_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("HandBook.Classes.handBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("HandBooks");
                });

            modelBuilder.Entity("HandBook.Classes.handBook", b =>
                {
                    b.OwnsOne("HandBook.Classes.Car", "car", b1 =>
                        {
                            b1.Property<int>("handBookId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Brand")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Color")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("DateOfManufacture")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("DateofLastTechnicalInspection")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("FeaturesOfDesignAndColoring")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("SerialNumber")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("SideNumber")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("handBookId");

                            b1.ToTable("HandBooks");

                            b1.WithOwner()
                                .HasForeignKey("handBookId");
                        });

                    b.OwnsOne("HandBook.Classes.PasportsData", "data", b1 =>
                        {
                            b1.Property<int>("handBookId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("BirthDate")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("FIO")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("IssueDate")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("IssuedBy")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Series")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("handBookId");

                            b1.ToTable("HandBooks");

                            b1.WithOwner()
                                .HasForeignKey("handBookId");
                        });

                    b.Navigation("car")
                        .IsRequired();

                    b.Navigation("data")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
