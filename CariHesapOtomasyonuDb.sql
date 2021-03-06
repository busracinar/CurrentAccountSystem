USE [master]
GO
/****** Object:  Database [CariHesapDb]    Script Date: 15.12.2019 15:50:00 ******/
CREATE DATABASE [CariHesapDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CariHesapDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\CariHesapDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CariHesapDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\CariHesapDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CariHesapDb] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CariHesapDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CariHesapDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CariHesapDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CariHesapDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CariHesapDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CariHesapDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [CariHesapDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CariHesapDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CariHesapDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CariHesapDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CariHesapDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CariHesapDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CariHesapDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CariHesapDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CariHesapDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CariHesapDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CariHesapDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CariHesapDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CariHesapDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CariHesapDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CariHesapDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CariHesapDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CariHesapDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CariHesapDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CariHesapDb] SET  MULTI_USER 
GO
ALTER DATABASE [CariHesapDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CariHesapDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CariHesapDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CariHesapDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CariHesapDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CariHesapDb] SET QUERY_STORE = OFF
GO
USE [CariHesapDb]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 15.12.2019 15:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 15.12.2019 15:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[UserId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 15.12.2019 15:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CategoryId] [int] NULL,
	[ArrivalPrice] [int] NOT NULL,
	[SalePrice] [int] NOT NULL,
	[InitialStock] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[AvailableStock] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sale]    Script Date: 15.12.2019 15:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sale](
	[SaleId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[Count] [int] NOT NULL,
	[CustemerId] [int] NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Sale] PRIMARY KEY CLUSTERED 
(
	[SaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 15.12.2019 15:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryId], [Name], [Description], [IsDeleted]) VALUES (1, N'Gıda', N'Yenebilir ürünler', 0)
INSERT [dbo].[Category] ([CategoryId], [Name], [Description], [IsDeleted]) VALUES (2, N'Mobilya', N'Koltuk, masa vs', 0)
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerId], [Name], [Surname], [Phone], [Address], [UserId], [IsDeleted]) VALUES (2, N'büşra', N'çınar', N'5357217675', N'küçükçekmece', 1, 0)
INSERT [dbo].[Customer] ([CustomerId], [Name], [Surname], [Phone], [Address], [UserId], [IsDeleted]) VALUES (4, N'berat', N'ber', N'6632565', N'bçekmece', 1, 0)
INSERT [dbo].[Customer] ([CustomerId], [Name], [Surname], [Phone], [Address], [UserId], [IsDeleted]) VALUES (5, N'büşir', N'hel', N'54256464', N'askjdaı', 1, 0)
INSERT [dbo].[Customer] ([CustomerId], [Name], [Surname], [Phone], [Address], [UserId], [IsDeleted]) VALUES (6, N'selin', N'sel', N'5256', N'sdfsdf', 1, 0)
SET IDENTITY_INSERT [dbo].[Customer] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductId], [Name], [CategoryId], [ArrivalPrice], [SalePrice], [InitialStock], [Description], [IsDeleted], [AvailableStock]) VALUES (1, N'Pirinç', 1, 30, 40, 100, N'Besleyici', 0, 0)
INSERT [dbo].[Product] ([ProductId], [Name], [CategoryId], [ArrivalPrice], [SalePrice], [InitialStock], [Description], [IsDeleted], [AvailableStock]) VALUES (2, N'Sandalye', 2, 60, 100, 100, N'Oturacak', 0, 100)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[Sale] ON 

INSERT [dbo].[Sale] ([SaleId], [ProductId], [Count], [CustemerId], [Date]) VALUES (15, 1, 30, 2, CAST(N'2019-12-15T11:57:52.833' AS DateTime))
INSERT [dbo].[Sale] ([SaleId], [ProductId], [Count], [CustemerId], [Date]) VALUES (16, 1, 20, 6, CAST(N'2019-12-15T14:15:24.990' AS DateTime))
INSERT [dbo].[Sale] ([SaleId], [ProductId], [Count], [CustemerId], [Date]) VALUES (17, 1, 50, 5, CAST(N'2019-12-15T14:59:49.577' AS DateTime))
SET IDENTITY_INSERT [dbo].[Sale] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserName], [Password]) VALUES (1, N'b', N'c')
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_User]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD  CONSTRAINT [FK_Sale_Customer] FOREIGN KEY([CustemerId])
REFERENCES [dbo].[Customer] ([CustomerId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Sale] CHECK CONSTRAINT [FK_Sale_Customer]
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD  CONSTRAINT [FK_Sale_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Sale] CHECK CONSTRAINT [FK_Sale_Product]
GO
USE [master]
GO
ALTER DATABASE [CariHesapDb] SET  READ_WRITE 
GO
