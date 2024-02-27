﻿// <auto-generated />
using System;
using InternetBanking.API.Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InternetBanking.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InternetBanking.API.Entidades.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<int>("Limite")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Saldo")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Clientes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyToken = 0u,
                            Limite = 100000,
                            Nome = "Fulano 1",
                            Saldo = 0
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyToken = 0u,
                            Limite = 80000,
                            Nome = "Fulano 2",
                            Saldo = 0
                        },
                        new
                        {
                            Id = 3,
                            ConcurrencyToken = 0u,
                            Limite = 1000000,
                            Nome = "Fulano 3",
                            Saldo = 0
                        },
                        new
                        {
                            Id = 4,
                            ConcurrencyToken = 0u,
                            Limite = 10000000,
                            Nome = "Fulano 4",
                            Saldo = 0
                        },
                        new
                        {
                            Id = 5,
                            ConcurrencyToken = 0u,
                            Limite = 500000,
                            Nome = "Fulano 5",
                            Saldo = 0
                        });
                });

            modelBuilder.Entity("InternetBanking.API.Entidades.Transacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RealizadaEm")
                        .HasColumnType("Timestamp");

                    b.Property<char>("Tipo")
                        .HasColumnType("character(1)");

                    b.Property<int>("Valor")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Transacoes");
                });

            modelBuilder.Entity("InternetBanking.API.Entidades.Transacao", b =>
                {
                    b.HasOne("InternetBanking.API.Entidades.Cliente", "Cliente")
                        .WithMany("Transacoes")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("InternetBanking.API.Entidades.Cliente", b =>
                {
                    b.Navigation("Transacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
