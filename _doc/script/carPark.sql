USE [CarPark]
GO
/****** Object:  Table [dbo].[Auth]    Script Date: 7.09.2020 15:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auth](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](150) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
 CONSTRAINT [PK_Auth] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Car]    Script Date: 7.09.2020 15:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Plate] [nvarchar](50) NOT NULL,
	[Brand] [nvarchar](50) NULL,
	[Model] [nvarchar](50) NULL,
 CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 7.09.2020 15:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocationMove]    Script Date: 7.09.2020 15:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocationMove](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CarId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[EntryDate] [datetime] NOT NULL,
	[ExitDate] [datetime] NULL,
 CONSTRAINT [PK_LocationMove] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personnel]    Script Date: 7.09.2020 15:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personnel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TC] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Personnel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 7.09.2020 15:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](150) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfileDetail]    Script Date: 7.09.2020 15:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfileId] [int] NOT NULL,
	[AuthId] [int] NOT NULL,
 CONSTRAINT [PK_ProfileDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfilePersonnel]    Script Date: 7.09.2020 15:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfilePersonnel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfileId] [int] NOT NULL,
	[PersonnelId] [int] NOT NULL,
 CONSTRAINT [PK_ProfileEmployee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Auth] ON 

INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (1, N'PAGE_AUTH_LIST', N'Yetki Listeleme', 1, CAST(N'2020-09-07T12:39:07.690' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (2, N'PAGE_AUTH_ADD', N'Yetki Ekleme', 1, CAST(N'2020-09-07T12:39:50.830' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (3, N'PAGE_AUTH_EDIT', N'Yetki Düzenleme', 1, CAST(N'2020-09-07T12:41:17.590' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (4, N'PAGE_AUTH_DISPLAY', N'Yetki Görüntüleme', 1, CAST(N'2020-09-07T12:41:25.907' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (5, N'PAGE_AUTH_DELETE', N'Yetki Silme', 1, CAST(N'2020-09-07T12:41:33.837' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (6, N'PAGE_CAR_LIST', N'Araç Listeleme', 1, CAST(N'2020-09-07T12:41:45.207' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (7, N'PAGE_CAR_ADD', N'Araç Eklme', 1, CAST(N'2020-09-07T12:41:53.027' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (8, N'PAGE_CAR_EDIT', N'Araç Düzenleme', 1, CAST(N'2020-09-07T12:42:06.713' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (9, N'PAGE_CAR_DISPLAY', N'Araç Görüntüleme', 1, CAST(N'2020-09-07T12:42:22.203' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (10, N'PAGE_LOCATION_LIST', N'Lokasyon Listeleme', 1, CAST(N'2020-09-07T12:42:35.363' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (11, N'PAGE_LOCATION_ADD', N'Lokasyon Ekleme', 1, CAST(N'2020-09-07T12:42:45.390' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (12, N'PAGE_LOCATION_EDIT', N'Lokasyon Düzenleme', 1, CAST(N'2020-09-07T12:42:56.643' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (13, N'PAGE_LOCATION_DISPLAY', N'Lokasyon Görüntüleme', 1, CAST(N'2020-09-07T12:43:10.767' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (14, N'PAGE_LOCATIONMOVE_LIST', N'Lokasyon Hareketi Listeleme', 1, CAST(N'2020-09-07T12:44:43.770' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (15, N'PAGE_LOCATIONMOVE_ADD', N'Lokasyon Hareketi Ekleme', 1, CAST(N'2020-09-07T12:44:58.543' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (16, N'PAGE_LOCATIONMOVE_EDIT', N'Lokasyon Hareketi Düzenleme', 1, CAST(N'2020-09-07T12:45:11.573' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (17, N'PAGE_LOCATIONMOVE_DISPLAY', N'Lokasyon Hareketi Görüntüleme', 1, CAST(N'2020-09-07T12:45:25.027' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (18, N'PAGE_PERSONNEL_LIST', N'Personel Listeleme', 1, CAST(N'2020-09-07T12:45:34.967' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (19, N'PAGE_PERSONNEL_ADD', N'Personel Ekleme', 1, CAST(N'2020-09-07T12:45:43.593' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (20, N'PAGE_PERSONNEL_EDIT', N'Personel Düzenleme', 1, CAST(N'2020-09-07T12:45:51.233' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (21, N'PAGE_PERSONNEL_DISPLAY', N'Personel Görüntüleme', 1, CAST(N'2020-09-07T12:46:02.413' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (22, N'PAGE_PROFILE_LIST', N'Profil Listeleme', 1, CAST(N'2020-09-07T12:46:11.253' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (23, N'PAGE_PROFILE_ADD', N'Profil Ekleme', 1, CAST(N'2020-09-07T12:46:20.093' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (24, N'PAGE_PROFILE_EDIT', N'Profil Düzenleme', 1, CAST(N'2020-09-07T12:46:30.620' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (25, N'PAGE_PROFILE_DISPLAY', N'Profil Görüntüleme', 1, CAST(N'2020-09-07T12:46:38.493' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (26, N'PAGE_PROFILE_DELETE', N'Profil Silme', 1, CAST(N'2020-09-07T12:46:48.403' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (27, N'PAGE_PROFILEDETAIL_BATCHEDIT', N'Profil Detaylarını Görüntüleme', 1, CAST(N'2020-09-07T12:47:00.743' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (28, N'PAGE_PROFILEPERSONNEL_BATCHEDIT', N'Profil Kullanıcılarını Görüntüleme', 1, CAST(N'2020-09-07T12:47:13.633' AS DateTime), 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Auth] OFF
GO
SET IDENTITY_INSERT [dbo].[Car] ON 

INSERT [dbo].[Car] ([Id], [Plate], [Brand], [Model]) VALUES (1, N'41AA4141', N'BMW', N'İ30')
INSERT [dbo].[Car] ([Id], [Plate], [Brand], [Model]) VALUES (2, N'41AA4142', N'BMW', NULL)
INSERT [dbo].[Car] ([Id], [Plate], [Brand], [Model]) VALUES (3, N'41AA4143', N'AUDI', N'S40')
INSERT [dbo].[Car] ([Id], [Plate], [Brand], [Model]) VALUES (4, N'41AA4144', N'RENAULT', N'CLIO')
INSERT [dbo].[Car] ([Id], [Plate], [Brand], [Model]) VALUES (5, N'41AA4145', N'CITROEN', N'NEMO')
SET IDENTITY_INSERT [dbo].[Car] OFF
GO
SET IDENTITY_INSERT [dbo].[Location] ON 

INSERT [dbo].[Location] ([Id], [Name]) VALUES (1, N'Location 1')
INSERT [dbo].[Location] ([Id], [Name]) VALUES (2, N'Location 2')
INSERT [dbo].[Location] ([Id], [Name]) VALUES (3, N'Location 3')
INSERT [dbo].[Location] ([Id], [Name]) VALUES (4, N'Location 4')
INSERT [dbo].[Location] ([Id], [Name]) VALUES (5, N'Location 5')
INSERT [dbo].[Location] ([Id], [Name]) VALUES (6, N'Location 6')
INSERT [dbo].[Location] ([Id], [Name]) VALUES (7, N'Location 7')
SET IDENTITY_INSERT [dbo].[Location] OFF
GO
SET IDENTITY_INSERT [dbo].[LocationMove] ON 

INSERT [dbo].[LocationMove] ([Id], [CarId], [LocationId], [EntryDate], [ExitDate]) VALUES (1, 1, 1, CAST(N'2020-09-07T15:46:34.313' AS DateTime), CAST(N'2020-09-07T15:46:51.567' AS DateTime))
INSERT [dbo].[LocationMove] ([Id], [CarId], [LocationId], [EntryDate], [ExitDate]) VALUES (2, 3, 1, CAST(N'2020-09-07T15:47:03.843' AS DateTime), CAST(N'2020-09-07T15:47:08.863' AS DateTime))
INSERT [dbo].[LocationMove] ([Id], [CarId], [LocationId], [EntryDate], [ExitDate]) VALUES (3, 3, 2, CAST(N'2020-09-07T15:48:13.467' AS DateTime), NULL)
INSERT [dbo].[LocationMove] ([Id], [CarId], [LocationId], [EntryDate], [ExitDate]) VALUES (4, 5, 3, CAST(N'2020-09-07T15:48:17.437' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[LocationMove] OFF
GO
SET IDENTITY_INSERT [dbo].[Personnel] ON 

INSERT [dbo].[Personnel] ([Id], [TC], [Name], [LastName], [Phone], [Address], [UserName], [Password]) VALUES (1, N'11122233344', N'Talha', N'Erdoğan', N'5558887799', N'A.Sok B Cd. No : D ', N'admin', N'1')
INSERT [dbo].[Personnel] ([Id], [TC], [Name], [LastName], [Phone], [Address], [UserName], [Password]) VALUES (2, N'99988877766', N'TALHA', N'ERDOGAN', N'5538889966', N'E Sok. D Cd. No:C B/A', N'personnel', N'1')
SET IDENTITY_INSERT [dbo].[Personnel] OFF
GO
SET IDENTITY_INSERT [dbo].[Profile] ON 

INSERT [dbo].[Profile] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (1, N'SYSTEMADMIN', N'Admin Profile', 1, CAST(N'2020-09-07T12:37:42.757' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Profile] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (2, N'PERSONNEL_PROFILE', N'Personnel Profile', 1, CAST(N'2020-09-07T12:38:16.383' AS DateTime), 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Profile] OFF
GO
SET IDENTITY_INSERT [dbo].[ProfileDetail] ON 

INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (5, 1, 20)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (6, 1, 19)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (7, 1, 21)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (8, 1, 18)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (9, 1, 27)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (10, 1, 24)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (11, 1, 23)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (12, 1, 25)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (13, 1, 28)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (14, 1, 22)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (15, 1, 26)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (16, 1, 3)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (17, 1, 2)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (18, 1, 4)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (19, 1, 1)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (20, 1, 5)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (21, 2, 8)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (22, 2, 7)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (23, 2, 9)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (24, 2, 6)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (25, 2, 12)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (26, 2, 11)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (27, 2, 13)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (28, 2, 16)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (29, 2, 15)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (30, 2, 17)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (31, 2, 14)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (32, 2, 10)
SET IDENTITY_INSERT [dbo].[ProfileDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[ProfilePersonnel] ON 

INSERT [dbo].[ProfilePersonnel] ([Id], [ProfileId], [PersonnelId]) VALUES (1, 1, 1)
INSERT [dbo].[ProfilePersonnel] ([Id], [ProfileId], [PersonnelId]) VALUES (2, 2, 2)
SET IDENTITY_INSERT [dbo].[ProfilePersonnel] OFF
GO
