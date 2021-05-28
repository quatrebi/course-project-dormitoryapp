
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/26/2021 02:21:56
-- Generated from EDMX file: D:\Documents\Visual Studio\DormitoryApp\DormitoryDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dormitorydb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CitizenUniversityInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UniversityInfos] DROP CONSTRAINT [FK_CitizenUniversityInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_CitizenDormitoryInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DormitoryInfos] DROP CONSTRAINT [FK_CitizenDormitoryInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_DormitoryInfoDormitory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DormitoryInfos] DROP CONSTRAINT [FK_DormitoryInfoDormitory];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomDormitoryInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DormitoryInfos] DROP CONSTRAINT [FK_RoomDormitoryInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomEmployeeLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeLogs] DROP CONSTRAINT [FK_RoomEmployeeLog];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountHuman]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Humans] DROP CONSTRAINT [FK_AccountHuman];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeDormitoryInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DormitoryInfos] DROP CONSTRAINT [FK_EmployeeDormitoryInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeEmployeeLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeLogs] DROP CONSTRAINT [FK_EmployeeEmployeeLog];
GO
IF OBJECT_ID(N'[dbo].[FK_Citizen_inherits_Human]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Humans_Citizen] DROP CONSTRAINT [FK_Citizen_inherits_Human];
GO
IF OBJECT_ID(N'[dbo].[FK_Employee_inherits_Human]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Humans_Employee] DROP CONSTRAINT [FK_Employee_inherits_Human];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[MenuButtons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MenuButtons];
GO
IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[EmployeeLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeLogs];
GO
IF OBJECT_ID(N'[dbo].[DormitoryInfos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DormitoryInfos];
GO
IF OBJECT_ID(N'[dbo].[Dormitories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dormitories];
GO
IF OBJECT_ID(N'[dbo].[UniversityInfos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UniversityInfos];
GO
IF OBJECT_ID(N'[dbo].[Rooms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rooms];
GO
IF OBJECT_ID(N'[dbo].[Humans]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Humans];
GO
IF OBJECT_ID(N'[dbo].[Humans_Citizen]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Humans_Citizen];
GO
IF OBJECT_ID(N'[dbo].[Humans_Employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Humans_Employee];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MenuButtons'
CREATE TABLE [dbo].[MenuButtons] (
    [MBID] int  NOT NULL,
    [Caption] nvarchar(max)  NULL,
    [ImageSource] nvarchar(max)  NULL,
    [Permission] smallint  NOT NULL,
    [ViewName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [AID] int  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Permission] smallint  NOT NULL
);
GO

-- Creating table 'EmployeeLogs'
CREATE TABLE [dbo].[EmployeeLogs] (
    [ELID] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Datetime] datetime  NOT NULL,
    [RoomRID] int  NULL,
    [EmployeeHID] int  NOT NULL
);
GO

-- Creating table 'DormitoryInfos'
CREATE TABLE [dbo].[DormitoryInfos] (
    [DIID] int  NOT NULL,
    [OrderNumber] int  NULL,
    [CheckInDate] datetime  NULL,
    [CheckOutDate] datetime  NULL,
    [DormitoryDID] int  NULL,
    [RoomRID] int  NULL,
    [Citizen_HID] int  NULL,
    [Employee_HID] int  NULL
);
GO

-- Creating table 'Dormitories'
CREATE TABLE [dbo].[Dormitories] (
    [DID] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [NumberOfFloors] int  NOT NULL,
    [RoomsPerFloor] int  NOT NULL,
    [CitizensPerRoom] int  NOT NULL
);
GO

-- Creating table 'UniversityInfos'
CREATE TABLE [dbo].[UniversityInfos] (
    [UIID] int  NOT NULL,
    [University] nvarchar(max)  NOT NULL,
    [Faculty] nvarchar(max)  NOT NULL,
    [Specialty] nvarchar(max)  NOT NULL,
    [GroupNumber] int  NOT NULL,
    [Citizen_HID] int  NOT NULL
);
GO

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [RID] int  NOT NULL,
    [Number] smallint  NOT NULL,
    [Floor] smallint  NOT NULL,
    [DormitoryDID] int  NOT NULL
);
GO

-- Creating table 'Humans'
CREATE TABLE [dbo].[Humans] (
    [HID] int  NOT NULL,
    [Surname] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NULL,
    [Patronymic] nvarchar(max)  NULL,
    [Account_AID] int  NOT NULL
);
GO

-- Creating table 'Humans_Citizen'
CREATE TABLE [dbo].[Humans_Citizen] (
    [HID] int  NOT NULL
);
GO

-- Creating table 'Humans_Employee'
CREATE TABLE [dbo].[Humans_Employee] (
    [Position] nvarchar(max)  NOT NULL,
    [HID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MBID] in table 'MenuButtons'
ALTER TABLE [dbo].[MenuButtons]
ADD CONSTRAINT [PK_MenuButtons]
    PRIMARY KEY CLUSTERED ([MBID] ASC);
GO

-- Creating primary key on [AID] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([AID] ASC);
GO

-- Creating primary key on [ELID] in table 'EmployeeLogs'
ALTER TABLE [dbo].[EmployeeLogs]
ADD CONSTRAINT [PK_EmployeeLogs]
    PRIMARY KEY CLUSTERED ([ELID] ASC);
GO

-- Creating primary key on [DIID] in table 'DormitoryInfos'
ALTER TABLE [dbo].[DormitoryInfos]
ADD CONSTRAINT [PK_DormitoryInfos]
    PRIMARY KEY CLUSTERED ([DIID] ASC);
GO

-- Creating primary key on [DID] in table 'Dormitories'
ALTER TABLE [dbo].[Dormitories]
ADD CONSTRAINT [PK_Dormitories]
    PRIMARY KEY CLUSTERED ([DID] ASC);
GO

-- Creating primary key on [UIID] in table 'UniversityInfos'
ALTER TABLE [dbo].[UniversityInfos]
ADD CONSTRAINT [PK_UniversityInfos]
    PRIMARY KEY CLUSTERED ([UIID] ASC);
GO

-- Creating primary key on [RID] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([RID] ASC);
GO

-- Creating primary key on [HID] in table 'Humans'
ALTER TABLE [dbo].[Humans]
ADD CONSTRAINT [PK_Humans]
    PRIMARY KEY CLUSTERED ([HID] ASC);
GO

-- Creating primary key on [HID] in table 'Humans_Citizen'
ALTER TABLE [dbo].[Humans_Citizen]
ADD CONSTRAINT [PK_Humans_Citizen]
    PRIMARY KEY CLUSTERED ([HID] ASC);
GO

-- Creating primary key on [HID] in table 'Humans_Employee'
ALTER TABLE [dbo].[Humans_Employee]
ADD CONSTRAINT [PK_Humans_Employee]
    PRIMARY KEY CLUSTERED ([HID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Citizen_HID] in table 'UniversityInfos'
ALTER TABLE [dbo].[UniversityInfos]
ADD CONSTRAINT [FK_CitizenUniversityInfo]
    FOREIGN KEY ([Citizen_HID])
    REFERENCES [dbo].[Humans_Citizen]
        ([HID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CitizenUniversityInfo'
CREATE INDEX [IX_FK_CitizenUniversityInfo]
ON [dbo].[UniversityInfos]
    ([Citizen_HID]);
GO

-- Creating foreign key on [Citizen_HID] in table 'DormitoryInfos'
ALTER TABLE [dbo].[DormitoryInfos]
ADD CONSTRAINT [FK_CitizenDormitoryInfo]
    FOREIGN KEY ([Citizen_HID])
    REFERENCES [dbo].[Humans_Citizen]
        ([HID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CitizenDormitoryInfo'
CREATE INDEX [IX_FK_CitizenDormitoryInfo]
ON [dbo].[DormitoryInfos]
    ([Citizen_HID]);
GO

-- Creating foreign key on [DormitoryDID] in table 'DormitoryInfos'
ALTER TABLE [dbo].[DormitoryInfos]
ADD CONSTRAINT [FK_DormitoryInfoDormitory]
    FOREIGN KEY ([DormitoryDID])
    REFERENCES [dbo].[Dormitories]
        ([DID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DormitoryInfoDormitory'
CREATE INDEX [IX_FK_DormitoryInfoDormitory]
ON [dbo].[DormitoryInfos]
    ([DormitoryDID]);
GO

-- Creating foreign key on [RoomRID] in table 'DormitoryInfos'
ALTER TABLE [dbo].[DormitoryInfos]
ADD CONSTRAINT [FK_RoomDormitoryInfo]
    FOREIGN KEY ([RoomRID])
    REFERENCES [dbo].[Rooms]
        ([RID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomDormitoryInfo'
CREATE INDEX [IX_FK_RoomDormitoryInfo]
ON [dbo].[DormitoryInfos]
    ([RoomRID]);
GO

-- Creating foreign key on [RoomRID] in table 'EmployeeLogs'
ALTER TABLE [dbo].[EmployeeLogs]
ADD CONSTRAINT [FK_RoomEmployeeLog]
    FOREIGN KEY ([RoomRID])
    REFERENCES [dbo].[Rooms]
        ([RID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomEmployeeLog'
CREATE INDEX [IX_FK_RoomEmployeeLog]
ON [dbo].[EmployeeLogs]
    ([RoomRID]);
GO

-- Creating foreign key on [Account_AID] in table 'Humans'
ALTER TABLE [dbo].[Humans]
ADD CONSTRAINT [FK_AccountHuman]
    FOREIGN KEY ([Account_AID])
    REFERENCES [dbo].[Accounts]
        ([AID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountHuman'
CREATE INDEX [IX_FK_AccountHuman]
ON [dbo].[Humans]
    ([Account_AID]);
GO

-- Creating foreign key on [Employee_HID] in table 'DormitoryInfos'
ALTER TABLE [dbo].[DormitoryInfos]
ADD CONSTRAINT [FK_EmployeeDormitoryInfo]
    FOREIGN KEY ([Employee_HID])
    REFERENCES [dbo].[Humans_Employee]
        ([HID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDormitoryInfo'
CREATE INDEX [IX_FK_EmployeeDormitoryInfo]
ON [dbo].[DormitoryInfos]
    ([Employee_HID]);
GO

-- Creating foreign key on [EmployeeHID] in table 'EmployeeLogs'
ALTER TABLE [dbo].[EmployeeLogs]
ADD CONSTRAINT [FK_EmployeeEmployeeLog]
    FOREIGN KEY ([EmployeeHID])
    REFERENCES [dbo].[Humans_Employee]
        ([HID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeEmployeeLog'
CREATE INDEX [IX_FK_EmployeeEmployeeLog]
ON [dbo].[EmployeeLogs]
    ([EmployeeHID]);
GO

-- Creating foreign key on [DormitoryDID] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [FK_DormitoryRoom]
    FOREIGN KEY ([DormitoryDID])
    REFERENCES [dbo].[Dormitories]
        ([DID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DormitoryRoom'
CREATE INDEX [IX_FK_DormitoryRoom]
ON [dbo].[Rooms]
    ([DormitoryDID]);
GO

-- Creating foreign key on [HID] in table 'Humans_Citizen'
ALTER TABLE [dbo].[Humans_Citizen]
ADD CONSTRAINT [FK_Citizen_inherits_Human]
    FOREIGN KEY ([HID])
    REFERENCES [dbo].[Humans]
        ([HID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [HID] in table 'Humans_Employee'
ALTER TABLE [dbo].[Humans_Employee]
ADD CONSTRAINT [FK_Employee_inherits_Human]
    FOREIGN KEY ([HID])
    REFERENCES [dbo].[Humans]
        ([HID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------