CREATE DATABASE [FlatsDB]
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO
ALTER DATABASE [FlatsDB] SET COMPATIBILITY_LEVEL = 100
GO
ALTER DATABASE [FlatsDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FlatsDB] SET  DISABLE_BROKER 
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flats](
	[f_id] [int] IDENTITY(1,1) NOT NULL,
	[f_type] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[f_rooms] [int] NULL,
	[f_baths] [int] NULL,
	[f_hall] [int] NULL,
	[f_kitchens] [int] NULL,
	[f_location] [nchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[f_image] [nchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[f_status] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[u_id] [varchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Flats] PRIMARY KEY CLUSTERED 
(
	[f_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[c_id] [int] IDENTITY(1,1) NOT NULL,
	[c_name] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[c_cnic] [nchar](16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[c_phone] [nchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[f_id] [int] NULL,
	[c_sMonth] [nchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[c_rentStatus] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[u_id] [varchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[c_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rents](
	[r_id] [int] IDENTITY(1,1) NOT NULL,
	[c_id] [int] NULL,
	[f_id] [int] NULL,
	[r_month] [nchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Rents] PRIMARY KEY CLUSTERED 
(
	[r_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RMC](
	[f_id] [int] NULL,
	[c_id] [int] NULL,
	[c_name] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[c_phone] [nchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[r_status] [nchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)

GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[u_id] [varchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[u_name] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[u_email] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[u_password] [char](8) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[online?] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[u_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Flats] FOREIGN KEY([f_id])
REFERENCES [dbo].[Flats] ([f_id])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Flats]
GO

ALTER TABLE [dbo].[Rents]  WITH CHECK ADD  CONSTRAINT [FK_Rents_Customers] FOREIGN KEY([c_id])
REFERENCES [dbo].[Customers] ([c_id])
GO
ALTER TABLE [dbo].[Rents] CHECK CONSTRAINT [FK_Rents_Customers]
GO

ALTER TABLE [dbo].[Rents]  WITH CHECK ADD  CONSTRAINT [FK_Rents_Flats] FOREIGN KEY([f_id])
REFERENCES [dbo].[Flats] ([f_id])
GO
ALTER TABLE [dbo].[Rents] CHECK CONSTRAINT [FK_Rents_Flats]
GO

ALTER TABLE [dbo].[RMC]  WITH CHECK ADD  CONSTRAINT [FK_RMC_Customers] FOREIGN KEY([c_id])
REFERENCES [dbo].[Customers] ([c_id])
GO
ALTER TABLE [dbo].[RMC] CHECK CONSTRAINT [FK_RMC_Customers]
GO

ALTER TABLE [dbo].[RMC]  WITH CHECK ADD  CONSTRAINT [FK_RMC_Flats] FOREIGN KEY([f_id])
REFERENCES [dbo].[Flats] ([f_id])
GO
ALTER TABLE [dbo].[RMC] CHECK CONSTRAINT [FK_RMC_Flats]
GO

