USE [master]
GO
/****** Object:  Database [OldGoodsMarketplace]    Script Date: 3/6/2025 3:03:46 PM ******/
CREATE DATABASE [OldGoodsMarketplace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OldGoodsMarketplace', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\OldGoodsMarketplace.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OldGoodsMarketplace_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\OldGoodsMarketplace_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [OldGoodsMarketplace] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OldGoodsMarketplace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OldGoodsMarketplace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET ARITHABORT OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OldGoodsMarketplace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OldGoodsMarketplace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OldGoodsMarketplace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OldGoodsMarketplace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET RECOVERY FULL 
GO
ALTER DATABASE [OldGoodsMarketplace] SET  MULTI_USER 
GO
ALTER DATABASE [OldGoodsMarketplace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OldGoodsMarketplace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OldGoodsMarketplace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OldGoodsMarketplace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OldGoodsMarketplace] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OldGoodsMarketplace] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'OldGoodsMarketplace', N'ON'
GO
ALTER DATABASE [OldGoodsMarketplace] SET QUERY_STORE = ON
GO
ALTER DATABASE [OldGoodsMarketplace] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [OldGoodsMarketplace]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 3/6/2025 3:03:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discounts]    Script Date: 3/6/2025 3:03:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discounts](
	[DiscountID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[DiscountPercent] [float] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[DiscountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 3/6/2025 3:03:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 3/6/2025 3:03:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[BuyerID] [int] NOT NULL,
	[VendorID] [int] NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](20) NULL,
	[CreatedAt] [datetime] NULL,
	[CompletedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 3/6/2025 3:03:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CategoryID] [int] NULL,
	[Condition] [nvarchar](50) NOT NULL,
	[Images] [nvarchar](max) NULL,
	[Location] [nvarchar](255) NOT NULL,
	[Status] [nvarchar](20) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ratings]    Script Date: 3/6/2025 3:03:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ratings](
	[RatingID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[RatingValue] [float] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[RatingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/6/2025 3:03:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Gender] [nvarchar](10) NULL,
	[BirthOfDate] [date] NULL,
	[Gmail] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](15) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](255) NULL,
	[AvatarUrl] [nvarchar](255) NULL,
	[Balance] [decimal](18, 2) NULL,
	[TotalPosts] [int] NULL,
	[TotalPurchases] [int] NULL,
	[Rating] [float] NULL,
	[Status] [nvarchar](20) NULL,
	[IsVerified] [bit] NULL,
	[LastLoginAt] [datetime] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wishlists]    Script Date: 3/6/2025 3:03:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wishlists](
	[WishlistID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[WishlistID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedAt]) VALUES (1, N'Ði?n t?', CAST(N'2025-03-06T13:50:00.767' AS DateTime))
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedAt]) VALUES (2, N'Qu?n áo', CAST(N'2025-03-06T13:50:00.767' AS DateTime))
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedAt]) VALUES (3, N'N?i th?t', CAST(N'2025-03-06T13:50:00.767' AS DateTime))
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedAt]) VALUES (4, N'Sách', CAST(N'2025-03-06T13:50:00.767' AS DateTime))
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Discounts] ON 

INSERT [dbo].[Discounts] ([DiscountID], [Code], [DiscountPercent], [ExpirationDate], [IsActive]) VALUES (1, N'WELCOME10', 10, CAST(N'2025-04-05T13:50:00.773' AS DateTime), 1)
INSERT [dbo].[Discounts] ([DiscountID], [Code], [DiscountPercent], [ExpirationDate], [IsActive]) VALUES (2, N'SUMMER20', 20, CAST(N'2025-05-05T13:50:00.773' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Discounts] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Price]) VALUES (1, 1, 1, CAST(12000000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Price]) VALUES (2, 2, 2, CAST(300000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderID], [BuyerID], [VendorID], [TotalPrice], [Status], [CreatedAt], [CompletedAt]) VALUES (1, 3, 2, CAST(12000000.00 AS Decimal(18, 2)), N'Pending', CAST(N'2025-03-06T13:50:00.770' AS DateTime), NULL)
INSERT [dbo].[Orders] ([OrderID], [BuyerID], [VendorID], [TotalPrice], [Status], [CreatedAt], [CompletedAt]) VALUES (2, 3, 2, CAST(300000.00 AS Decimal(18, 2)), N'Completed', CAST(N'2025-03-06T13:50:00.770' AS DateTime), CAST(N'2025-03-06T13:50:00.770' AS DateTime))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [UserID], [Title], [Description], [Price], [CategoryID], [Condition], [Images], [Location], [Status], [CreatedAt], [UpdatedAt]) VALUES (1, 2, N'Ði?n tho?i iPhone 12', N'iPhone 12 cu, còn 99% nhu m?i', CAST(12000000.00 AS Decimal(18, 2)), 1, N'Like New', N'iphone12.jpg', N'Hà N?i', N'Approved', CAST(N'2025-03-06T13:50:00.770' AS DateTime), CAST(N'2025-03-06T13:50:00.770' AS DateTime))
INSERT [dbo].[Products] ([ProductID], [UserID], [Title], [Description], [Price], [CategoryID], [Condition], [Images], [Location], [Status], [CreatedAt], [UpdatedAt]) VALUES (2, 2, N'Áo khoác nam', N'Áo khoác ch?ng nu?c, size L', CAST(300000.00 AS Decimal(18, 2)), 2, N'Used', N'aokhoac.jpg', N'TP HCM', N'Approved', CAST(N'2025-03-06T13:50:00.770' AS DateTime), CAST(N'2025-03-06T13:50:00.770' AS DateTime))
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Ratings] ON 

INSERT [dbo].[Ratings] ([RatingID], [UserID], [ProductID], [RatingValue], [Comment], [CreatedAt]) VALUES (1, 3, 1, 5, N'S?n ph?m r?t t?t, dùng mu?t mà!', CAST(N'2025-03-06T13:50:00.773' AS DateTime))
INSERT [dbo].[Ratings] ([RatingID], [UserID], [ProductID], [RatingValue], [Comment], [CreatedAt]) VALUES (2, 3, 2, 4, N'Áo khoác khá ?n, hoi r?ng hon mong d?i.', CAST(N'2025-03-06T13:50:00.773' AS DateTime))
SET IDENTITY_INSERT [dbo].[Ratings] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Username], [PasswordHash], [Name], [Gender], [BirthOfDate], [Gmail], [PhoneNumber], [Role], [Address], [AvatarUrl], [Balance], [TotalPosts], [TotalPurchases], [Rating], [Status], [IsVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (1, N'admin', N'hashedpassword1', N'Admin User', N'Male', CAST(N'1990-01-01' AS Date), N'admin@example.com', N'0987654321', N'Admin', N'Hà N?i', NULL, CAST(0.00 AS Decimal(18, 2)), 0, 0, 5, N'Active', 1, CAST(N'2025-03-06T13:50:00.763' AS DateTime), CAST(N'2025-03-06T13:50:00.763' AS DateTime), CAST(N'2025-03-06T13:50:00.763' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Username], [PasswordHash], [Name], [Gender], [BirthOfDate], [Gmail], [PhoneNumber], [Role], [Address], [AvatarUrl], [Balance], [TotalPosts], [TotalPurchases], [Rating], [Status], [IsVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (2, N'seller1', N'hashedpassword2', N'Nguy?n Van A', N'Male', CAST(N'1995-05-10' AS Date), N'seller1@example.com', N'0901234567', N'Seller', N'Hà N?i', N'avatar1.jpg', CAST(1000000.00 AS Decimal(18, 2)), 5, 0, 4.5, N'Active', 1, CAST(N'2025-03-06T13:50:00.763' AS DateTime), CAST(N'2025-03-06T13:50:00.763' AS DateTime), CAST(N'2025-03-06T13:50:00.763' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Username], [PasswordHash], [Name], [Gender], [BirthOfDate], [Gmail], [PhoneNumber], [Role], [Address], [AvatarUrl], [Balance], [TotalPosts], [TotalPurchases], [Rating], [Status], [IsVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (3, N'buyer1', N'hashedpassword3', N'Tr?n Th? B', N'Female', CAST(N'1998-09-15' AS Date), N'buyer1@example.com', N'0912345678', N'Buyer', N'TP HCM', N'avatar2.jpg', CAST(500000.00 AS Decimal(18, 2)), 0, 2, 4, N'Active', 1, CAST(N'2025-03-06T13:50:00.763' AS DateTime), CAST(N'2025-03-06T13:50:00.763' AS DateTime), CAST(N'2025-03-06T13:50:00.763' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Wishlists] ON 

INSERT [dbo].[Wishlists] ([WishlistID], [UserID], [ProductID], [CreatedAt]) VALUES (1, 3, 1, CAST(N'2025-03-06T13:50:00.773' AS DateTime))
INSERT [dbo].[Wishlists] ([WishlistID], [UserID], [ProductID], [CreatedAt]) VALUES (2, 3, 2, CAST(N'2025-03-06T13:50:00.773' AS DateTime))
SET IDENTITY_INSERT [dbo].[Wishlists] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Categori__8517B2E00D5C0F84]    Script Date: 3/6/2025 3:03:46 PM ******/
ALTER TABLE [dbo].[Categories] ADD UNIQUE NONCLUSTERED 
(
	[CategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Discount__A25C5AA7E1237F84]    Script Date: 3/6/2025 3:03:46 PM ******/
ALTER TABLE [dbo].[Discounts] ADD UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__536C85E497751ECE]    Script Date: 3/6/2025 3:03:46 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__85FB4E38A23DC978]    Script Date: 3/6/2025 3:03:46 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[PhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__B488B103C13CF10F]    Script Date: 3/6/2025 3:03:46 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Gmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Discounts] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Ratings] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [Balance]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [TotalPosts]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [TotalPurchases]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [Rating]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('Active') FOR [Status]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsVerified]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Wishlists] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Buyer] FOREIGN KEY([BuyerID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Buyer]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Vendor] FOREIGN KEY([VendorID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Vendor]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD  CONSTRAINT [FK_Ratings_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Ratings] CHECK CONSTRAINT [FK_Ratings_Product]
GO
ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD  CONSTRAINT [FK_Ratings_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Ratings] CHECK CONSTRAINT [FK_Ratings_User]
GO
ALTER TABLE [dbo].[Wishlists]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Wishlists]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Discounts]  WITH CHECK ADD CHECK  (([DiscountPercent]>=(0) AND [DiscountPercent]<=(100)))
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD CHECK  (([Status]='Cancelled' OR [Status]='Completed' OR [Status]='Pending'))
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD CHECK  (([Condition]='Damaged' OR [Condition]='Used' OR [Condition]='Like New' OR [Condition]='New'))
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD CHECK  (([Status]='Rejected' OR [Status]='Sold' OR [Status]='Approved' OR [Status]='Pending'))
GO
ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD CHECK  (([RatingValue]>=(1) AND [RatingValue]<=(5)))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([Gender]='Other' OR [Gender]='Female' OR [Gender]='Male'))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([Role]='Buyer' OR [Role]='Seller' OR [Role]='Admin'))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([Status]='Inactive' OR [Status]='Banned' OR [Status]='Active'))
GO
USE [master]
GO
ALTER DATABASE [OldGoodsMarketplace] SET  READ_WRITE 
GO
