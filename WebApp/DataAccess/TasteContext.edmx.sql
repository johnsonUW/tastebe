
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/16/2018 22:20:09
-- Generated from EDMX file: C:\Users\jason\Documents\repo\tastebe\WebApp\DataAccess\TasteContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [taste];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Cuisine]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cuisine];
GO
IF OBJECT_ID(N'[dbo].[Dishes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dishes];
GO
IF OBJECT_ID(N'[dbo].[OrderedDishes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderedDishes];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Payments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payments];
GO
IF OBJECT_ID(N'[dbo].[Restaurants]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Restaurants];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Admins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admins];
GO
IF OBJECT_ID(N'[dbo].[Preferences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Preferences];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Cuisine'
CREATE TABLE [dbo].[Cuisine] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Taste] nvarchar(max)  NULL
);
GO

-- Creating table 'Dishes'
CREATE TABLE [dbo].[Dishes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RestaurantId] int  NOT NULL,
    [CuisineId] int  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Flavors] nvarchar(max)  NULL,
    [Ingredients] nvarchar(max)  NULL,
    [Category] nvarchar(max)  NULL,
    [Price] float  NOT NULL,
    [Image] nvarchar(max)  NULL,
    [Deleted] bit  NOT NULL,
    [CloverId] nvarchar(500)  NULL
);
GO

-- Creating table 'OrderedDishes'
CREATE TABLE [dbo].[OrderedDishes] (
    [DishId] int  NOT NULL,
    [Quantity] int  NOT NULL,
    [UserId] nvarchar(100)  NOT NULL,
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [OrderId] nvarchar(max)  NOT NULL,
    [CuisineId] int  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderId] nvarchar(max)  NOT NULL,
    [UserId] nvarchar(100)  NOT NULL,
    [Datetime] datetime  NOT NULL,
    [RestaurantId] int  NOT NULL,
    [RestaurantName] nvarchar(max)  NULL,
    [Details] nvarchar(max)  NOT NULL,
    [Paid] bit  NOT NULL,
    [TableName] nvarchar(max)  NOT NULL,
    [TipInPennies] int  NOT NULL,
    [TotalInPennies] int  NOT NULL,
    [TaxInPennies] int  NOT NULL
);
GO

-- Creating table 'Payments'
CREATE TABLE [dbo].[Payments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Success] bit  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [UserId] nvarchar(500)  NOT NULL,
    [TransactionId] nvarchar(500)  NOT NULL,
    [OrderId] nvarchar(500)  NOT NULL
);
GO

-- Creating table 'Restaurants'
CREATE TABLE [dbo].[Restaurants] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CloverId] nvarchar(max)  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Location] nvarchar(max)  NULL,
    [Phone] nvarchar(20)  NULL,
    [Owner] nvarchar(40)  NULL,
    [Image] nvarchar(max)  NULL,
    [AccessToken] nvarchar(500)  NULL,
    [IsSandbox] bit  NOT NULL,
    [ExchangeRate] float  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] nvarchar(100)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Admins'
CREATE TABLE [dbo].[Admins] (
    [Password] nvarchar(100)  NOT NULL,
    [Username] nvarchar(100)  NOT NULL,
    [RestaurantName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Preferences'
CREATE TABLE [dbo].[Preferences] (
    [UserId] nvarchar(100)  NOT NULL,
    [Ingredients] nvarchar(max)  NULL,
    [CuisineId] int  NULL,
    [Dishes] nvarchar(max)  NULL,
    [Flavors] nvarchar(max)  NULL,
    [Count] int  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Cuisine'
ALTER TABLE [dbo].[Cuisine]
ADD CONSTRAINT [PK_Cuisine]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Dishes'
ALTER TABLE [dbo].[Dishes]
ADD CONSTRAINT [PK_Dishes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderedDishes'
ALTER TABLE [dbo].[OrderedDishes]
ADD CONSTRAINT [PK_OrderedDishes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [PK_Payments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Restaurants'
ALTER TABLE [dbo].[Restaurants]
ADD CONSTRAINT [PK_Restaurants]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [Username] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [PK_Admins]
    PRIMARY KEY CLUSTERED ([Username] ASC);
GO

-- Creating primary key on [Id] in table 'Preferences'
ALTER TABLE [dbo].[Preferences]
ADD CONSTRAINT [PK_Preferences]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------