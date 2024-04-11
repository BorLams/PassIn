﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PassIn.Infrastructure.Contexts;

#nullable disable

namespace PassIn.Api.Migrations
{
    [DbContext(typeof(PassInDbContext))]
    [Migration("20240411145445_AjustandoChavesEntreAttendeeECheckIn")]
    partial class AjustandoChavesEntreAttendeeECheckIn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("PassIn.Domain.Entities.Attendee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<Guid>("EventId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("PassIn.Domain.Entities.CheckIn", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AttendeeId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CheckedInAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AttendeeId")
                        .IsUnique();

                    b.ToTable("CheckIns");
                });

            modelBuilder.Entity("PassIn.Domain.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Details")
                        .HasColumnType("longtext");

                    b.Property<int>("MaximumAttendees")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("PassIn.Domain.Entities.Attendee", b =>
                {
                    b.HasOne("PassIn.Domain.Entities.Event", "Event")
                        .WithMany("Attendees")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("PassIn.Domain.Entities.CheckIn", b =>
                {
                    b.HasOne("PassIn.Domain.Entities.Attendee", "Attendee")
                        .WithOne("CheckIn")
                        .HasForeignKey("PassIn.Domain.Entities.CheckIn", "AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attendee");
                });

            modelBuilder.Entity("PassIn.Domain.Entities.Attendee", b =>
                {
                    b.Navigation("CheckIn");
                });

            modelBuilder.Entity("PassIn.Domain.Entities.Event", b =>
                {
                    b.Navigation("Attendees");
                });
#pragma warning restore 612, 618
        }
    }
}
