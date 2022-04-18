CREATE DATABASE fia
GO
USE [fia]
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[CarID] [int] IDENTITY(1,1) NOT NULL,
	[Model] [varchar](50) NOT NULL,
	[Manufacturer] [varchar](50) NOT NULL,
	[Year] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drivers](
	[DriverID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Lastname] [varchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[Titles] [int] NOT NULL,
 CONSTRAINT [PK__Drivers__F1B1CD24BE797BC3] PRIMARY KEY CLUSTERED 
(
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tracks]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tracks](
	[TrackID] [int] IDENTITY(1,1) NOT NULL,
	[TrackName] [varchar](50) NOT NULL,
	[Location] [varchar](50) NOT NULL,
	[Length] [int] NOT NULL,
	[Capacity] [int] NOT NULL,
	[Turns] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TrackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Winners]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Winners](
	[WinnerID] [int] IDENTITY(1,1) NOT NULL,
	[DriverID] [int] NOT NULL,
	[CarID] [int] NOT NULL,
	[TrackID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK__Winners__8A3D1D886AEFE77B] PRIMARY KEY CLUSTERED 
(
	[WinnerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Winners]  WITH CHECK ADD  CONSTRAINT [fk_winners__cars] FOREIGN KEY([CarID])
REFERENCES [dbo].[Cars] ([CarID])
GO
ALTER TABLE [dbo].[Winners] CHECK CONSTRAINT [fk_winners__cars]
GO
ALTER TABLE [dbo].[Winners]  WITH CHECK ADD  CONSTRAINT [fk_winners__drivers] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[Winners] CHECK CONSTRAINT [fk_winners__drivers]
GO
ALTER TABLE [dbo].[Winners]  WITH CHECK ADD  CONSTRAINT [fk_winners__tracks] FOREIGN KEY([TrackID])
REFERENCES [dbo].[Tracks] ([TrackID])
GO
ALTER TABLE [dbo].[Winners] CHECK CONSTRAINT [fk_winners__tracks]
GO
/****** Object:  StoredProcedure [dbo].[Cars_Add]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Cars_Add]
	@Model varchar(50),
	@Manufacturer varchar(50),
	@Year int
AS
	INSERT INTO Cars(Model,Manufacturer,[Year])
	VALUES (@Model,@Manufacturer,@Year )
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Cars_Delete]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Cars_Delete]
@id int
AS
DELETE FROM Cars WHERE CarID=@id
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Cars_Update]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Cars_Update]
	@Model varchar(50),
	@Manufacturer varchar(50),
	@Year int,
	@CarID int
AS
	UPDATE Cars set Model=@Model,Manufacturer=@Manufacturer,[Year]=@Year
	WHERE CarID=@CarID
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Drivers_Add]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Drivers_Add]
	@Name varchar(50),
	@Lastname varchar(50),
	@Age int,
	@Country varchar(50),
	@Titles int
AS
	INSERT INTO Drivers([Name], Lastname,Age,Country,Titles)
	VALUES (@Name,@Lastname,@Age,@Country,@Titles )
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Drivers_Delete]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Drivers_Delete]
@id int
AS
DELETE FROM Drivers WHERE DriverID=@id
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Drivers_Update]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Drivers_Update]
	@Name varchar(50),
	@Lastname varchar(50),
	@Age int,
	@Country varchar(50),
	@Titles int,
	@DriverID int
AS
	UPDATE Drivers set [Name]=@Name,Lastname=@Lastname,Age=@Age,Country=@Country,Titles=@Titles
	WHERE DriverID=@DriverID
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Tracks_Add]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Tracks_Add]
	@TrackName varchar(50),
	@Location varchar(50),
	@Length int,
	@Capacity int,
	@Turns int
AS
	INSERT INTO Tracks(TrackName,[Location],[Length],Capacity,Turns)
	VALUES (@TrackName,@Location,@Length,@Capacity,@Turns)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Tracks_Delete]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Tracks_Delete]
@id int
AS
DELETE FROM Tracks WHERE TrackID=@id
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Tracks_Update]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Tracks_Update]
	@TrackName varchar(50),
	@Location varchar(50),
	@Length int,
	@Capacity int,
	@Turns int,
	@TrackID int
AS
	UPDATE Tracks set TrackName=@TrackName,[Location]=@Location,[Length]=@Length,Capacity=@Capacity,Turns=@Turns
	WHERE TrackID=@TrackID
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[UpdateAllAtOnce]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateAllAtOnce]
@WinnerID int,
@DriverID int,
@CarID int,
@TrackID int,
@Date Datetime,
@Name varchar(50),
@Lastname varchar(50),
@Age int,
@Country varchar(50),
@Titles int,
@Model varchar(50),
@Manufacturer varchar(50),
@Year int,
@TrackName varchar(50),
@Location varchar(50),
@Length int,
@Capacity int,
@Turns int
AS
UPDATE Winners SET [Date] = @Date WHERE WinnerID=@WinnerID;
UPDATE Drivers SET [Name] = @Name,Lastname=@Lastname,Age=@Age,Country=@Country,Titles=@Titles WHERE DriverID=@DriverID;
UPDATE Cars SET Model=@Model,Manufacturer=@Manufacturer,[Year]=@Year WHERE CarID=@CarID;
UPDATE Tracks SET TrackName=@TrackName, [Location]=@Location,[Length]=@Length, Capacity=@Capacity,Turns=@Turns WHERE TrackID=@TrackID;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Winners_Add]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Winners_Add]
	@DriverID int,
	@CarID int,
	@TrackID int,
	@Date Datetime
AS
	INSERT INTO Winners(DriverID, CarID,TrackID, [Date])
	VALUES (@DriverID, @CarID, @TrackID, @Date)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Winners_Delete]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Winners_Delete]
@id int
AS
DELETE FROM Winners WHERE WinnerID=@id
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Winners_GetAll]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Winners_GetAll]
AS
	select * from Winners as w inner join Drivers as d on w.DriverID=d.DriverID
inner join Cars as c on w.CarID=c.CarID inner join Tracks as t on w.TrackID=t.TrackID

RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Winners_GetByID]    Script Date: 7/12/2021 22:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Winners_GetByID]
@id int
AS
select * from Winners as w inner join Drivers as d on w.DriverID=d.DriverID
inner join Cars as c on w.CarID=c.CarID inner join Tracks as t on w.TrackID=t.TrackID where WinnerID=@id
RETURN 0
GO
USE [master]
GO
ALTER DATABASE [fia] SET  READ_WRITE 
GO
