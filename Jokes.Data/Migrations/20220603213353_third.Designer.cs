﻿// <auto-generated />
using System;
using Jokes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Jokes.Data.Migrations
{
    [DbContext(typeof(JokeDataContext))]
    [Migration("20220603213353_third")]
    partial class third
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Jokes.Data.Joke", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Dislikes")
                        .HasColumnType("int");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<int>("OriginalId")
                        .HasColumnType("int");

                    b.Property<string>("Punchline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Setup")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Jokes");
                });

            modelBuilder.Entity("Jokes.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Jokes.Data.UserLikedJokes", b =>
                {
                    b.Property<int>("JokeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateLiked")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Liked")
                        .HasColumnType("bit");

                    b.HasKey("JokeId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserLikedJokes");
                });

            modelBuilder.Entity("Jokes.Data.UserLikedJokes", b =>
                {
                    b.HasOne("Jokes.Data.Joke", null)
                        .WithMany("UserLikedJokes")
                        .HasForeignKey("JokeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Jokes.Data.User", null)
                        .WithMany("UserLikedJokes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Jokes.Data.Joke", b =>
                {
                    b.Navigation("UserLikedJokes");
                });

            modelBuilder.Entity("Jokes.Data.User", b =>
                {
                    b.Navigation("UserLikedJokes");
                });
#pragma warning restore 612, 618
        }
    }
}