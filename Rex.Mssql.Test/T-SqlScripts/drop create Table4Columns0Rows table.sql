USE [RexUnitTesting]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Table4Columns0Rows]') AND type in (N'U'))
DROP TABLE [dbo].[Table4Columns0Rows]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Table4Columns0Rows]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Table4Columns0Rows](
	[Column1] [int] NOT NULL PRIMARY KEY,
	[Column2] [date] NULL,
	[Column3] [varchar](max) NOT NULL,
	[Column4] [varbinary](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO


