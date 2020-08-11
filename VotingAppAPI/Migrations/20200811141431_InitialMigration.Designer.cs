﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VotingAppAPI.Data;

namespace VotingAppAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200811141431_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6");

            modelBuilder.Entity("VotingAppAPI.Models.Candidate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ElectionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Statement")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ElectionId");

                    b.ToTable("candidates");
                });

            modelBuilder.Entity("VotingAppAPI.Models.Election", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsOver")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("WinnerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("WinnerId");

                    b.ToTable("elections");
                });

            modelBuilder.Entity("VotingAppAPI.Models.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CandidateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ElectionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VoterId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("ElectionId");

                    b.HasIndex("VoterId");

                    b.ToTable("votes");
                });

            modelBuilder.Entity("VotingAppAPI.Models.Voter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("voters");
                });

            modelBuilder.Entity("VotingAppAPI.Models.Candidate", b =>
                {
                    b.HasOne("VotingAppAPI.Models.Election", null)
                        .WithMany("Candidates")
                        .HasForeignKey("ElectionId");
                });

            modelBuilder.Entity("VotingAppAPI.Models.Election", b =>
                {
                    b.HasOne("VotingAppAPI.Models.Candidate", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId");
                });

            modelBuilder.Entity("VotingAppAPI.Models.Vote", b =>
                {
                    b.HasOne("VotingAppAPI.Models.Candidate", "Candidate")
                        .WithMany()
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VotingAppAPI.Models.Election", "Election")
                        .WithMany("Votes")
                        .HasForeignKey("ElectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VotingAppAPI.Models.Voter", "Voter")
                        .WithMany()
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}