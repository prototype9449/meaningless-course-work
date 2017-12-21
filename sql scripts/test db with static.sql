
use Test_100_000
GO
CREATE TABLE [dbo].[TestTable](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BoolType] [bit] not null,
	[IntType] [int] not null,
	[StringType] [nvarchar](500) NOT NULL,
	[DateTimeType] [datetime] NOT NULL,
	[DateTimeOffsetType] [datetimeoffset] NOT NULL,
	[TimeType] [time] NOT NULL,
	[GuidType] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TestTable] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[Employees](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BoolType] [bit] not null,
	[IntType] [int] not null,
	[StringType] [nvarchar](500) NOT NULL,
	[DateTimeType] [datetime] NOT NULL,
	[DateTimeOffsetType] [datetimeoffset] NOT NULL,
	[TimeType] [time] NOT NULL,
	[GuidType] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeGroups]    Script Date: 5/2/2017 8:52:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeGroups](
	[EmployeeId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_Emplyee_Groups] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Groups]    Script Date: 5/2/2017 8:52:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_groups] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

create TABLE [dbo].[Policies](	
	[GroupId] [int] NOT NULL,
	[PredicateId] [int] NOT NULL	
 CONSTRAINT [PK_Policies] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC, [PredicateId] asc
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Predicates](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](1000) NOT NULL,
	[TableName] [nvarchar](100) not null,
 CONSTRAINT [PK_Predicates] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO

ALTER TABLE [dbo].[Policies]  WITH CHECK ADD  CONSTRAINT [FK_Policies_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Policies] CHECK CONSTRAINT [FK_Policies_Groups]
GO
ALTER TABLE [dbo].[Policies]  WITH CHECK ADD  CONSTRAINT [FK_Policies_Predicates] FOREIGN KEY([PredicateId])
REFERENCES [dbo].[Predicates] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Policies] CHECK CONSTRAINT [FK_Policies_Predicates]
GO
ALTER TABLE [dbo].[EmployeeGroups]  WITH NOCHECK ADD  CONSTRAINT [FK_EmployeeId1_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeGroups] CHECK CONSTRAINT [FK_EmployeeId1_Employees]
GO
ALTER TABLE [dbo].[EmployeeGroups]  WITH NOCHECK ADD  CONSTRAINT [FK_GroupId_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeGroups] CHECK CONSTRAINT [FK_GroupId_Groups]
GO

create PROCEDURE setInitialContext(   
    @UserId int)
AS
	SET NOCOUNT ON;  
	EXEC sp_set_session_context 'UserId', @UserId; 
GO   

EXEC sp_configure 'clr enabled', 1  
RECONFIGURE; 


ALTER DATABASE Test_100_000 SET TRUSTWORTHY ON;


drop security policy if exists dbo.[TestTablePolicy]   

drop function if exists dbo.securityPredicateTestTable
go
drop function if exists dbo.getUserAccess
go
drop function if exists dbo.getUserAccessClr
go
drop ASSEMBLY if exists Parser
go
create ASSEMBLY Parser FROM 'E:\RemotedProjects\security-policy\WebApplication1\SqlParcer\bin\Release\SqlParcer.dll'
WITH PERMISSION_SET = UNSAFE;    
GO 

go
create FUNCTION dbo.getUserAccessClr(	
	@predicates nvarchar(max), 
	@id1 int, @BoolType1 bit, @IntType1 int, @StringType1 nvarchar(500), @DateTimeType1 datetime, @DateTimeOffsetType1 datetimeoffset, 
	@TimeType1 time, @GuidType1 uniqueidentifier,
	@id2 int, @BoolType2 bit, @IntType2 int, @StringType2 nvarchar(500), @DateTimeType2 datetime, @DateTimeOffsetType2 datetimeoffset, 
	@TimeType2 time, @GuidType2 uniqueidentifier
) RETURNS bit  
 AS EXTERNAL NAME Parser.[SqlParser.ContextParser].ExecuteStaticPredicate;   
GO 

CREATE FUNCTION dbo.getUserAccess(@CurrentTableName nvarchar(200), @id1 int, @BoolType1 bit, @IntType1 int, @StringType1 nvarchar(500), @DateTimeType1 datetime, @DateTimeOffsetType1 datetimeoffset, 
	@TimeType1 time, @GuidType1 uniqueidentifier)  
RETURNS bit
WITH SCHEMABINDING
AS   
BEGIN
 DECLARE @predicates nvarchar(max);
 Declare @result bit;
 Declare @userId int = CAST(SESSION_CONTEXT(N'UserId') AS int)
 select @predicates = COALESCE(@predicates + ' and ', '') + dbo.Predicates.Value from 
	 dbo.Predicates join  dbo.Policies 
	on  dbo.Predicates.id =  dbo.Policies.PredicateId 
	and  dbo.Predicates.TableName = @CurrentTableName
	join  dbo.EmployeeGroups 
	on  dbo.EmployeeGroups.GroupId =  dbo.Policies.GroupId
	and  dbo.EmployeeGroups.EmployeeId = @userId;

DECLARE @id2 int;
DECLARE @BoolType2 bit;
DECLARE @IntType2 int;
DECLARE @StringType2 nvarchar(400);
DECLARE @DateTimeType2 datetime;
DECLARE @DateTimeOffsetType2 datetimeoffset;
DECLARE @TimeType2 time;
DECLARE @GuidType2 uniqueidentifier;

select @id2 = id, @BoolType2 = BoolType, @IntType2 =IntType, @StringType2 = StringType, 
@DateTimeType2 = DateTimeType, @DateTimeOffsetType2 = DateTimeOffsetType, @DateTimeOffsetType2 = DateTimeOffsetType, @TimeType2 =TimeType, @GuidType2 = GuidType 
from dbo.Employees where id = @userId

	if @predicates is Null
	  begin
	    set @result = 1
	  end
	else
	  begin
	    select @result = dbo.getUserAccessClr(
	    	@predicates,
	    	@id1, @BoolType1, @IntType1, @StringType1, @DateTimeType1, @DateTimeOffsetType1, @TimeType1, @GuidType1,
	    	@id2, @BoolType2, @IntType2, @StringType2, @DateTimeType2, @DateTimeOffsetType2, @TimeType2, @GuidType2
	    	)
	    
	  end
	  return @result	
END;
go
create FUNCTION dbo.securityPredicateTestTable(@id int, @BoolType bit, @IntType int, @StringType nvarchar(500), @DateTimeType datetime, @DateTimeOffsetType datetimeoffset, 
	@TimeType time, @GuidType uniqueidentifier)  
    RETURNS TABLE 
	 WITH SCHEMABINDING
AS  
    RETURN SELECT 1 as Resu where ((select dbo.getUserAccess('dbo.TestTable', @id, @BoolType, @IntType, @StringType, @DateTimeType, @DateTimeOffsetType, @TimeType, @GuidType)) = 1) 
go
 
create SECURITY POLICY dbo.[TestTablePolicy]   
ADD FILTER PREDICATE dbo.securityPredicateTestTable(id, BoolType, IntType, StringType, DateTimeType, DateTimeOffsetType, TimeType, GuidType)   
    ON [dbo].[TestTable]
WITH (STATE = ON); 