USE [OnlineShop]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 5/2/2017 8:52:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](25) NOT NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customers]    Script Date: 5/2/2017 8:52:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[CompanyName] [nvarchar](40) NOT NULL,
	[Address] [nvarchar](60) NULL,
	[City] [nvarchar](20) NULL,
	[Phone] [nvarchar](24) NULL,
	[OwnerId] int DEFAULT CAST(SESSION_CONTEXT(N'UserId') AS int),
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
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
/****** Object:  Table [dbo].[Employees]    Script Date: 5/2/2017 8:52:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
	[BirthDate] [datetime] NULL,
	[HireDate] [datetime] NULL,
	[City] [nvarchar](20) NULL,
	[Phone] [nvarchar](24) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[id] ASC
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

GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 5/2/2017 8:52:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderId] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Number] [smallint] NOT NULL,
 CONSTRAINT [PK_Order_Details] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 5/2/2017 8:52:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Description] nvarchar(300),
	[CustomerID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[OrderDate] [datetime] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 5/2/2017 8:52:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[Number] [smallint] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
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
	[Assembly] [varbinary(max)] NOT NULL,
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

GO
ALTER TABLE [dbo].[OrderDetails] ADD  CONSTRAINT [DF_Order_Details_Quantity]  DEFAULT ((1)) FOR [Number]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_UnitPrice]  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_UnitsInStock]  DEFAULT ((0)) FOR [Number]
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
ALTER TABLE [dbo].[OrderDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_Details_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_Order_Details_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_Details_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_Order_Details_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH NOCHECK ADD  CONSTRAINT [FK_EmployeeId_Employees] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_EmployeeId_Employees]
GO
ALTER TABLE [dbo].[Orders]  WITH NOCHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
ALTER TABLE [dbo].[Products]  WITH NOCHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[Employees]  WITH NOCHECK ADD  CONSTRAINT [CK_Birthdate] CHECK  (([BirthDate]<getdate()))
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [CK_Birthdate]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH NOCHECK ADD  CONSTRAINT [CK_Quantity] CHECK  (([Number]>(0)))
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [CK_Quantity]
GO
ALTER TABLE [dbo].[Products]  WITH NOCHECK ADD  CONSTRAINT [CK_Products_UnitPrice] CHECK  (([Price]>=(0)))
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [CK_Products_UnitPrice]
GO
ALTER TABLE [dbo].[Products]  WITH NOCHECK ADD  CONSTRAINT [CK_UnitsInStock] CHECK  (([Number]>=(0)))
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [CK_UnitsInStock]
GO


var t = {
	"PrimaryTable": "Employees",
	"PrimaryKeys": [
	   "FirstName",
	   "LastName"
	],
	"Tables": [
		{
			"Name": "another table",
			"ForeignKeys": [
				{
					"ForeignKey": "LName",
					"To": "LastNameName",
				},
				{
					"ForeignKey": "FName",
					"To": "FirstName",
				}
			]			
		}
	]
}

create PROCEDURE setInitialContext(   
    @UserId int)
AS
	SET NOCOUNT ON;  
	declare @FullName nvarchar(150);
	declare @City nvarchar(20);
	declare @Phone nvarchar(24);
    select @FullName = FullName, @City = City, @Phone = Phone from Employees where id = @UserId
	EXEC sp_set_session_context 'Table', @Employees;  
	EXEC sp_set_session_context 'UserId', @UserId; 
	EXEC sp_set_session_context 'City', @City;  
	EXEC sp_set_session_context 'Phone', @Phone;  
GO   

EXEC sp_configure 'clr enabled', 1  
RECONFIGURE; 


drop function if exists getUserAccessClr
go
drop ASSEMBLY if exists Parser
go
create ASSEMBLY Parser FROM 'C:\repositories\security-policy\ClassLibrary1\ClassLibrary1\ClassLibrary1\Parser.dll';  
GO 
create FUNCTION dbo.getUserAccessClr(@Predicate nvarchar(4000)) RETURNS bit  
 AS EXTERNAL NAME Parser.ContextParser.ExecutePredicate;   
GO 

drop function if exists dbo.getUserAccess
go
CREATE FUNCTION dbo.getUserAccess(@TableName nvarchar(200), @Columns nvarchar(4000), @PredicateIds nvarchar(max))  
RETURNS bit
AS   
BEGIN
 Declare @result bit;
   if @predicatesIds is Null
     begin
       set @result = 1
     end
   else
     begin
       select @result = dbo.getUserAccessClr(@predicatesIds, @RowValue)
     end
  return @result	
END;
go
drop function if exists dbo.securityPredicateOrders
go
create FUNCTION dbo.securityPredicateOrders(@employeeID int, @customerID int)  
    RETURNS TABLE 
	 WITH SCHEMABINDING
AS  
    RETURN SELECT 1 as Resu where ((select dbo.getUserAccess('Orders',
    	CONCAT('EmployeeID="', @employeeID, '",CustomerId="', @customerID, '"')    	
    	)) = 1) 

go

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
print dbo.getColumnsAsString('Employees', 'dbo')


CREATE FUNCTION dbo.getUserPredicateIds(@TableName nvarchar(200))  
RETURNS nvarchar(max)
AS   
BEGIN
 DECLARE @predicatesIds nvarchar(4000);
 select @predicatesIds = COALESCE(@predicatesIds + ',', '') + dbo.Predicates.id from 
	 dbo.Predicates join  dbo.Policies 
	on  dbo.Predicates.id =  dbo.Policies.PredicateId 
	and  dbo.Predicates.TableName = @TableName
	join  dbo.EmployeeGroups 
	on  dbo.EmployeeGroups.GroupId =  dbo.Policies.GroupId
	and  dbo.EmployeeGroups.EmployeeId = CAST(SESSION_CONTEXT(N'UserId') AS int);

	
	  return @predicatesIds	
END;

drop function if exists dbo.securityPredicateEmployees
go
create FUNCTION dbo.securityPredicateEmployees(@FullName nvarchar(150), @City nvarchar(20), @Phone nvarchar(24))  
    RETURNS TABLE 
	 WITH SCHEMABINDING
AS  
    RETURN SELECT 1 as Resu where ((select dbo.getUserAccess('Employees',
    	CONCAT('FullName="', @FullName, '",City="', @City, '", Phone="', @Phone,'"')    	
    	)) = 1) 

go

drop function if exists dbo.securityPredicateCustomers
go
create FUNCTION dbo.securityPredicateCustomers(
@FullName nvarchar(50), @CompanyName nvarchar(40), @Address nvarchar(60), @City nvarchar(20), @Phone nvarchar(24), @OwnerId nvarchar(128))  
    RETURNS TABLE 
	 WITH SCHEMABINDING
AS  
    RETURN SELECT 1 as Resu where ((select dbo.getUserAccess(
    	'Customers', 
    	dbo.getColumnsAsString('Customers', 'dbo'), 
    	dbo.getUserPredicateIds('Customers'), 
    	@FullName, @CompanyName, @Address, @City, @Phone, @OwnerId))) = 1) 

go

drop security policy if exists dbo.[OrdersPolicy]   
create SECURITY POLICY dbo.[OrdersPolicy]   
ADD FILTER PREDICATE dbo.securityPredicateOrders(EmployeeID, CustomerId)   
    ON [dbo].[Orders]
WITH (STATE = ON); 

drop security policy if exists dbo.[EmployeesPolicy]   
create SECURITY POLICY dbo.[EmployeesPolicy]   
ADD FILTER PREDICATE dbo.securityPredicateEmployees(FullName, City, Phone)   
    ON [dbo].[Employees]
WITH (STATE = ON); 

drop security policy if exists dbo.[CustomersPolicy]   
create SECURITY POLICY dbo.[CustomersPolicy]   
ADD FILTER PREDICATE dbo.securityPredicateCustomers(FullName, CompanyName, Address, City, Phone, OwnerId)   
    ON [dbo].[Customers]
WITH (STATE = ON); 