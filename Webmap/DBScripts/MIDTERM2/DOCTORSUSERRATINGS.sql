USE [DBPATIENTNOWAIT]
GO

/****** Object:  Table [dbo].[DOCTORSUSERRATINGS]    Script Date: 03-03-2017 17:37:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DOCTORSUSERRATINGS](
	[USERRATINGID] [int] NOT NULL,
	[DOCTORID] [int] NULL,
	[DOCTORNAME] [varchar](8000) NULL,
	[ClinicName] [varchar](8000) NULL,
	[USERID] [int] NULL,
	[USERNAME] [varchar](8000) NULL,
	[USERCOMMENTS] [varchar](8000) NULL,
	[USERSearchDATETIME] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[USERRATINGID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DOCTORSUSERRATINGS] ADD  DEFAULT (getdate()) FOR [USERSearchDATETIME]
GO

ALTER TABLE [dbo].[DOCTORSUSERRATINGS]  WITH CHECK ADD FOREIGN KEY([DOCTORID])
REFERENCES [dbo].[DOCTORS] ([DOCTORID])
GO

