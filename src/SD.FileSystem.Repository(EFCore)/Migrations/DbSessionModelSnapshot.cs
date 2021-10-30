﻿// <auto-generated />
using System;
using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SD.FileSystem.Repository.Base;

namespace SD.FileSystem.Repository.Migrations
{
    [DbContext(typeof(DbSession))]
    partial class DbSessionModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 31)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SD.FileSystem.Domain.Entities.File", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("CHAR(16) CHARACTER SET OCTETS");

                    b.Property<string>("AbsolutePath")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<DateTime>("AddedTime")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("CreatorAccount")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<string>("CreatorName")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("BOOLEAN");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Description")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<string>("ExtensionName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("VARCHAR(16)");

                    b.Property<string>("HashValue")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("VARCHAR(32)");

                    b.Property<string>("HostName")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<string>("Keywords")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("VARCHAR(64)");

                    b.Property<string>("Number")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<string>("OperatorAccount")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<string>("OperatorName")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<string>("RelativePath")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<DateTime>("SavedTime")
                        .HasColumnType("TIMESTAMP");

                    b.Property<long>("Size")
                        .HasColumnType("BIGINT");

                    b.Property<DateTime>("UploadedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Url")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<string>("Use")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AddedTime")
                        .HasDatabaseName("IX_AddedTime");

                    b.HasIndex("HashValue")
                        .HasDatabaseName("IX_HashValue");

                    b.ToTable("File");
                });
#pragma warning restore 612, 618
        }
    }
}
