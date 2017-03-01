USE [DBPATIENTNOWAIT]
GO

/****** Object:  Table [dbo].[CLINICS]    Script Date: 01-03-2017 11:28:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CLINICS](
	[ClinicID] [int] NOT NULL,
	[CLINICGoogleUID] [varchar](8000) NULL,
	[ClinicName] [varchar](8000) NULL,
	[CLINICADDRESS] [varchar](8000) NULL,
	[ClinicEmail] [varchar](8000) NULL,
	[ClinicTelePhone] [varchar](8000) NULL,
	[CLINICWorkingHours] [varchar](8000) NULL,
	[ClinicWebsite] [varchar](8000) NULL,
	[USERID] [int] NULL,
	[USERNAME] [varchar](8000) NULL,
	[USERSearchDATETIME] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ClinicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CLINICS] ADD  DEFAULT (getdate()) FOR [USERSearchDATETIME]
GO


