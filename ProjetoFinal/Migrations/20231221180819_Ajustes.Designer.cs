﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoFinal.Data;

#nullable disable

namespace ProjetoFinal.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20231221180819_Ajustes")]
    partial class Ajustes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProjetoFinal.Models.Computador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NumPatrimonio")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("SecaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SecaoId");

                    b.ToTable("Computadores");
                });

            modelBuilder.Entity("ProjetoFinal.Models.Procedimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Procedimentos");
                });

            modelBuilder.Entity("ProjetoFinal.Models.RegistroManutencao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ComputadorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataManutencao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataPrevisao")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProcedimentoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComputadorId");

                    b.HasIndex("ProcedimentoId");

                    b.ToTable("RegistroManutencoes");
                });

            modelBuilder.Entity("ProjetoFinal.Models.Secao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Secoes");
                });

            modelBuilder.Entity("ProjetoFinal.Models.Computador", b =>
                {
                    b.HasOne("ProjetoFinal.Models.Secao", "Secao")
                        .WithMany("Computadores")
                        .HasForeignKey("SecaoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Secao");
                });

            modelBuilder.Entity("ProjetoFinal.Models.RegistroManutencao", b =>
                {
                    b.HasOne("ProjetoFinal.Models.Computador", "Computador")
                        .WithMany("Manutencoes")
                        .HasForeignKey("ComputadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjetoFinal.Models.Procedimento", "Procedimento")
                        .WithMany("registroManutencoes")
                        .HasForeignKey("ProcedimentoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Computador");

                    b.Navigation("Procedimento");
                });

            modelBuilder.Entity("ProjetoFinal.Models.Computador", b =>
                {
                    b.Navigation("Manutencoes");
                });

            modelBuilder.Entity("ProjetoFinal.Models.Procedimento", b =>
                {
                    b.Navigation("registroManutencoes");
                });

            modelBuilder.Entity("ProjetoFinal.Models.Secao", b =>
                {
                    b.Navigation("Computadores");
                });
#pragma warning restore 612, 618
        }
    }
}
