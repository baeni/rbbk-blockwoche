﻿// <auto-generated />
using Bücherei.Lib.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bücherei.Lib.Migrations
{
    [DbContext(typeof(DocumentContext))]
    partial class DocumentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Bücherei.Lib.EntitiesDocument.BuechereiDoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("buechereien", (string)null);
                });

            modelBuilder.Entity("Bücherei.Lib.EntitiesDocument.BuechereiDoc", b =>
                {
                    b.OwnsMany("Bücherei.Lib.EntitiesDocument.Autor", "Autoren", b1 =>
                        {
                            b1.Property<int>("BuechereiDocId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<string>("Nachname")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Vorname")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("BuechereiDocId", "Id");

                            b1.ToTable("buechereien");

                            b1.ToJson("Autoren");

                            b1.WithOwner()
                                .HasForeignKey("BuechereiDocId");

                            b1.OwnsMany("Bücherei.Lib.EntitiesDocument.Buch", "Buecher", b2 =>
                                {
                                    b2.Property<int>("AutorBuechereiDocId")
                                        .HasColumnType("integer");

                                    b2.Property<int>("AutorId")
                                        .HasColumnType("integer");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Titel")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("AutorBuechereiDocId", "AutorId", "Id");

                                    b2.ToTable("buechereien");

                                    b2.WithOwner()
                                        .HasForeignKey("AutorBuechereiDocId", "AutorId");
                                });

                            b1.Navigation("Buecher");
                        });

                    b.Navigation("Autoren");
                });
#pragma warning restore 612, 618
        }
    }
}
