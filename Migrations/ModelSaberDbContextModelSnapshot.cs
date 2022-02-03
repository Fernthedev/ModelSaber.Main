﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModelSaber.Database;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ModelSaber.Main.Migrations
{
    [DbContext(typeof(ModelSaberDbContext))]
    partial class ModelSaberDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ModelSaber.Models.Model", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Hash")
                        .HasColumnType("text")
                        .HasColumnName("hash");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<byte>("Platform")
                        .HasColumnType("smallint")
                        .HasColumnName("platform");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.Property<byte?>("ThumbnailExt")
                        .HasColumnType("smallint")
                        .HasColumnName("thumbnail_ext");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint")
                        .HasColumnName("type");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<Guid>("Uuid")
                        .HasColumnType("uuid")
                        .HasColumnName("uuid");

                    b.HasKey("Id")
                        .HasName("pk_models");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_models_user_id");

                    b.ToTable("models", (string)null);
                });

            modelBuilder.Entity("ModelSaber.Models.ModelTag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ModelId")
                        .HasColumnType("bigint")
                        .HasColumnName("model_id");

                    b.Property<long>("TagId")
                        .HasColumnType("bigint")
                        .HasColumnName("tag_id");

                    b.HasKey("Id")
                        .HasName("pk_model_tags");

                    b.HasIndex("ModelId")
                        .HasDatabaseName("ix_model_tags_model_id");

                    b.HasIndex("TagId")
                        .HasDatabaseName("ix_model_tags_tag_id");

                    b.ToTable("model_tags", (string)null);
                });

            modelBuilder.Entity("ModelSaber.Models.ModelUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ModelId")
                        .HasColumnType("bigint")
                        .HasColumnName("model_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_model_users");

                    b.HasIndex("ModelId")
                        .HasDatabaseName("ix_model_users_model_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_model_users_user_id");

                    b.ToTable("model_users", (string)null);
                });

            modelBuilder.Entity("ModelSaber.Models.ModelVariation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ModelId")
                        .HasColumnType("bigint")
                        .HasColumnName("model_id");

                    b.Property<long>("ParentModelId")
                        .HasColumnType("bigint")
                        .HasColumnName("parent_model_id");

                    b.HasKey("Id")
                        .HasName("pk_model_variations");

                    b.HasIndex("ModelId")
                        .IsUnique()
                        .HasDatabaseName("ix_model_variations_model_id");

                    b.HasIndex("ParentModelId")
                        .HasDatabaseName("ix_model_variations_parent_model_id");

                    b.ToTable("model_variations", (string)null);
                });

            modelBuilder.Entity("ModelSaber.Models.OAuthClient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid")
                        .HasColumnName("client_id");

                    b.Property<string>("ClientSecret")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("client_secret");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("RedirectUri")
                        .HasColumnType("text")
                        .HasColumnName("redirect_uri");

                    b.Property<string>("Scope")
                        .HasColumnType("text")
                        .HasColumnName("scope");

                    b.Property<byte[]>("SecKey")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("sec_key");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_o_auth_clients");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_o_auth_clients_user_id");

                    b.ToTable("o_auth_clients", (string)null);
                });

            modelBuilder.Entity("ModelSaber.Models.OAuthToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint")
                        .HasColumnName("client_id");

                    b.Property<long>("ExpiresIn")
                        .HasColumnType("bigint")
                        .HasColumnName("expires_in");

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("inserted_at");

                    b.Property<byte[]>("Refresh")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("refresh");

                    b.Property<string>("Scope")
                        .HasColumnType("text")
                        .HasColumnName("scope");

                    b.Property<byte[]>("Token")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("token");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_o_auth_tokens");

                    b.HasIndex("ClientId")
                        .HasDatabaseName("ix_o_auth_tokens_client_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_o_auth_tokens_user_id");

                    b.ToTable("o_auth_tokens", (string)null);
                });

            modelBuilder.Entity("ModelSaber.Models.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<Guid>("CursorId")
                        .HasColumnType("uuid")
                        .HasColumnName("cursor_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_tags");

                    b.ToTable("tags", (string)null);
                });

            modelBuilder.Entity("ModelSaber.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Avatar")
                        .HasColumnType("text")
                        .HasColumnName("avatar");

                    b.Property<string>("BSaber")
                        .HasColumnType("text")
                        .HasColumnName("b_saber");

                    b.Property<decimal?>("DiscordId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("discord_id");

                    b.Property<byte>("Level")
                        .HasColumnType("smallint")
                        .HasColumnName("level");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("ModelSaber.Models.UserLogons", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_logons");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_logons_user_id");

                    b.ToTable("logons", (string)null);
                });

            modelBuilder.Entity("ModelSaber.Models.UserTags", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_tags");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_tags_user_id");

                    b.ToTable("user_tags", (string)null);
                });

            modelBuilder.Entity("ModelSaber.Models.Vote", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("DownVote")
                        .HasColumnType("boolean")
                        .HasColumnName("down_vote");

                    b.Property<string>("GameId")
                        .HasColumnType("text")
                        .HasColumnName("game_id");

                    b.Property<long>("ModelId")
                        .HasColumnType("bigint")
                        .HasColumnName("model_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_votes");

                    b.HasIndex("ModelId")
                        .HasDatabaseName("ix_votes_model_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_votes_user_id");

                    b.ToTable("votes", (string)null);
                });

            modelBuilder.Entity("ModelSaber.Models.Model", b =>
                {
                    b.HasOne("ModelSaber.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_models_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelSaber.Models.ModelTag", b =>
                {
                    b.HasOne("ModelSaber.Models.Model", "Model")
                        .WithMany("Tags")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_model_tags_models_model_id");

                    b.HasOne("ModelSaber.Models.Tag", "Tag")
                        .WithMany("ModelTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_model_tags_tags_tag_id");

                    b.Navigation("Model");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("ModelSaber.Models.ModelUser", b =>
                {
                    b.HasOne("ModelSaber.Models.Model", "Model")
                        .WithMany("Users")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_model_users_models_model_id");

                    b.HasOne("ModelSaber.Models.User", "User")
                        .WithMany("Models")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_model_users_users_user_id");

                    b.Navigation("Model");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelSaber.Models.ModelVariation", b =>
                {
                    b.HasOne("ModelSaber.Models.Model", "Model")
                        .WithOne("ModelVariation")
                        .HasForeignKey("ModelSaber.Models.ModelVariation", "ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_model_variations_models_model_id1");

                    b.HasOne("ModelSaber.Models.Model", "ParentModel")
                        .WithMany("ModelVariations")
                        .HasForeignKey("ParentModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_model_variations_models_model_id");

                    b.Navigation("Model");

                    b.Navigation("ParentModel");
                });

            modelBuilder.Entity("ModelSaber.Models.OAuthClient", b =>
                {
                    b.HasOne("ModelSaber.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_o_auth_clients_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelSaber.Models.OAuthToken", b =>
                {
                    b.HasOne("ModelSaber.Models.OAuthClient", "Client")
                        .WithMany("Tokens")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_o_auth_tokens_o_auth_clients_client_id");

                    b.HasOne("ModelSaber.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_o_auth_tokens_users_user_id");

                    b.Navigation("Client");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelSaber.Models.UserLogons", b =>
                {
                    b.HasOne("ModelSaber.Models.User", "User")
                        .WithOne("Logon")
                        .HasForeignKey("ModelSaber.Models.UserLogons", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_logons_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelSaber.Models.UserTags", b =>
                {
                    b.HasOne("ModelSaber.Models.User", "User")
                        .WithMany("UserTags")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_tags_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelSaber.Models.Vote", b =>
                {
                    b.HasOne("ModelSaber.Models.Model", "Model")
                        .WithMany("Votes")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_votes_models_model_id");

                    b.HasOne("ModelSaber.Models.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_votes_users_user_id");

                    b.Navigation("Model");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelSaber.Models.Model", b =>
                {
                    b.Navigation("ModelVariation")
                        .IsRequired();

                    b.Navigation("ModelVariations");

                    b.Navigation("Tags");

                    b.Navigation("Users");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("ModelSaber.Models.OAuthClient", b =>
                {
                    b.Navigation("Tokens");
                });

            modelBuilder.Entity("ModelSaber.Models.Tag", b =>
                {
                    b.Navigation("ModelTags");
                });

            modelBuilder.Entity("ModelSaber.Models.User", b =>
                {
                    b.Navigation("Logon")
                        .IsRequired();

                    b.Navigation("Models");

                    b.Navigation("UserTags");

                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
