// <auto-generated />
using System;
using DbConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbConnection.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220528154405_Carts-Table")]
    partial class CartsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DbConnection.Models.Accounts", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountDisplayName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts", "dbo");
                });

            modelBuilder.Entity("DbConnection.Models.Carts", b =>
                {
                    b.Property<Guid>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("FinalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CartId");

                    b.HasIndex("AccountId");

                    b.ToTable("Carts", "dbo");
                });

            modelBuilder.Entity("DbConnection.Models.Products", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.ToTable("Products", "dbo");
                });

            modelBuilder.Entity("DbConnection.Models.Carts", b =>
                {
                    b.HasOne("DbConnection.Models.Accounts", "Account")
                        .WithMany("Carts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("DbConnection.Models.Accounts", b =>
                {
                    b.Navigation("Carts");
                });
#pragma warning restore 612, 618
        }
    }
}
