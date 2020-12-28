﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Silk.Core.Database;

namespace Silk.Core.Migrations
{
    [DbContext(typeof(SilkDbContext))]
    partial class SilkDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Silk.Core.Database.Models.Ban", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("GuildConfigModelId")
                        .HasColumnType("integer");

                    b.Property<string>("GuildId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("GuildId1")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("UserInfoDatabaseId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GuildConfigModelId");

                    b.HasIndex("GuildId1");

                    b.HasIndex("UserInfoDatabaseId");

                    b.ToTable("Ban");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.BlackListedWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("GuildConfigModelId")
                        .HasColumnType("integer");

                    b.Property<decimal?>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GuildConfigModelId");

                    b.HasIndex("GuildId");

                    b.ToTable("BlackListedWord");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.ChangelogModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Additions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Authors")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ChangeTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Removals")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ChangeLogs");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.GlobalUserModel", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<int>("Cash")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastCashOut")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("GlobalUsers");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.GuildConfigModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<bool>("AutoDehoist")
                        .HasColumnType("boolean");

                    b.Property<bool>("BlacklistInvites")
                        .HasColumnType("boolean");

                    b.Property<bool>("BlacklistWords")
                        .HasColumnType("boolean");

                    b.Property<decimal>("GeneralLoggingChannel")
                        .HasColumnType("numeric(20,0)");

                    b.Property<bool>("GreetMembers")
                        .HasColumnType("boolean");

                    b.Property<decimal>("GreetingChannel")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("InfractionFormat")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsPremium")
                        .HasColumnType("boolean");

                    b.Property<bool>("LogMessageChanges")
                        .HasColumnType("boolean");

                    b.Property<bool>("LogRoleChange")
                        .HasColumnType("boolean");

                    b.Property<decimal>("MuteRoleId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<bool>("ScanInvites")
                        .HasColumnType("boolean");

                    b.Property<bool>("UseAggressiveRegex")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("GuildConfigs");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.GuildInviteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("GuildConfigModelId")
                        .HasColumnType("integer");

                    b.Property<string>("GuildName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VanityURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GuildConfigModelId");

                    b.ToTable("GuildInviteModel");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.GuildModel", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<int?>("ConfigurationId")
                        .HasColumnType("integer");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.HasKey("Id");

                    b.HasIndex("ConfigurationId");

                    b.ToTable("Guilds");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.ItemModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<decimal>("OwnerId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.SelfAssignableRole", b =>
                {
                    b.Property<decimal>("RoleId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<int?>("GuildConfigModelId")
                        .HasColumnType("integer");

                    b.HasKey("RoleId");

                    b.HasIndex("GuildConfigModelId");

                    b.ToTable("SelfAssignableRole");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.TicketMessageHistoryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Sender")
                        .HasColumnType("numeric(20,0)");

                    b.Property<int>("TicketModelId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TicketModelId");

                    b.ToTable("TicketMessageHistoryModel");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.TicketModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Closed")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Opened")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Opener")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.TicketResponderModel", b =>
                {
                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("ResponderId")
                        .HasColumnType("numeric(20,0)");

                    b.ToTable("TicketResponderModel");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.UserInfractionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<decimal>("Enforcer")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTime>("InfractionTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("InfractionType")
                        .HasColumnType("integer");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserDatabaseId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("UserId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("UserDatabaseId");

                    b.HasIndex("GuildId", "UserId");

                    b.ToTable("UserInfractionModel");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.UserModel", b =>
                {
                    b.Property<long>("DatabaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("Flags")
                        .HasColumnType("integer");

                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("DatabaseId");

                    b.HasIndex("GuildId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.WhiteListedLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("GuildConfigModelId")
                        .HasColumnType("integer");

                    b.Property<decimal?>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<bool>("GuildLevelLink")
                        .HasColumnType("boolean");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GuildConfigModelId");

                    b.HasIndex("GuildId");

                    b.ToTable("WhiteListedLink");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.Ban", b =>
                {
                    b.HasOne("Silk.Core.Database.Models.GuildConfigModel", null)
                        .WithMany("Bans")
                        .HasForeignKey("GuildConfigModelId");

                    b.HasOne("Silk.Core.Database.Models.GuildModel", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId1");

                    b.HasOne("Silk.Core.Database.Models.UserModel", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoDatabaseId");

                    b.Navigation("Guild");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.BlackListedWord", b =>
                {
                    b.HasOne("Silk.Core.Database.Models.GuildConfigModel", null)
                        .WithMany("BlackListedWords")
                        .HasForeignKey("GuildConfigModelId");

                    b.HasOne("Silk.Core.Database.Models.GuildModel", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId");

                    b.Navigation("Guild");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.GuildInviteModel", b =>
                {
                    b.HasOne("Silk.Core.Database.Models.GuildConfigModel", null)
                        .WithMany("AllowedInvites")
                        .HasForeignKey("GuildConfigModelId");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.GuildModel", b =>
                {
                    b.HasOne("Silk.Core.Database.Models.GuildConfigModel", "Configuration")
                        .WithMany()
                        .HasForeignKey("ConfigurationId");

                    b.Navigation("Configuration");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.ItemModel", b =>
                {
                    b.HasOne("Silk.Core.Database.Models.GlobalUserModel", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.SelfAssignableRole", b =>
                {
                    b.HasOne("Silk.Core.Database.Models.GuildConfigModel", null)
                        .WithMany("SelfAssignableRoles")
                        .HasForeignKey("GuildConfigModelId");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.TicketMessageHistoryModel", b =>
                {
                    b.HasOne("Silk.Core.Database.Models.TicketModel", "TicketModel")
                        .WithMany("History")
                        .HasForeignKey("TicketModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TicketModel");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.UserInfractionModel", b =>
                {
                    b.HasOne("Silk.Core.Database.Models.UserModel", "User")
                        .WithMany("Infractions")
                        .HasForeignKey("UserDatabaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.UserModel", b =>
                {
                    b.HasOne("Silk.Core.Database.Models.GuildModel", "Guild")
                        .WithMany("Users")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guild");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.WhiteListedLink", b =>
                {
                    b.HasOne("Silk.Core.Database.Models.GuildConfigModel", null)
                        .WithMany("WhiteListedLinks")
                        .HasForeignKey("GuildConfigModelId");

                    b.HasOne("Silk.Core.Database.Models.GuildModel", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId");

                    b.Navigation("Guild");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.GlobalUserModel", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.GuildConfigModel", b =>
                {
                    b.Navigation("AllowedInvites");

                    b.Navigation("Bans");

                    b.Navigation("BlackListedWords");

                    b.Navigation("SelfAssignableRoles");

                    b.Navigation("WhiteListedLinks");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.GuildModel", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.TicketModel", b =>
                {
                    b.Navigation("History");
                });

            modelBuilder.Entity("Silk.Core.Database.Models.UserModel", b =>
                {
                    b.Navigation("Infractions");
                });
#pragma warning restore 612, 618
        }
    }
}
