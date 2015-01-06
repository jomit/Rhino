CREATE TABLE [dbo].[Tags](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Tree] [nvarchar](100) NOT NULL,
	[Branch] [nvarchar](100) NULL,
	[Nodes] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Content](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Text] [nvarchar](max) NULL,
	[Tags] [nvarchar](max) NULL,
	[FileName] [nvarchar](500) NULL,
	[FileLocation] [nvarchar](500) NULL,
	[FileContentType] [nvarchar](500) NULL,
	[FileSize] [decimal](18, 0) NULL,
	[ThumbnailName] [nvarchar](500) NULL,
	[ThumbnailLocation] [nvarchar](500) NULL,
	[ThumbnailContentType] [nvarchar](500) NULL,
	[ThumbnailSize] [decimal](18, 0) NULL,
	[IsApproved] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UserName] [nvarchar](500) NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

