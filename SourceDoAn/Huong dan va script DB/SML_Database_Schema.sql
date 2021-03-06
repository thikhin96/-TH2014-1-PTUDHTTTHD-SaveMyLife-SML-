USE [master]
GO
/****** Object:  Database [QL_NPP]    Script Date: 2018-01-10 9:55:37 PM ******/
CREATE DATABASE [QL_NPP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QL_NPP', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\QL_NPP.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QL_NPP_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\QL_NPP_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QL_NPP] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QL_NPP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QL_NPP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QL_NPP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QL_NPP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QL_NPP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QL_NPP] SET ARITHABORT OFF 
GO
ALTER DATABASE [QL_NPP] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QL_NPP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QL_NPP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QL_NPP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QL_NPP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QL_NPP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QL_NPP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QL_NPP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QL_NPP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QL_NPP] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QL_NPP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QL_NPP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QL_NPP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QL_NPP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QL_NPP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QL_NPP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QL_NPP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QL_NPP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QL_NPP] SET  MULTI_USER 
GO
ALTER DATABASE [QL_NPP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QL_NPP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QL_NPP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QL_NPP] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QL_NPP] SET DELAYED_DURABILITY = DISABLED 
GO
USE [QL_NPP]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[idUser] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](30) NULL,
	[Password] [varchar](30) NULL,
	[activated] [bit] NULL,
	[decentralization] [tinyint] NULL,
	[locked] [bit] NULL,
	[dateCreate] [datetime] NULL,
	[dateUpdate] [datetime] NULL,
	[note] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Assignment]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignment](
	[staff] [int] NOT NULL,
	[PDistributor] [int] NOT NULL,
	[date] [datetime] NULL,
	[place] [nvarchar](100) NULL,
	[isComplete] [bit] NULL,
	[result] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[staff] ASC,
	[PDistributor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Batch]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Batch](
	[IdBatch] [int] IDENTITY(1,1) NOT NULL,
	[ManufacturedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBatch] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BatchDetail]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchDetail](
	[IdBatch] [int] NOT NULL,
	[IdProduct] [int] NOT NULL,
	[Quantity] [int] NULL,
	[ExipreDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBatch] ASC,
	[IdProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bill]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[idBill] [int] IDENTITY(1,1) NOT NULL,
	[purchase] [money] NULL,
	[createdDate] [datetime] NULL,
	[types] [tinyint] NULL,
	[description] [nvarchar](200) NULL,
	[idDeliveryOrder] [int] NULL,
	[idStaff] [int] NULL,
	[idDistributor] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idBill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Consignee]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Consignee](
	[idConsignee] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[idDistributor] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idConsignee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contract]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contract](
	[idContract] [int] IDENTITY(1,1) NOT NULL,
	[beginDate] [datetime] NULL,
	[expiredDate] [datetime] NULL,
	[minOrderTotalValue] [money] NULL,
	[maxDebt] [money] NULL,
	[commission] [tinyint] NULL,
	[disType] [bit] NULL,
	[area] [nvarchar](30) NULL,
	[status] [bit] NULL,
	[note] [nvarchar](max) NULL,
	[distributor] [int] NULL,
	[representative] [int] NULL,
	[staff] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idContract] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Debt]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Debt](
	[idDebt] [int] IDENTITY(1,1) NOT NULL,
	[Purchase] [money] NULL,
	[CreatedDate] [datetime] NULL,
	[idDistributor] [int] NULL,
	[idStaff] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idDebt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DeliveryOrder]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryOrder](
	[idDeliveryOrder] [int] IDENTITY(1,1) NOT NULL,
	[recipient] [nvarchar](50) NULL,
	[deliveryAdd] [nvarchar](100) NULL,
	[totalPurchase] [money] NULL,
	[deliveryDate] [datetime] NULL,
	[status] [tinyint] NULL,
	[updateDate] [datetime] NULL,
	[description] [nvarchar](200) NULL,
	[idOrder] [int] NULL,
	[idStaff] [int] NULL,
	[idDistributor] [int] NULL,
	[recipientPhone] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDeliveryOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DetailedDeliveryOrder]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailedDeliveryOrder](
	[idDeliveryOrder] [int] NOT NULL,
	[idProduct] [int] NOT NULL,
	[quantity] [int] NULL,
	[note] [nvarchar](200) NULL,
	[promoQuantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idDeliveryOrder] ASC,
	[idProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Distributor]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Distributor](
	[idDistributor] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[address] [nvarchar](100) NULL,
	[phone] [varchar](11) NULL,
	[Email] [varchar](100) NULL,
	[createdDate] [datetime] NULL,
	[updatedDate] [datetime] NULL,
	[note] [nvarchar](max) NULL,
	[status] [bit] NULL,
	[UserName] [varchar](30) NULL,
	[debt] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[idDistributor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Log_Login]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log_Login](
	[idLog] [bigint] IDENTITY(1,1) NOT NULL,
	[idAccount] [int] NULL,
	[at_time] [datetime] NULL,
	[status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Log_Order]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log_Order](
	[idLog] [int] IDENTITY(1,1) NOT NULL,
	[createdDate] [datetime] NULL,
	[oldPrice] [int] NULL,
	[newPrice] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Log_OrderDetail]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log_OrderDetail](
	[idLog] [int] IDENTITY(1,1) NOT NULL,
	[createdDate] [datetime] NULL,
	[oldQuantity] [int] NULL,
	[newQuantity] [int] NULL,
	[description] [nvarchar](100) NULL,
	[idOrder] [int] NULL,
	[idProduct] [int] NULL,
	[idStaff] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Log_Product]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log_Product](
	[idLog_Product] [int] IDENTITY(1,1) NOT NULL,
	[createdDate] [datetime] NULL,
	[idStaff] [int] NULL,
	[oldPrice] [int] NULL,
	[newPrice] [int] NULL,
	[description] [nvarchar](max) NULL,
	[idProduct] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idLog_Product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Order]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[idOrder] [int] IDENTITY(1,1) NOT NULL,
	[Total] [money] NULL,
	[DeliveryType] [bit] NULL,
	[PaymentType] [bit] NULL,
	[EstimateDateOfDelivery] [datetime] NULL,
	[Statuses] [tinyint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[idDistributor] [int] NULL,
	[idConsignee] [int] NULL,
	[idStaff] [int] NULL,
	[Descriptions] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[idOrder] [int] NOT NULL,
	[idProduct] [int] NOT NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idOrder] ASC,
	[idProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaySlip]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaySlip](
	[idPaySlip] [int] IDENTITY(1,1) NOT NULL,
	[AmountSpent] [money] NULL,
	[SpendingReasons] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[idDistributor] [int] NULL,
	[idStaff] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPaySlip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PotentialDistributor]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PotentialDistributor](
	[idDistributor] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[address] [nvarchar](100) NULL,
	[phone] [varchar](11) NULL,
	[Email] [varchar](100) NULL,
	[createdDate] [datetime] NULL,
	[updatedDate] [datetime] NULL,
	[note] [nvarchar](max) NULL,
	[status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[idDistributor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[IdProduct] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NULL,
	[Price] [money] NULL,
	[IsDisabled] [bit] NULL,
	[ProductType] [int] NULL,
	[Unit] [int] NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[idProductType] [int] IDENTITY(1,1) NOT NULL,
	[productTypeName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idProductType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[idPromotion] [int] IDENTITY(1,1) NOT NULL,
	[startDate] [datetime] NULL,
	[endDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPromotion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PromotionGifts]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PromotionGifts](
	[idPromotion] [int] NOT NULL,
	[idProduct] [int] NOT NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPromotion] ASC,
	[idProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PromotionProduct]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PromotionProduct](
	[idPromotion] [int] NOT NULL,
	[idProduct] [int] NOT NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPromotion] ASC,
	[idProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Representative]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Representative](
	[idRepresentative] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](30) NULL,
	[phone] [varchar](11) NULL,
	[email] [varchar](100) NULL,
	[title] [nvarchar](50) NULL,
	[PDistributor] [int] NULL,
	[Distributor] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idRepresentative] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReturnBase]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReturnBase](
	[idReturn] [int] IDENTITY(1,1) NOT NULL,
	[Total] [money] NULL,
	[DateCreate] [datetime] NULL,
	[ModeOfReturn] [bit] NULL,
	[idDistributor] [int] NULL,
	[idStorage] [int] NULL,
	[idStaff] [int] NULL,
	[idReturnRequest] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idReturn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReturnDetail]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReturnDetail](
	[idReturn] [int] NOT NULL,
	[idProduct] [int] NOT NULL,
	[Quantity] [int] NULL,
	[ProductMoneyRefunding] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[idReturn] ASC,
	[idProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReturnRequest]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReturnRequest](
	[IdReturnRequest] [int] IDENTITY(1,1) NOT NULL,
	[DateCreate] [datetime] NULL,
	[Status] [int] NULL,
	[Note] [nvarchar](max) NULL,
	[ModeOfReturn] [bit] NULL,
	[Distributor] [int] NULL,
	[Storage] [int] NULL,
	[Staff] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdReturnRequest] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReturnRequestDetail]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReturnRequestDetail](
	[IdReturnRequest] [int] NOT NULL,
	[IdProduct] [int] NOT NULL,
	[Quantity] [int] NULL,
	[Reason] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdReturnRequest] ASC,
	[IdProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesReport]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesReport](
	[idSalesReport] [int] IDENTITY(1,1) NOT NULL,
	[beginDate] [datetime] NULL,
	[endDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idSalesReport] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesReportDetail]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesReportDetail](
	[idSalesReport] [int] NOT NULL,
	[idProductType] [int] NOT NULL,
	[price] [int] NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idSalesReport] ASC,
	[idProductType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Staff]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Staff](
	[idStaff] [int] IDENTITY(1,1) NOT NULL,
	[staffName] [nvarchar](30) NULL,
	[account] [varchar](30) NULL,
	[idCard] [varchar](12) NULL,
	[phone] [varchar](11) NULL,
	[adress] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idStaff] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[idCard] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Storage]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Storage](
	[IdStorage] [int] IDENTITY(1,1) NOT NULL,
	[HouseNumber_Street] [nvarchar](50) NULL,
	[Ward_Commune] [nvarchar](50) NULL,
	[District] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Description] [nvarchar](100) NULL,
	[Distributor] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdStorage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Unit]    Script Date: 2018-01-10 9:55:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[idUnit] [int] IDENTITY(1,1) NOT NULL,
	[unitName] [nvarchar](30) NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idUnit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[unitName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_Assignment_PDistributor] FOREIGN KEY([PDistributor])
REFERENCES [dbo].[PotentialDistributor] ([idDistributor])
GO
ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_Assignment_PDistributor]
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_Assignment_Staff] FOREIGN KEY([staff])
REFERENCES [dbo].[Staff] ([idStaff])
GO
ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_Assignment_Staff]
GO
ALTER TABLE [dbo].[BatchDetail]  WITH CHECK ADD  CONSTRAINT [FK_BatchDetail_Batch] FOREIGN KEY([IdBatch])
REFERENCES [dbo].[Batch] ([IdBatch])
GO
ALTER TABLE [dbo].[BatchDetail] CHECK CONSTRAINT [FK_BatchDetail_Batch]
GO
ALTER TABLE [dbo].[BatchDetail]  WITH CHECK ADD  CONSTRAINT [FK_BatchDetail_Product] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[BatchDetail] CHECK CONSTRAINT [FK_BatchDetail_Product]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_DeliveryOrder] FOREIGN KEY([idDeliveryOrder])
REFERENCES [dbo].[DeliveryOrder] ([idDeliveryOrder])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_DeliveryOrder]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Distributor] FOREIGN KEY([idDistributor])
REFERENCES [dbo].[Distributor] ([idDistributor])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_Distributor]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Staff] FOREIGN KEY([idStaff])
REFERENCES [dbo].[Staff] ([idStaff])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_Staff]
GO
ALTER TABLE [dbo].[Consignee]  WITH CHECK ADD  CONSTRAINT [FK_Consignee_Distributor] FOREIGN KEY([idDistributor])
REFERENCES [dbo].[Distributor] ([idDistributor])
GO
ALTER TABLE [dbo].[Consignee] CHECK CONSTRAINT [FK_Consignee_Distributor]
GO
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_HopDong_NDD] FOREIGN KEY([representative])
REFERENCES [dbo].[Representative] ([idRepresentative])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_HopDong_NDD]
GO
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_HopDong_NhanVien] FOREIGN KEY([staff])
REFERENCES [dbo].[Staff] ([idStaff])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_HopDong_NhanVien]
GO
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_HopDong_NhaPhanPhoi] FOREIGN KEY([distributor])
REFERENCES [dbo].[Distributor] ([idDistributor])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_HopDong_NhaPhanPhoi]
GO
ALTER TABLE [dbo].[Debt]  WITH CHECK ADD  CONSTRAINT [FK_Debt_Distributor] FOREIGN KEY([idDistributor])
REFERENCES [dbo].[Distributor] ([idDistributor])
GO
ALTER TABLE [dbo].[Debt] CHECK CONSTRAINT [FK_Debt_Distributor]
GO
ALTER TABLE [dbo].[Debt]  WITH CHECK ADD  CONSTRAINT [FK_Debt_Staff] FOREIGN KEY([idStaff])
REFERENCES [dbo].[Staff] ([idStaff])
GO
ALTER TABLE [dbo].[Debt] CHECK CONSTRAINT [FK_Debt_Staff]
GO
ALTER TABLE [dbo].[DeliveryOrder]  WITH CHECK ADD  CONSTRAINT [FK_DeliveryOrder_Distributor] FOREIGN KEY([idDistributor])
REFERENCES [dbo].[Distributor] ([idDistributor])
GO
ALTER TABLE [dbo].[DeliveryOrder] CHECK CONSTRAINT [FK_DeliveryOrder_Distributor]
GO
ALTER TABLE [dbo].[DeliveryOrder]  WITH CHECK ADD  CONSTRAINT [FK_DeliveryOrder_Order] FOREIGN KEY([idOrder])
REFERENCES [dbo].[Order] ([idOrder])
GO
ALTER TABLE [dbo].[DeliveryOrder] CHECK CONSTRAINT [FK_DeliveryOrder_Order]
GO
ALTER TABLE [dbo].[DeliveryOrder]  WITH CHECK ADD  CONSTRAINT [FK_DeliveryOrder_Staff] FOREIGN KEY([idStaff])
REFERENCES [dbo].[Staff] ([idStaff])
GO
ALTER TABLE [dbo].[DeliveryOrder] CHECK CONSTRAINT [FK_DeliveryOrder_Staff]
GO
ALTER TABLE [dbo].[DetailedDeliveryOrder]  WITH CHECK ADD  CONSTRAINT [FK_DetailedDeliveryOrder_DeliveryOrder] FOREIGN KEY([idDeliveryOrder])
REFERENCES [dbo].[DeliveryOrder] ([idDeliveryOrder])
GO
ALTER TABLE [dbo].[DetailedDeliveryOrder] CHECK CONSTRAINT [FK_DetailedDeliveryOrder_DeliveryOrder]
GO
ALTER TABLE [dbo].[DetailedDeliveryOrder]  WITH CHECK ADD  CONSTRAINT [FK_DetailedDeliveryOrder_Product] FOREIGN KEY([idProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[DetailedDeliveryOrder] CHECK CONSTRAINT [FK_DetailedDeliveryOrder_Product]
GO
ALTER TABLE [dbo].[Distributor]  WITH CHECK ADD  CONSTRAINT [FK_NPP_Account] FOREIGN KEY([UserName])
REFERENCES [dbo].[Account] ([UserName])
GO
ALTER TABLE [dbo].[Distributor] CHECK CONSTRAINT [FK_NPP_Account]
GO
ALTER TABLE [dbo].[Log_OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_Log_OrderDetail_Order] FOREIGN KEY([idOrder])
REFERENCES [dbo].[Order] ([idOrder])
GO
ALTER TABLE [dbo].[Log_OrderDetail] CHECK CONSTRAINT [FK_Log_OrderDetail_Order]
GO
ALTER TABLE [dbo].[Log_OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_Log_OrderDetail_Product] FOREIGN KEY([idProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[Log_OrderDetail] CHECK CONSTRAINT [FK_Log_OrderDetail_Product]
GO
ALTER TABLE [dbo].[Log_OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_Log_OrderDetail_Staff] FOREIGN KEY([idStaff])
REFERENCES [dbo].[Staff] ([idStaff])
GO
ALTER TABLE [dbo].[Log_OrderDetail] CHECK CONSTRAINT [FK_Log_OrderDetail_Staff]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Consignee] FOREIGN KEY([idConsignee])
REFERENCES [dbo].[Consignee] ([idConsignee])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Consignee]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Distributor] FOREIGN KEY([idDistributor])
REFERENCES [dbo].[Distributor] ([idDistributor])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Distributor]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([idOrder])
REFERENCES [dbo].[Order] ([idOrder])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([idProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[PaySlip]  WITH CHECK ADD  CONSTRAINT [FK_PaySlip_Distributor] FOREIGN KEY([idDistributor])
REFERENCES [dbo].[Distributor] ([idDistributor])
GO
ALTER TABLE [dbo].[PaySlip] CHECK CONSTRAINT [FK_PaySlip_Distributor]
GO
ALTER TABLE [dbo].[PaySlip]  WITH CHECK ADD  CONSTRAINT [FK_PaySlip_Staff] FOREIGN KEY([idStaff])
REFERENCES [dbo].[Staff] ([idStaff])
GO
ALTER TABLE [dbo].[PaySlip] CHECK CONSTRAINT [FK_PaySlip_Staff]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductType] FOREIGN KEY([ProductType])
REFERENCES [dbo].[ProductType] ([idProductType])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductType]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Unit] FOREIGN KEY([Unit])
REFERENCES [dbo].[Unit] ([idUnit])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Unit]
GO
ALTER TABLE [dbo].[PromotionGifts]  WITH CHECK ADD  CONSTRAINT [FK_PromotionGifts_Product] FOREIGN KEY([idProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[PromotionGifts] CHECK CONSTRAINT [FK_PromotionGifts_Product]
GO
ALTER TABLE [dbo].[PromotionGifts]  WITH CHECK ADD  CONSTRAINT [FK_PromotionGifts_Promotion] FOREIGN KEY([idPromotion])
REFERENCES [dbo].[Promotion] ([idPromotion])
GO
ALTER TABLE [dbo].[PromotionGifts] CHECK CONSTRAINT [FK_PromotionGifts_Promotion]
GO
ALTER TABLE [dbo].[PromotionProduct]  WITH CHECK ADD  CONSTRAINT [FK_PromotionProduct_Product] FOREIGN KEY([idProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[PromotionProduct] CHECK CONSTRAINT [FK_PromotionProduct_Product]
GO
ALTER TABLE [dbo].[PromotionProduct]  WITH CHECK ADD  CONSTRAINT [FK_PromotionProduct_Promotion] FOREIGN KEY([idPromotion])
REFERENCES [dbo].[Promotion] ([idPromotion])
GO
ALTER TABLE [dbo].[PromotionProduct] CHECK CONSTRAINT [FK_PromotionProduct_Promotion]
GO
ALTER TABLE [dbo].[Representative]  WITH CHECK ADD  CONSTRAINT [FK_Representative_Distributor] FOREIGN KEY([Distributor])
REFERENCES [dbo].[Distributor] ([idDistributor])
GO
ALTER TABLE [dbo].[Representative] CHECK CONSTRAINT [FK_Representative_Distributor]
GO
ALTER TABLE [dbo].[Representative]  WITH CHECK ADD  CONSTRAINT [FK_Representative_PDistributor] FOREIGN KEY([PDistributor])
REFERENCES [dbo].[PotentialDistributor] ([idDistributor])
GO
ALTER TABLE [dbo].[Representative] CHECK CONSTRAINT [FK_Representative_PDistributor]
GO
ALTER TABLE [dbo].[ReturnBase]  WITH CHECK ADD  CONSTRAINT [FK_ReturnBase_Distributor] FOREIGN KEY([idDistributor])
REFERENCES [dbo].[Distributor] ([idDistributor])
GO
ALTER TABLE [dbo].[ReturnBase] CHECK CONSTRAINT [FK_ReturnBase_Distributor]
GO
ALTER TABLE [dbo].[ReturnBase]  WITH CHECK ADD  CONSTRAINT [FK_ReturnBase_ReturnRequest] FOREIGN KEY([idReturnRequest])
REFERENCES [dbo].[ReturnRequest] ([IdReturnRequest])
GO
ALTER TABLE [dbo].[ReturnBase] CHECK CONSTRAINT [FK_ReturnBase_ReturnRequest]
GO
ALTER TABLE [dbo].[ReturnBase]  WITH CHECK ADD  CONSTRAINT [FK_ReturnBase_Staff] FOREIGN KEY([idStaff])
REFERENCES [dbo].[Staff] ([idStaff])
GO
ALTER TABLE [dbo].[ReturnBase] CHECK CONSTRAINT [FK_ReturnBase_Staff]
GO
ALTER TABLE [dbo].[ReturnBase]  WITH CHECK ADD  CONSTRAINT [FK_ReturnBase_Storage] FOREIGN KEY([idStorage])
REFERENCES [dbo].[Storage] ([IdStorage])
GO
ALTER TABLE [dbo].[ReturnBase] CHECK CONSTRAINT [FK_ReturnBase_Storage]
GO
ALTER TABLE [dbo].[ReturnDetail]  WITH CHECK ADD  CONSTRAINT [FK_ReturnDetail_Product] FOREIGN KEY([idProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[ReturnDetail] CHECK CONSTRAINT [FK_ReturnDetail_Product]
GO
ALTER TABLE [dbo].[ReturnDetail]  WITH CHECK ADD  CONSTRAINT [FK_ReturnDetail_ReturnBase] FOREIGN KEY([idReturn])
REFERENCES [dbo].[ReturnBase] ([idReturn])
GO
ALTER TABLE [dbo].[ReturnDetail] CHECK CONSTRAINT [FK_ReturnDetail_ReturnBase]
GO
ALTER TABLE [dbo].[ReturnRequest]  WITH CHECK ADD  CONSTRAINT [FK_ReturnRequest_Distributor] FOREIGN KEY([Distributor])
REFERENCES [dbo].[Distributor] ([idDistributor])
GO
ALTER TABLE [dbo].[ReturnRequest] CHECK CONSTRAINT [FK_ReturnRequest_Distributor]
GO
ALTER TABLE [dbo].[ReturnRequest]  WITH CHECK ADD  CONSTRAINT [FK_ReturnRequest_Staff] FOREIGN KEY([Staff])
REFERENCES [dbo].[Staff] ([idStaff])
GO
ALTER TABLE [dbo].[ReturnRequest] CHECK CONSTRAINT [FK_ReturnRequest_Staff]
GO
ALTER TABLE [dbo].[ReturnRequest]  WITH CHECK ADD  CONSTRAINT [FK_ReturnRequest_Storage] FOREIGN KEY([Storage])
REFERENCES [dbo].[Storage] ([IdStorage])
GO
ALTER TABLE [dbo].[ReturnRequest] CHECK CONSTRAINT [FK_ReturnRequest_Storage]
GO
ALTER TABLE [dbo].[ReturnRequestDetail]  WITH CHECK ADD  CONSTRAINT [FK_ReturnRequestDetail_Product] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[ReturnRequestDetail] CHECK CONSTRAINT [FK_ReturnRequestDetail_Product]
GO
ALTER TABLE [dbo].[ReturnRequestDetail]  WITH CHECK ADD  CONSTRAINT [FK_ReturnRequestDetail_ReturnRequest] FOREIGN KEY([IdReturnRequest])
REFERENCES [dbo].[ReturnRequest] ([IdReturnRequest])
GO
ALTER TABLE [dbo].[ReturnRequestDetail] CHECK CONSTRAINT [FK_ReturnRequestDetail_ReturnRequest]
GO
ALTER TABLE [dbo].[SalesReportDetail]  WITH CHECK ADD  CONSTRAINT [FK_SalesReportDetail_ProductType] FOREIGN KEY([idProductType])
REFERENCES [dbo].[ProductType] ([idProductType])
GO
ALTER TABLE [dbo].[SalesReportDetail] CHECK CONSTRAINT [FK_SalesReportDetail_ProductType]
GO
ALTER TABLE [dbo].[SalesReportDetail]  WITH CHECK ADD  CONSTRAINT [FK_SalesReportDetail_SalesReport] FOREIGN KEY([idSalesReport])
REFERENCES [dbo].[SalesReport] ([idSalesReport])
GO
ALTER TABLE [dbo].[SalesReportDetail] CHECK CONSTRAINT [FK_SalesReportDetail_SalesReport]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Account] FOREIGN KEY([account])
REFERENCES [dbo].[Account] ([UserName])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Account]
GO
ALTER TABLE [dbo].[Storage]  WITH CHECK ADD  CONSTRAINT [FK_Storage_Distributor] FOREIGN KEY([Distributor])
REFERENCES [dbo].[Distributor] ([idDistributor])
GO
ALTER TABLE [dbo].[Storage] CHECK CONSTRAINT [FK_Storage_Distributor]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD CHECK  (([decentralization]=(3) OR [decentralization]=(2) OR [decentralization]=(1)))
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD CHECK  ((len([Password])>=(8) AND len([Password])<=(30)))
GO
ALTER TABLE [dbo].[BatchDetail]  WITH CHECK ADD CHECK  (([Quantity]>(0)))
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD CHECK  (([types]=(2) OR [types]=(1)))
GO
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD CHECK  (([commission]>=(0) AND [commission]<=(100)))
GO
ALTER TABLE [dbo].[DeliveryOrder]  WITH CHECK ADD CHECK  (([status]=(5) OR [status]=(4) OR [status]=(3) OR [status]=(2) OR [status]=(1)))
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD CHECK  (([Statuses]=(3) OR [Statuses]=(2) OR [Statuses]=(1) OR [Statuses]=(0)))
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD CHECK  (([quantity]>(0)))
GO
ALTER TABLE [dbo].[PotentialDistributor]  WITH CHECK ADD CHECK  (([status]=(4) OR [status]=(3) OR [status]=(2) OR [status]=(1) OR [status]=(0)))
GO
ALTER TABLE [dbo].[PromotionGifts]  WITH CHECK ADD CHECK  (([quantity]>(0)))
GO
ALTER TABLE [dbo].[ReturnDetail]  WITH CHECK ADD CHECK  (([Quantity]>(0)))
GO
ALTER TABLE [dbo].[ReturnRequest]  WITH CHECK ADD CHECK  (([Status]=(2) OR [Status]=(1) OR [Status]=(0)))
GO
ALTER TABLE [dbo].[ReturnRequestDetail]  WITH CHECK ADD CHECK  (([Quantity]>(0)))
GO
ALTER TABLE [dbo].[Unit]  WITH CHECK ADD CHECK  (([quantity]>(0)))
GO
USE [master]
GO
ALTER DATABASE [QL_NPP] SET  READ_WRITE 
GO
