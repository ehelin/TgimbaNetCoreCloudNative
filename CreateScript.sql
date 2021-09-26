﻿CREATE SCHEMA [Bucket]

CREATE TABLE [Bucket].[BrowserCapability](
	[BrowserCapabilityId] [bigint] IDENTITY(1,1) NOT NULL,
	[BrowserLogId] [bigint] NULL,
	[Key] [varchar](100) NULL,
	[Value] [varchar](500) NULL,
	[tett] [text] NULL,
 CONSTRAINT [PK_BrowserCapability] PRIMARY KEY CLUSTERED 
(
	[BrowserCapabilityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [Bucket].[BrowserLog](
	[BrowserLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[ActiveXControls] [bit] NULL,
	[AOL] [bit] NULL,
	[BackgroundSounds] [bit] NULL,
	[Beta] [bit] NULL,
	[Browser] [varchar](100) NULL,
	[CanCombineFormsInDeck] [bit] NULL,
	[CanInitiateVoiceCall] [bit] NULL,
	[CanRenderAfterInputOrSelectElement] [bit] NULL,
	[CanRenderEmptySelects] [bit] NULL,
	[CanRenderInputAndSelectElementsTogether] [bit] NULL,
	[CanRenderMixedSelects] [bit] NULL,
	[CanRenderOneventAndPrevElementsTogether] [bit] NULL,
	[CanRenderPostBackCards] [bit] NULL,
	[CanRenderSetvarZeroWithMultiSelectionList] [bit] NULL,
	[CanSendMail] [bit] NULL,
	[CDF] [bit] NULL,
	[ClrVersion] [varchar](100) NULL,
	[Cookies] [bit] NULL,
	[Crawler] [bit] NULL,
	[DefaultSubmitButtonLimit] [int] NULL,
	[EcmaScriptVersion] [varchar](100) NULL,
	[Frames] [bit] NULL,
	[GatewayMajorVersion] [int] NULL,
	[GatewayMinorVersion] [float] NULL,
	[GatewayVersion] [varchar](100) NULL,
	[HasBackButton] [bit] NULL,
	[HidesRightAlignedMultiselectScrollbars] [bit] NULL,
	[Id] [varchar](100) NULL,
	[InputType] [varchar](100) NULL,
	[IsColor] [bit] NULL,
	[IsMobileDevice] [bit] NULL,
	[JavaApplets] [bit] NULL,
	[JScriptVersion] [varchar](100) NULL,
	[MajorVersion] [int] NULL,
	[MaximumHrefLength] [int] NULL,
	[MaximumRenderedPageSize] [int] NULL,
	[MaximumSoftkeyLabelLength] [int] NULL,
	[MinorVersion] [float] NULL,
	[MinorVersionString] [varchar](100) NULL,
	[MobileDeviceManufacturer] [varchar](100) NULL,
	[MobileDeviceModel] [varchar](100) NULL,
	[MSDomVersion] [varchar](100) NULL,
	[NumberOfSoftkeys] [varchar](100) NULL,
	[Platform] [varchar](100) NULL,
	[PreferredImageMime] [varchar](100) NULL,
	[PreferredRenderingMime] [varchar](100) NULL,
	[PreferredRenderingType] [varchar](100) NULL,
	[PreferredRequestEncoding] [varchar](100) NULL,
	[PreferredResponseEncoding] [varchar](100) NULL,
	[RendersBreakBeforeWmlSelectAndInput] [bit] NULL,
	[RendersBreaksAfterHtmlLists] [bit] NULL,
	[RendersBreaksAfterWmlAnchor] [bit] NULL,
	[RendersBreaksAfterWmlInput] [bit] NULL,
	[RendersWmlDoAcceptsInline] [bit] NULL,
	[RendersWmlSelectsAsMenuCards] [bit] NULL,
	[RequiredMetaTagNameValue] [varchar](100) NULL,
	[RequiresAttributeColonSubstitution] [bit] NULL,
	[RequiresContentTypeMetaTag] [bit] NULL,
	[RequiresControlStateInSession] [bit] NULL,
	[RequiresDBCSCharacter] [bit] NULL,
	[RequiresHtmlAdaptiveErrorReporting] [bit] NULL,
	[RequiresLeadingPageBreak] [bit] NULL,
	[RequiresNoBreakInFormatting] [bit] NULL,
	[RequiresOutputOptimization] [bit] NULL,
	[RequiresPhoneNumbersAsPlainText] [bit] NULL,
	[RequiresSpecialViewStateEncoding] [bit] NULL,
	[RequiresUniqueFilePathSuffix] [bit] NULL,
	[RequiresUniqueHtmlCheckboxNames] [bit] NULL,
	[RequiresUniqueHtmlInputNames] [bit] NULL,
	[RequiresUrlEncodedPostfieldValues] [bit] NULL,
	[ScreenBitDepth] [int] NULL,
	[ScreenCharactersHeight] [int] NULL,
	[ScreenCharactersWidt] [int] NULL,
	[ScreenPixelsHeight] [int] NULL,
	[ScreenPixelsWidth] [int] NULL,
	[SupportsAccesskeyAttribute] [bit] NULL,
	[SupportsBodyColor] [bit] NULL,
	[SupportsBold] [bit] NULL,
	[SupportsCacheControlMetaTag] [bit] NULL,
	[SupportsCallback] [bit] NULL,
	[SupportsCss] [bit] NULL,
	[SupportsDivAlign] [bit] NULL,
	[SupportsDivNoWrap] [bit] NULL,
	[SupportsEmptyStringInCookieValue] [bit] NULL,
	[SupportsFontColor] [bit] NULL,
	[SupportsFontName] [bit] NULL,
	[SupportsFontSize] [bit] NULL,
	[SupportsImageSubmit] [bit] NULL,
	[SupportsIModeSymbols] [bit] NULL,
	[SupportsInputIStyle] [bit] NULL,
	[SupportsInputMode] [bit] NULL,
	[SupportsItalic] [bit] NULL,
	[SupportsJPhoneMultiMediaAttribute] [bit] NULL,
	[SupportsJPhoneSymbols] [bit] NULL,
	[SupportsQueryStringInFormAction] [bit] NULL,
	[SupportsRedirectWithCookie] [bit] NULL,
	[SupportsSelectMultiple] [bit] NULL,
	[SupportsUncheck] [bit] NULL,
	[SupportsXmlHttp] [bit] NULL,
	[Tables] [bit] NULL,
	[Type] [varchar](100) NULL,
	[UseOptimizedCacheKey] [bit] NULL,
	[VBScript] [bit] NULL,
	[Version] [varchar](100) NULL,
	[W3CDomVersion] [varchar](100) NULL,
	[Win16] [bit] NULL,
	[Win32] [bit] NULL,
 CONSTRAINT [PK_BrowserLog] PRIMARY KEY CLUSTERED 
(
	[BrowserLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Bucket].[BucketListItem](
	[BucketListItemId] [int] IDENTITY(1,1) NOT NULL,
	[ListItemName] [nvarchar](max) NULL,
	[Created] [datetime] NULL,
	[Category] [nvarchar](255) NULL,
	[Achieved] [bit] NULL,
	[CategorySortOrder] [int] NULL,
	[Latitude] [decimal](18, 10) NULL,
	[Longitude] [decimal](18, 10) NULL,
 CONSTRAINT [PK_BucketListItems] PRIMARY KEY CLUSTERED 
(
	[BucketListItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [Bucket].[BucketListUser](
	[BucketListUserId] [int] IDENTITY(1,1) NOT NULL,
	[BucketListItemId] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_BucketListUser] PRIMARY KEY CLUSTERED 
(
	[BucketListUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Bucket].[BuildStatistics](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Start] [datetime] NULL,
	[End] [datetime] NULL,
	[BuildNumber] [varchar](500) NULL,
	[Status] [varchar](500) NULL,
	[Type] [varchar](50) NULL,
 CONSTRAINT [PK_BuildStatistics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Bucket].[Log](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[LogMessage] [nvarchar](max) NULL,
	[Created] [datetime] NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [Bucket].[SystemStatistics](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[WebsiteIsUp] [bit] NULL,
	[DatabaseIsUp] [bit] NULL,
	[AzureFunctionIsUp] [bit] NULL,
	[Created] [datetime] NULL,
 CONSTRAINT [PK_SystemStatistics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Bucket].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) NULL,
	[Salt] [nvarchar](max) NULL,
	[PassWord] [nvarchar](max) NULL,
	[Email] [nvarchar](255) NULL,
	[Token] [nvarchar](1000) NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [nvarchar](255) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

