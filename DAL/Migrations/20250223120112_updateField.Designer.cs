﻿// <auto-generated />
using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(NewsContext))]
    [Migration("20250223120112_updateField")]
    partial class updateField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DAL.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryDescription = "News related to political affairs and government updates",
                            CategoryName = "Politics",
                            IsActive = true
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryDescription = "Latest updates from the sports industry",
                            CategoryName = "Sports",
                            IsActive = true
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryDescription = "News on technological advancements and innovations",
                            CategoryName = "Technology",
                            IsActive = true
                        },
                        new
                        {
                            CategoryId = 4,
                            CategoryDescription = "Covers movies, music, and celebrity news",
                            CategoryName = "Entertainment",
                            IsActive = true
                        },
                        new
                        {
                            CategoryId = 5,
                            CategoryDescription = "News related to elections worldwide",
                            CategoryName = "Elections",
                            IsActive = true,
                            ParentCategoryId = 1
                        },
                        new
                        {
                            CategoryId = 6,
                            CategoryDescription = "Latest football news and match updates",
                            CategoryName = "Football",
                            IsActive = true,
                            ParentCategoryId = 2
                        },
                        new
                        {
                            CategoryId = 7,
                            CategoryDescription = "Latest updates on artificial intelligence and machine learning",
                            CategoryName = "AI & Machine Learning",
                            IsActive = true,
                            ParentCategoryId = 3
                        },
                        new
                        {
                            CategoryId = 8,
                            CategoryDescription = "News on Hollywood movies and celebrities",
                            CategoryName = "Hollywood",
                            IsActive = true,
                            ParentCategoryId = 4
                        });
                });

            modelBuilder.Entity("DAL.Entities.NewsArticle", b =>
                {
                    b.Property<string>("NewsArticleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Headline")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewsContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewsSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("NewsStatus")
                        .HasColumnType("bit");

                    b.Property<string>("NewsTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("NewsArticleId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("NewsArticles");
                });

            modelBuilder.Entity("DAL.Entities.NewsTag", b =>
                {
                    b.Property<string>("NewsArticleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("NewsArticleId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("NewsTags");
                });

            modelBuilder.Entity("DAL.Entities.SystemAccount", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<string>("AccountEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AccountPasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AccountRole")
                        .HasColumnType("int");

                    b.HasKey("AccountId");

                    b.ToTable("SystemAccounts");

                    b.HasData(
                        new
                        {
                            AccountId = -1,
                            AccountEmail = "admin@example.com",
                            AccountName = "Kha UwU",
                            AccountPasswordHash = "$2a$11$rwxlp.y4gjXvIW7IreN0LOpB9RIptTa/AnFIq0CM9GaEAaXcWKhDa",
                            AccountRole = 3
                        },
                        new
                        {
                            AccountId = -2,
                            AccountEmail = "lecturer@example.com",
                            AccountName = "John",
                            AccountPasswordHash = "$2a$11$r4p3qfWEXlXdPjUwOpF0DOKoTga8YV7Q9TKKHfX7XNH8GaZV.Mp/m",
                            AccountRole = 2
                        },
                        new
                        {
                            AccountId = -3,
                            AccountEmail = "staff@example.com",
                            AccountName = "Larry",
                            AccountPasswordHash = "$2a$11$XhFt4joOsZAtT2U3JZnc4OF16cwVwZ/rA6VMB8wR9UizPs9rf5/we",
                            AccountRole = 1
                        });
                });

            modelBuilder.Entity("DAL.Entities.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TagName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            TagId = 1,
                            Note = "Important and urgent news updates.",
                            TagName = "Breaking News"
                        },
                        new
                        {
                            TagId = 2,
                            Note = "Covers political topics and government affairs.",
                            TagName = "Politics"
                        },
                        new
                        {
                            TagId = 3,
                            Note = "News about sports, teams, and events.",
                            TagName = "Sports"
                        },
                        new
                        {
                            TagId = 4,
                            Note = "Covers tech innovations, AI, and software updates.",
                            TagName = "Technology"
                        },
                        new
                        {
                            TagId = 5,
                            Note = "Covers movies, music, and celebrity news.",
                            TagName = "Entertainment"
                        },
                        new
                        {
                            TagId = 6,
                            Note = "Covers stock markets, economy, and business news.",
                            TagName = "Finance"
                        },
                        new
                        {
                            TagId = 7,
                            Note = "Medical news, fitness, and health tips.",
                            TagName = "Health"
                        },
                        new
                        {
                            TagId = 8,
                            Note = "International news and global events.",
                            TagName = "World"
                        },
                        new
                        {
                            TagId = 9,
                            Note = "Scientific discoveries and space exploration.",
                            TagName = "Science"
                        },
                        new
                        {
                            TagId = 10,
                            Note = "Latest trends in artificial intelligence and ML.",
                            TagName = "AI & Machine Learning"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Category", b =>
                {
                    b.HasOne("DAL.Entities.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("DAL.Entities.NewsArticle", b =>
                {
                    b.HasOne("DAL.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DAL.Entities.SystemAccount", "CreatedBy")
                        .WithMany("CreatedArticles")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("DAL.Entities.SystemAccount", "UpdatedBy")
                        .WithMany("UpdatedArticles")
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Category");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("DAL.Entities.NewsTag", b =>
                {
                    b.HasOne("DAL.Entities.NewsArticle", "NewsArticle")
                        .WithMany("NewsTags")
                        .HasForeignKey("NewsArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.Tag", "Tag")
                        .WithMany("NewsTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("NewsArticle");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("DAL.Entities.NewsArticle", b =>
                {
                    b.Navigation("NewsTags");
                });

            modelBuilder.Entity("DAL.Entities.SystemAccount", b =>
                {
                    b.Navigation("CreatedArticles");

                    b.Navigation("UpdatedArticles");
                });

            modelBuilder.Entity("DAL.Entities.Tag", b =>
                {
                    b.Navigation("NewsTags");
                });
#pragma warning restore 612, 618
        }
    }
}
