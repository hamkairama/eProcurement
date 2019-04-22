USE [eProcurement]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TPROC_PC_APPR]') AND parent_object_id = OBJECT_ID(N'[dbo].[TPROC_APPR_PC]'))
ALTER TABLE [dbo].[TPROC_APPR_PC] DROP CONSTRAINT [FK_TPROC_PC_APPR]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_APP__CREAT__6501FCD8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_APPR_PC] DROP CONSTRAINT [DF__TPROC_APP__CREAT__6501FCD8]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TPROC_APP__ROW_S__65F62111]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TPROC_APPR_PC] DROP CONSTRAINT [DF__TPROC_APP__ROW_S__65F62111]
END

GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_APPR_PC]    Script Date: 01/03/2018 12:30:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TPROC_APPR_PC]') AND type in (N'U'))
DROP TABLE [dbo].[TPROC_APPR_PC]
GO

USE [eProcurement]
GO

/****** Object:  Table [dbo].[TPROC_APPR_PC]    Script Date: 01/03/2018 12:30:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TPROC_APPR_PC](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PC_ID] [int] NOT NULL,
	[USER_ID] [varchar](50) NOT NULL,
	[NAME] [varchar](50) NOT NULL,
	[EMAIL] [varchar](100) NOT NULL,
	[AS_IS] [varchar](20) NULL,
	[STATUS] [varchar](50) NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TPROC_APPR_PC]  WITH CHECK ADD  CONSTRAINT [FK_TPROC_PC_APPR] FOREIGN KEY([PC_ID])
REFERENCES [dbo].[TPROC_PC] ([ID])
GO

ALTER TABLE [dbo].[TPROC_APPR_PC] CHECK CONSTRAINT [FK_TPROC_PC_APPR]
GO

ALTER TABLE [dbo].[TPROC_APPR_PC] ADD  DEFAULT (sysdatetime()) FOR [CREATED_TIME]
GO

ALTER TABLE [dbo].[TPROC_APPR_PC] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO


