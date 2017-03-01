USE [DBPATIENTNOWAIT]
GO

/****** Object:  Table [dbo].[USER]    Script Date: 28-02-2017 14:13:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[USER](
	[USERID] [int] NOT NULL,
	[USERNAME] [varchar](8000) NULL,
	[USERLOGINDATETIME] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[USERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[USER] ADD  DEFAULT (getdate()) FOR [USERLOGINDATETIME]
GO

