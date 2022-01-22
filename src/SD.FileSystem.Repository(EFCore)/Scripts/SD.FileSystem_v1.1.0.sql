USE [master]
GO

CREATE DATABASE [SD.FileSystem]
GO

USE [SD.FileSystem]
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [File] (
    [Id] uniqueidentifier NOT NULL,
    [ExtensionName] nvarchar(16) NOT NULL,
    [Size] bigint NOT NULL,
    [HashValue] nvarchar(32) NOT NULL,
    [RelativePath] nvarchar(max) NULL,
    [AbsolutePath] nvarchar(max) NULL,
    [HostName] nvarchar(max) NULL,
    [Url] nvarchar(max) NULL,
    [UploadedDate] DATE NOT NULL,
    [Use] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [PartitionIndex] int NOT NULL,
    [AddedTime] datetime2 NOT NULL,
    [Number] nvarchar(64) NOT NULL,
    [Name] nvarchar(256) NOT NULL,
    [Keywords] nvarchar(max) NOT NULL,
    [SavedTime] datetime2 NOT NULL,
    [CreatorAccount] nvarchar(max) NULL,
    [CreatorName] nvarchar(max) NULL,
    [OperatorAccount] nvarchar(max) NULL,
    [OperatorName] nvarchar(max) NULL,
    CONSTRAINT [PK_File] PRIMARY KEY NONCLUSTERED ([Id])
);
GO

CREATE CLUSTERED INDEX [IX_File_AddedTime] ON [File] ([AddedTime]);
GO

CREATE INDEX [IX_File_HashValue] ON [File] ([HashValue]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220122085656_v1.1.0', N'5.0.10');
GO

COMMIT;
GO
