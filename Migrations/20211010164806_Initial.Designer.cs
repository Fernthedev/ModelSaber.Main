﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModelSaber.Database;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ModelSaber.Main.Migrations
{
    [DbContext(typeof(ModelSaberDbContext))]
    [Migration("20211010164806_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ModelSaber.Database.Models.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date");

                    b.Property<string>("Hash")
                        .HasColumnType("text")
                        .HasColumnName("hash");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<byte>("Platform")
                        .HasColumnType("smallint")
                        .HasColumnName("platform");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.Property<byte>("ThumbnailExt")
                        .HasColumnType("smallint")
                        .HasColumnName("thumbnail_ext");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint")
                        .HasColumnName("type");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<Guid>("Uuid")
                        .HasColumnType("uuid")
                        .HasColumnName("uuid");

                    b.HasKey("Id")
                        .HasName("pk_models");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_models_user_id");

                    b.ToTable("models");
                });

            modelBuilder.Entity("ModelSaber.Database.Models.ModelTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ModelId")
                        .HasColumnType("integer")
                        .HasColumnName("model_id");

                    b.Property<int>("TagId")
                        .HasColumnType("integer")
                        .HasColumnName("tag_id");

                    b.HasKey("Id")
                        .HasName("pk_model_tags");

                    b.HasIndex("ModelId")
                        .HasDatabaseName("ix_model_tags_model_id");

                    b.HasIndex("TagId")
                        .HasDatabaseName("ix_model_tags_tag_id");

                    b.ToTable("model_tags");
                });

            modelBuilder.Entity("ModelSaber.Database.Models.ModelVariation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ModelId")
                        .HasColumnType("integer")
                        .HasColumnName("model_id");

                    b.Property<int>("ParentModelId")
                        .HasColumnType("integer")
                        .HasColumnName("parent_model_id");

                    b.HasKey("Id")
                        .HasName("pk_model_variations");

                    b.HasIndex("ModelId")
                        .IsUnique()
                        .HasDatabaseName("ix_model_variations_model_id");

                    b.HasIndex("ParentModelId")
                        .HasDatabaseName("ix_model_variations_parent_model_id");

                    b.ToTable("model_variations");
                });

            modelBuilder.Entity("ModelSaber.Database.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_tags");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("ModelSaber.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BSaber")
                        .HasColumnType("text")
                        .HasColumnName("b_saber");

                    b.Property<ulong?>("DiscordId")
                        .HasColumnType("bigint")
                        .HasColumnName("discord_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("ModelSaber.Database.Models.Model", b =>
                {
                    b.HasOne("ModelSaber.Database.Models.User", "User")
                        .WithMany("Models")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_models_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelSaber.Database.Models.ModelTag", b =>
                {
                    b.HasOne("ModelSaber.Database.Models.Model", "Model")
                        .WithMany("Tags")
                        .HasForeignKey("ModelId")
                        .HasConstraintName("fk_model_tags_models_model_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelSaber.Database.Models.Tag", "Tag")
                        .WithMany("ModelTags")
                        .HasForeignKey("TagId")
                        .HasConstraintName("fk_model_tags_tags_tag_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("ModelSaber.Database.Models.ModelVariation", b =>
                {
                    b.HasOne("ModelSaber.Database.Models.Model", "Model")
                        .WithOne("ModelVariation")
                        .HasForeignKey("ModelSaber.Database.Models.ModelVariation", "ModelId")
                        .HasConstraintName("fk_model_variations_models_model_id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelSaber.Database.Models.Model", "ParentModel")
                        .WithMany("ModelVariations")
                        .HasForeignKey("ParentModelId")
                        .HasConstraintName("fk_model_variations_models_model_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");

                    b.Navigation("ParentModel");
                });

            modelBuilder.Entity("ModelSaber.Database.Models.Model", b =>
                {
                    b.Navigation("ModelVariation");

                    b.Navigation("ModelVariations");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("ModelSaber.Database.Models.Tag", b =>
                {
                    b.Navigation("ModelTags");
                });

            modelBuilder.Entity("ModelSaber.Database.Models.User", b =>
                {
                    b.Navigation("Models");
                });
#pragma warning restore 612, 618
        }
    }
}
