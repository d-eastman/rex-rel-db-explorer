USE [RexUnitTesting]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewOfTable4Columns0Rows]'))
DROP VIEW [dbo].[ViewOfTable4Columns0Rows]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewOfTable4Columns0Rows]
AS
SELECT        Column1, Column2, Column3, Column4, 10 * Column1 AS TenTimesColumn1
FROM            dbo.Table4Columns0Rows
GO
