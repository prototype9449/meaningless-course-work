USE [OnlineShop]
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
	[Value] [nvarchar](200) NOT NULL,
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





ALTER DATABASE NewOnlineShop SET TRUSTWORTHY ON;


drop security policy if exists dbo.[OrdersPolicy]  
drop security policy if exists dbo.[EmployeesPolicy]   
drop security policy if exists dbo.[CustomersPolicy]  

drop function if exists dbo.securityPredicateOrders
drop function if exists dbo.securityPredicateEmployees
drop function if exists dbo.securityPredicateCustomers

go
drop function if exists dbo.getUserAccess
go
drop function if exists dbo.getUserAccessClr
go
drop ASSEMBLY if exists Parser
go
create ASSEMBLY Parser FROM 'C:\repositories\security-policy\WebApplication1\SqlParcer\bin\Debug\SqlParcer.dll'
WITH PERMISSION_SET = UNSAFE;    
GO 


 

go
create FUNCTION dbo.getUserAccessClr(
	@currentTableName nvarchar(500), 
	@userTableName nvarchar(500), 
	@predicates nvarchar(max), 
	@currentRowIdentifiers nvarchar(500),
	@userRowIdentifiers nvarchar(500)
) RETURNS bit  
 AS EXTERNAL NAME Parser.[SqlParcer.ContextParcer].ExecutePredicate;   
GO 

CREATE FUNCTION dbo.getUserAccess(@CurrentTableName nvarchar(200), @RowIdentifiers nvarchar(max))  
RETURNS bit
WITH SCHEMABINDING
AS   
BEGIN
 DECLARE @predicates nvarchar(max);
 Declare @result bit;
 select @predicates = COALESCE(@predicates + ' and ', '') + dbo.Predicates.Value from 
	 dbo.Predicates join  dbo.Policies 
	on  dbo.Predicates.id =  dbo.Policies.PredicateId 
	and  dbo.Predicates.TableName = @CurrentTableName
	join  dbo.EmployeeGroups 
	on  dbo.EmployeeGroups.GroupId =  dbo.Policies.GroupId
	and  dbo.EmployeeGroups.EmployeeId = CAST(SESSION_CONTEXT(N'UserId') AS int);

	if @predicates is Null
	  begin
	    set @result = 1
	  end
	else
	  begin
	    select @result = dbo.getUserAccessClr(
	    	@CurrentTableName, 
	    	'dbo.Employees', 
	    	@predicates, 
	    	@RowIdentifiers, 
	    	CONCAT ('[id][', cast(SESSION_CONTEXT(N'UserId') as nvarchar(50)),'][int]')
	    )
	  end
	  return @result	
END;
go
create FUNCTION dbo.securityPredicateOrders(@id int)  
    RETURNS TABLE 
	 WITH SCHEMABINDING
AS  
    RETURN SELECT 1 as Resu where ((select dbo.getUserAccess('dbo.Orders', concat('[id][', @id, '][int]'))) = 1) 
go
create FUNCTION dbo.securityPredicateEmployees(@id int)  
    RETURNS TABLE 
	 WITH SCHEMABINDING
AS  
    RETURN SELECT 1 as Resu where ((select dbo.getUserAccess('dbo.Employees', concat('[id][', @id, '][int]'))) = 1) 
go
create FUNCTION dbo.securityPredicateCustomers(@id int)  
    RETURNS TABLE 
	 WITH SCHEMABINDING
AS  
    RETURN SELECT 1 as Resu where ((select dbo.getUserAccess('dbo.Customers', concat('[id][', @id, '][int]'))) = 1)
go
 
create SECURITY POLICY dbo.[OrdersPolicy]   
ADD FILTER PREDICATE dbo.securityPredicateOrders(id)   
    ON [dbo].[Orders]
WITH (STATE = ON); 

create SECURITY POLICY dbo.[EmployeesPolicy]   
ADD FILTER PREDICATE dbo.securityPredicateEmployees(id)   
    ON [dbo].[Employees]
WITH (STATE = ON); 

create SECURITY POLICY dbo.[CustomersPolicy]   
ADD FILTER PREDICATE dbo.securityPredicateCustomers(id)   
    ON [dbo].[Customers]
WITH (STATE = ON); 









CREATE FUNCTION dbo.getColumnsAsString(@TABLE_NAME nvarchar(200), @SCHEMA_NAME nvarchar(128))  
RETURNS nvarchar(max)
AS   
BEGIN
 DECLARE @vvc_ColumnName nvarchar(128)
 DECLARE @vvc_ColumnList nvarchar(MAX)

IF @SCHEMA_NAME =''
  BEGIN
	return ''
  END
 
IF NOT EXISTS (SELECT T.name, s.name FROM sys.tables T JOIN sys.schemas S
          ON T.schema_id=S.schema_id
          WHERE T.name=@TABLE_NAME AND s.name =@SCHEMA_NAME)
  BEGIN
	return ''
  END
 
DECLARE TableCursor CURSOR FAST_FORWARD FOR
SELECT   CASE WHEN PATINDEX('% %',C.name) > 0 
         THEN '['+ C.name +']' 
         ELSE C.name 
         END
FROM     sys.columns C
JOIN     sys.tables T
ON       C.object_id  = T.object_id
JOIN     sys.schemas S
ON       S.schema_id  = T.schema_id
WHERE    T.name    = @TABLE_NAME
AND      S.name    = @SCHEMA_NAME
ORDER BY column_id

SET @vvc_ColumnList=''
 
OPEN TableCursor
FETCH NEXT FROM TableCursor INTO @vvc_ColumnName

WHILE @@FETCH_STATUS=0
  BEGIN
  SET @vvc_ColumnList = @vvc_ColumnList + @vvc_ColumnName

  -- get the details of the next column
  FETCH NEXT FROM TableCursor INTO @vvc_ColumnName

  -- add a comma if we are not at the end of the row
  IF @@FETCH_STATUS=0
    SET @vvc_ColumnList = @vvc_ColumnList + ','
  END

CLOSE TableCursor
DEALLOCATE TableCursor

return @vvc_ColumnList
END;


