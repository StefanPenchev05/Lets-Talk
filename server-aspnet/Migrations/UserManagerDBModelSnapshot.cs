﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Data;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(UserManagerDB))]
    partial class UserManagerDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Server.Models.Channel", b =>
                {
                    b.Property<int>("ChannelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ImageURL")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("ChannelId");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("Server.Models.DirectMessage", b =>
                {
                    b.Property<int>("DirectMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.HasKey("DirectMessageId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("DirectMessage");
                });

            modelBuilder.Entity("Server.Models.FriendRequest", b =>
                {
                    b.Property<int>("FriendRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("RequestedId")
                        .HasColumnType("int");

                    b.Property<int>("RequesterId")
                        .HasColumnType("int");

                    b.HasKey("FriendRequestId");

                    b.HasIndex("RequestedId");

                    b.HasIndex("RequesterId");

                    b.ToTable("FriendRequests");
                });

            modelBuilder.Entity("Server.Models.Friendship", b =>
                {
                    b.Property<int>("FriendshipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FriendId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FriendshipId");

                    b.HasIndex("FriendId");

                    b.HasIndex("UserId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("Server.Models.LoginLocations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LoginBrowser")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LoginDeviceName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LoginDeviceOperatingSystem")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SettingsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SettingsId")
                        .IsUnique();

                    b.ToTable("LoginLocations");
                });

            modelBuilder.Entity("Server.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MessageId");

                    b.HasIndex("ChannelId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Server.Models.NotificationSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("ReceiveEmailNotifications")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("ReceivePushNotifications")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("SettingsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SettingsId")
                        .IsUnique();

                    b.ToTable("NotificationSettings");
                });

            modelBuilder.Entity("Server.Models.PreferenceSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Language")
                        .HasColumnType("longtext");

                    b.Property<int>("SettingsId")
                        .HasColumnType("int");

                    b.Property<int>("Theme")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SettingsId")
                        .IsUnique();

                    b.ToTable("PreferenceSettings");
                });

            modelBuilder.Entity("Server.Models.PrivacySettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("AllowDirectMessages")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("SettingsId")
                        .HasColumnType("int");

                    b.Property<bool>("ShowOnlineStatus")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("SettingsId")
                        .IsUnique();

                    b.ToTable("PrivacySettings");
                });

            modelBuilder.Entity("Server.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("RoleType")
                        .HasColumnType("int");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Server.Models.SecuritySettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("AccountLockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FailedLoginAttempts")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastPasswordChange")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SettingsId")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorAuth")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TwoFactorAuthCode")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("SettingsId")
                        .IsUnique();

                    b.ToTable("SecuritySettings");
                });

            modelBuilder.Entity("Server.Models.SessionStore", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime?>("AbsoluteExpiration")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpiresAtTime")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong?>("SlidingExpirationInSeconds")
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Value")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SesssionStore");
                });

            modelBuilder.Entity("Server.Models.Settings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Server.Models.TempData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("ProfilePictureURL")
                        .HasColumnType("longtext");

                    b.Property<bool?>("TwoFactorAuth")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.Property<string>("VerificationCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("tempDatas");
                });

            modelBuilder.Entity("Server.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProfilePictureURL")
                        .HasColumnType("longtext");

                    b.Property<int>("SettingsId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Server.Models.UserChannel", b =>
                {
                    b.Property<int>("UserChannelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("TimeInChannel")
                        .HasColumnType("time(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserChannelId");

                    b.HasIndex("ChannelId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserChannels");
                });

            modelBuilder.Entity("Server.Models.DirectMessage", b =>
                {
                    b.HasOne("Server.Models.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Server.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Server.Models.FriendRequest", b =>
                {
                    b.HasOne("Server.Models.User", "Requested")
                        .WithMany()
                        .HasForeignKey("RequestedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Server.Models.User", "Requester")
                        .WithMany("FriendRequests")
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Requested");

                    b.Navigation("Requester");
                });

            modelBuilder.Entity("Server.Models.Friendship", b =>
                {
                    b.HasOne("Server.Models.User", "Friend")
                        .WithMany("FriendOf")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Server.Models.User", "User")
                        .WithMany("Friendships")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Friend");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Server.Models.LoginLocations", b =>
                {
                    b.HasOne("Server.Models.Settings", "Settings")
                        .WithOne("LoginLocations")
                        .HasForeignKey("Server.Models.LoginLocations", "SettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("Server.Models.Message", b =>
                {
                    b.HasOne("Server.Models.Channel", "Channel")
                        .WithMany("Messages")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Server.Models.NotificationSettings", b =>
                {
                    b.HasOne("Server.Models.Settings", "Settings")
                        .WithOne("NotificationSettings")
                        .HasForeignKey("Server.Models.NotificationSettings", "SettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("Server.Models.PreferenceSettings", b =>
                {
                    b.HasOne("Server.Models.Settings", "Settings")
                        .WithOne("PreferenceSettings")
                        .HasForeignKey("Server.Models.PreferenceSettings", "SettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("Server.Models.PrivacySettings", b =>
                {
                    b.HasOne("Server.Models.Settings", "Settings")
                        .WithOne("PrivacySettings")
                        .HasForeignKey("Server.Models.PrivacySettings", "SettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("Server.Models.SecuritySettings", b =>
                {
                    b.HasOne("Server.Models.Settings", "Settings")
                        .WithOne("SecuritySettings")
                        .HasForeignKey("Server.Models.SecuritySettings", "SettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("Server.Models.SessionStore", b =>
                {
                    b.HasOne("Server.Models.User", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Server.Models.Settings", b =>
                {
                    b.HasOne("Server.Models.User", "User")
                        .WithOne("Settings")
                        .HasForeignKey("Server.Models.Settings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Server.Models.UserChannel", b =>
                {
                    b.HasOne("Server.Models.Channel", "Channel")
                        .WithMany("UserChannels")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Role", "Role")
                        .WithMany("UserChannels")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.User", "User")
                        .WithMany("UserChannels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Server.Models.Channel", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserChannels");
                });

            modelBuilder.Entity("Server.Models.Role", b =>
                {
                    b.Navigation("UserChannels");
                });

            modelBuilder.Entity("Server.Models.Settings", b =>
                {
                    b.Navigation("LoginLocations");

                    b.Navigation("NotificationSettings");

                    b.Navigation("PreferenceSettings");

                    b.Navigation("PrivacySettings");

                    b.Navigation("SecuritySettings");
                });

            modelBuilder.Entity("Server.Models.User", b =>
                {
                    b.Navigation("FriendOf");

                    b.Navigation("FriendRequests");

                    b.Navigation("Friendships");

                    b.Navigation("Messages");

                    b.Navigation("Sessions");

                    b.Navigation("Settings");

                    b.Navigation("UserChannels");
                });
#pragma warning restore 612, 618
        }
    }
}
