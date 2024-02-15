﻿// <auto-generated />
using System;
using Clinica_medicala.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Clinica_medicala.Migrations
{
    [DbContext(typeof(ClinicaContext))]
    partial class ClinicaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Clinica_medicala.Models.Medic", b =>
                {
                    b.Property<int>("MedicID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicID"));

                    b.Property<string>("Nume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicID");

                    b.ToTable("Medici", (string)null);
                });

            modelBuilder.Entity("Clinica_medicala.Models.Pacient", b =>
                {
                    b.Property<int>("PacientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PacientID"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNasterii")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PacientID");

                    b.ToTable("Pacienti", (string)null);
                });

            modelBuilder.Entity("Clinica_medicala.Models.Programare", b =>
                {
                    b.Property<int>("ProgramareID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProgramareID"));

                    b.Property<DateTime>("DataProgramare")
                        .HasColumnType("datetime2");

                    b.Property<int>("PacientID")
                        .HasColumnType("int");

                    b.Property<int>("ServiciuID")
                        .HasColumnType("int");

                    b.HasKey("ProgramareID");

                    b.HasIndex("PacientID");

                    b.HasIndex("ServiciuID");

                    b.ToTable("Programari", (string)null);
                });

            modelBuilder.Entity("Clinica_medicala.Models.Serviciu", b =>
                {
                    b.Property<int>("ServiciuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiciuID"));

                    b.Property<decimal>("Pret")
                        .HasColumnType("decimal(6, 2)");

                    b.Property<int?>("SpecializareID")
                        .HasColumnType("int");

                    b.Property<string>("Titlu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiciuID");

                    b.HasIndex("SpecializareID");

                    b.ToTable("Servicii", (string)null);
                });

            modelBuilder.Entity("Clinica_medicala.Models.ServiciuPrestat", b =>
                {
                    b.Property<int>("ServiciuID")
                        .HasColumnType("int");

                    b.Property<int>("MedicID")
                        .HasColumnType("int");

                    b.HasKey("ServiciuID", "MedicID");

                    b.HasIndex("MedicID");

                    b.ToTable("ServiciiPrestate", (string)null);
                });

            modelBuilder.Entity("Clinica_medicala.Models.Specializare", b =>
                {
                    b.Property<int>("SpecializareID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpecializareID"));

                    b.Property<string>("Nume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpecializareID");

                    b.ToTable("Specializari", (string)null);
                });

            modelBuilder.Entity("Clinica_medicala.Models.Programare", b =>
                {
                    b.HasOne("Clinica_medicala.Models.Pacient", "Pacient")
                        .WithMany("Programari")
                        .HasForeignKey("PacientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clinica_medicala.Models.Serviciu", "Serviciu")
                        .WithMany("Programari")
                        .HasForeignKey("ServiciuID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pacient");

                    b.Navigation("Serviciu");
                });

            modelBuilder.Entity("Clinica_medicala.Models.Serviciu", b =>
                {
                    b.HasOne("Clinica_medicala.Models.Specializare", "Specializare")
                        .WithMany("Servicii")
                        .HasForeignKey("SpecializareID");

                    b.Navigation("Specializare");
                });

            modelBuilder.Entity("Clinica_medicala.Models.ServiciuPrestat", b =>
                {
                    b.HasOne("Clinica_medicala.Models.Medic", "Medic")
                        .WithMany("ServiciiPrestate")
                        .HasForeignKey("MedicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clinica_medicala.Models.Serviciu", "Serviciu")
                        .WithMany("ServiciiPrestate")
                        .HasForeignKey("ServiciuID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medic");

                    b.Navigation("Serviciu");
                });

            modelBuilder.Entity("Clinica_medicala.Models.Medic", b =>
                {
                    b.Navigation("ServiciiPrestate");
                });

            modelBuilder.Entity("Clinica_medicala.Models.Pacient", b =>
                {
                    b.Navigation("Programari");
                });

            modelBuilder.Entity("Clinica_medicala.Models.Serviciu", b =>
                {
                    b.Navigation("Programari");

                    b.Navigation("ServiciiPrestate");
                });

            modelBuilder.Entity("Clinica_medicala.Models.Specializare", b =>
                {
                    b.Navigation("Servicii");
                });
#pragma warning restore 612, 618
        }
    }
}
