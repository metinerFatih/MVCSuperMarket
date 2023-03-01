﻿// <auto-generated />
using System;
using MVCSuperMarket.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVCSuperMarket.Migrations
{
    [DbContext(typeof(SuperMarketDbContext))]
    partial class SuperMarketDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MVCSuperMarket.Classes.Urun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EklenmeTarihi")
                        .HasColumnType("datetime2");

                    b.Property<double>("Fiyat")
                        .HasColumnType("float");

                    b.Property<DateTime?>("GuncellenmeTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Resim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("StoktaVarMi")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Urunler");
                });
#pragma warning restore 612, 618
        }
    }
}
