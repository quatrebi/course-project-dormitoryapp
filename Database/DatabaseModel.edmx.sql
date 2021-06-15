
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/28/2021 08:44:16
-- Generated from EDMX file: D:\Documents\Visual Studio\DormitoryApp\Database\DatabaseModel.edmx
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

IF OBJECT_ID(N'[dbo].[FK_UserModelEmployeeModel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeModels] DROP CONSTRAINT [FK_UserModelEmployeeModel];
GO
IF OBJECT_ID(N'[dbo].[FK_UserModelResidentModel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResidentModels] DROP CONSTRAINT [FK_UserModelResidentModel];
GO
IF OBJECT_ID(N'[dbo].[FK_DormitoryModelRoomModel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoomModels] DROP CONSTRAINT [FK_DormitoryModelRoomModel];
GO
IF OBJECT_ID(N'[dbo].[FK_DormitoryModelEmployeeModel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeModels] DROP CONSTRAINT [FK_DormitoryModelEmployeeModel];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeModelEmployeeLogModel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeLogModels] DROP CONSTRAINT [FK_EmployeeModelEmployeeLogModel];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomModelResidentModel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResidentModels] DROP CONSTRAINT [FK_RoomModelResidentModel];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomModelEmployeeLogModel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeLogModels] DROP CONSTRAINT [FK_RoomModelEmployeeLogModel];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserModels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserModels];
GO
IF OBJECT_ID(N'[dbo].[DormitoryModels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DormitoryModels];
GO
IF OBJECT_ID(N'[dbo].[EmployeeModels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeModels];
GO
IF OBJECT_ID(N'[dbo].[EmployeeLogModels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeLogModels];
GO
IF OBJECT_ID(N'[dbo].[ResidentModels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ResidentModels];
GO
IF OBJECT_ID(N'[dbo].[RoomModels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoomModels];
GO
IF OBJECT_ID(N'[dbo].[MenuButtonModels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MenuButtonModels];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserModels'
CREATE TABLE [dbo].[UserModels] (
    [UID] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Permission] smallint  NOT NULL,
    [Surname] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NULL,
    [Patronymic] nvarchar(max)  NULL
);
GO

-- Creating table 'DormitoryModels'
CREATE TABLE [dbo].[DormitoryModels] (
    [DID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [NumberOfFloors] int  NOT NULL,
    [RoomsPerFloor] int  NOT NULL,
    [CitizensPerRoom] int  NOT NULL
);
GO

-- Creating table 'EmployeeModels'
CREATE TABLE [dbo].[EmployeeModels] (
    [EID] int IDENTITY(1,1) NOT NULL,
    [Position] nvarchar(max)  NULL,
    [DormitoryModelDID] int  NOT NULL,
    [UserModel_UID] int  NOT NULL
);
GO

-- Creating table 'EmployeeLogModels'
CREATE TABLE [dbo].[EmployeeLogModels] (
    [ELID] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Datetime] datetime  NOT NULL,
    [EmployeeModelUID] int  NOT NULL,
    [RoomModelRID] int  NOT NULL
);
GO

-- Creating table 'ResidentModels'
CREATE TABLE [dbo].[ResidentModels] (
    [RID] int IDENTITY(1,1) NOT NULL,
    [OrderNumber] int  NOT NULL,
    [CheckInDate] datetime  NOT NULL,
    [CheckOutDate] datetime  NOT NULL,
    [University] nvarchar(max)  NULL,
    [Faculty] nvarchar(max)  NULL,
    [Specialty] nvarchar(max)  NULL,
    [GroupNumber] int  NOT NULL,
    [RoomModelRID] int  NOT NULL,
    [UserModel_UID] int  NOT NULL
);
GO

-- Creating table 'RoomModels'
CREATE TABLE [dbo].[RoomModels] (
    [RID] int IDENTITY(1,1) NOT NULL,
    [Number] int  NOT NULL,
    [Floor] int  NOT NULL,
    [DormitoryModelDID] int  NOT NULL,
    [HeatSupply] float  NOT NULL,
    [Electricity] float  NOT NULL
);
GO

-- Creating table 'MenuButtonModels'
CREATE TABLE [dbo].[MenuButtonModels] (
    [MBID] int  NOT NULL,
    [Caption] nvarchar(max)  NULL,
    [ImageSource] nvarchar(max)  NULL,
    [Permission] smallint  NOT NULL,
    [ViewName] nvarchar(max)  NOT NULL,
    [ModelName] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UID] in table 'UserModels'
ALTER TABLE [dbo].[UserModels]
ADD CONSTRAINT [PK_UserModels]
    PRIMARY KEY CLUSTERED ([UID] ASC);
GO

-- Creating primary key on [DID] in table 'DormitoryModels'
ALTER TABLE [dbo].[DormitoryModels]
ADD CONSTRAINT [PK_DormitoryModels]
    PRIMARY KEY CLUSTERED ([DID] ASC);
GO

-- Creating primary key on [EID] in table 'EmployeeModels'
ALTER TABLE [dbo].[EmployeeModels]
ADD CONSTRAINT [PK_EmployeeModels]
    PRIMARY KEY CLUSTERED ([EID] ASC);
GO

-- Creating primary key on [ELID] in table 'EmployeeLogModels'
ALTER TABLE [dbo].[EmployeeLogModels]
ADD CONSTRAINT [PK_EmployeeLogModels]
    PRIMARY KEY CLUSTERED ([ELID] ASC);
GO

-- Creating primary key on [RID] in table 'ResidentModels'
ALTER TABLE [dbo].[ResidentModels]
ADD CONSTRAINT [PK_ResidentModels]
    PRIMARY KEY CLUSTERED ([RID] ASC);
GO

-- Creating primary key on [RID] in table 'RoomModels'
ALTER TABLE [dbo].[RoomModels]
ADD CONSTRAINT [PK_RoomModels]
    PRIMARY KEY CLUSTERED ([RID] ASC);
GO

-- Creating primary key on [MBID] in table 'MenuButtonModels'
ALTER TABLE [dbo].[MenuButtonModels]
ADD CONSTRAINT [PK_MenuButtonModels]
    PRIMARY KEY CLUSTERED ([MBID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserModel_UID] in table 'EmployeeModels'
ALTER TABLE [dbo].[EmployeeModels]
ADD CONSTRAINT [FK_UserModelEmployeeModel]
    FOREIGN KEY ([UserModel_UID])
    REFERENCES [dbo].[UserModels]
        ([UID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserModelEmployeeModel'
CREATE INDEX [IX_FK_UserModelEmployeeModel]
ON [dbo].[EmployeeModels]
    ([UserModel_UID]);
GO

-- Creating foreign key on [UserModel_UID] in table 'ResidentModels'
ALTER TABLE [dbo].[ResidentModels]
ADD CONSTRAINT [FK_UserModelResidentModel]
    FOREIGN KEY ([UserModel_UID])
    REFERENCES [dbo].[UserModels]
        ([UID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserModelResidentModel'
CREATE INDEX [IX_FK_UserModelResidentModel]
ON [dbo].[ResidentModels]
    ([UserModel_UID]);
GO

-- Creating foreign key on [DormitoryModelDID] in table 'RoomModels'
ALTER TABLE [dbo].[RoomModels]
ADD CONSTRAINT [FK_DormitoryModelRoomModel]
    FOREIGN KEY ([DormitoryModelDID])
    REFERENCES [dbo].[DormitoryModels]
        ([DID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DormitoryModelRoomModel'
CREATE INDEX [IX_FK_DormitoryModelRoomModel]
ON [dbo].[RoomModels]
    ([DormitoryModelDID]);
GO

-- Creating foreign key on [DormitoryModelDID] in table 'EmployeeModels'
ALTER TABLE [dbo].[EmployeeModels]
ADD CONSTRAINT [FK_DormitoryModelEmployeeModel]
    FOREIGN KEY ([DormitoryModelDID])
    REFERENCES [dbo].[DormitoryModels]
        ([DID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DormitoryModelEmployeeModel'
CREATE INDEX [IX_FK_DormitoryModelEmployeeModel]
ON [dbo].[EmployeeModels]
    ([DormitoryModelDID]);
GO

-- Creating foreign key on [EmployeeModelUID] in table 'EmployeeLogModels'
ALTER TABLE [dbo].[EmployeeLogModels]
ADD CONSTRAINT [FK_EmployeeModelEmployeeLogModel]
    FOREIGN KEY ([EmployeeModelUID])
    REFERENCES [dbo].[EmployeeModels]
        ([EID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeModelEmployeeLogModel'
CREATE INDEX [IX_FK_EmployeeModelEmployeeLogModel]
ON [dbo].[EmployeeLogModels]
    ([EmployeeModelUID]);
GO

-- Creating foreign key on [RoomModelRID] in table 'ResidentModels'
ALTER TABLE [dbo].[ResidentModels]
ADD CONSTRAINT [FK_RoomModelResidentModel]
    FOREIGN KEY ([RoomModelRID])
    REFERENCES [dbo].[RoomModels]
        ([RID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomModelResidentModel'
CREATE INDEX [IX_FK_RoomModelResidentModel]
ON [dbo].[ResidentModels]
    ([RoomModelRID]);
GO

-- Creating foreign key on [RoomModelRID] in table 'EmployeeLogModels'
ALTER TABLE [dbo].[EmployeeLogModels]
ADD CONSTRAINT [FK_RoomModelEmployeeLogModel]
    FOREIGN KEY ([RoomModelRID])
    REFERENCES [dbo].[RoomModels]
        ([RID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomModelEmployeeLogModel'
CREATE INDEX [IX_FK_RoomModelEmployeeLogModel]
ON [dbo].[EmployeeLogModels]
    ([RoomModelRID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------