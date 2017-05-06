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
	[OwnerId] nvarchar(128) DEFAULT CAST(SESSION_CONTEXT(N'UserId') AS nvarchar(128)),
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
	[CategoryId] [int] NULL,
	[Price] [money] NULL,
	[Number] [smallint] NULL,
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
	[TableName] [nvarchar](100) not null,
 CONSTRAINT [PK_Predicates] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO

ALTER TABLE [dbo].[Policies]  WITH CHECK ADD  CONSTRAINT [FK_Policies_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([id])
GO

ALTER TABLE [dbo].[Policies] CHECK CONSTRAINT [FK_Policies_Groups]
GO

ALTER TABLE [dbo].[Policies]  WITH CHECK ADD  CONSTRAINT [FK_Policies_Predicates] FOREIGN KEY([PredicateId])
REFERENCES [dbo].[Predicates] ([id])
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
REFERENCES [dbo].[Employees] ([id])
GO
ALTER TABLE [dbo].[EmployeeGroups] CHECK CONSTRAINT [FK_EmployeeId1_Employees]
GO
ALTER TABLE [dbo].[EmployeeGroups]  WITH NOCHECK ADD  CONSTRAINT [FK_GroupId_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([id])
GO
ALTER TABLE [dbo].[EmployeeGroups] CHECK CONSTRAINT [FK_GroupId_Groups]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_Details_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_Order_Details_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_Details_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_Order_Details_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH NOCHECK ADD  CONSTRAINT [FK_EmployeeId_Employees] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_EmployeeId_Employees]
GO
ALTER TABLE [dbo].[Orders]  WITH NOCHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
ALTER TABLE [dbo].[Products]  WITH NOCHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([id])
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


create PROCEDURE setInitialContext(   
    @UserId int)
AS
	SET NOCOUNT ON;  
	declare @FullName nvarchar(150);
	declare @City nvarchar(20);
	declare @Phone nvarchar(24);
    select @FullName = FullName, @City = City, @Phone = Phone from Employees where id = @UserId
	EXEC sp_set_session_context 'FullName', @FullName;  
	EXEC sp_set_session_context 'UserId', @UserId; 
	EXEC sp_set_session_context 'City', @City;  
	EXEC sp_set_session_context 'Phone', @Phone;  
GO   

drop function if exists [Security].getUserAccessClr
drop ASSEMBLY if exists Parser
create ASSEMBLY Parser FROM 'C:\Users\Сергей\Documents\Visual Studio 2015\Projects\ClassLibrary1\ClassLibrary1\Parser.dll';  
GO 
create FUNCTION [Security].getUserAccessClr(@Predicate nvarchar(50)) RETURNS bit  
 AS EXTERNAL NAME Parser.ContextParser.ExecutePredicate;   
GO  

CREATE FUNCTION [Security].getUserAccess(@TableName nvarchar(50))  
RETURNS bit   
AS   


drop function if exists getUserAccess
go
CREATE FUNCTION getUserAccess(@TableName nvarchar(50))  
RETURNS bit
 WITH SCHEMABINDING   
AS   
-- Returns the stock level for the product.  
BEGIN
 DECLARE @predicates nvarchar(4000);
 Declare @result bit;
 select @predicates = COALESCE(@predicates + ',', '') + dbo.Predicates.Value from 
	 dbo.Predicates join  dbo.Policies 
	on  dbo.Predicates.id =  dbo.Policies.PredicateId 
	and  dbo.Policies.TableName = @TableName
	join  dbo.EmployeeGroups 
	on  dbo.EmployeeGroups.GroupId =  dbo.Policies.GroupId
	and  dbo.EmployeeGroups.EmployeeId = CAST(SESSION_CONTEXT(N'UserId') AS int);

	if @predicates is Null
	  begin
	    set @result = 1
	  end
	else
	  begin
	    select @result = dbo.getUserAccessClr(@predicates)
	  end
	  return @result	
END;

drop function if exists getUserAccessClr
go
drop ASSEMBLY if exists Parser
go
create ASSEMBLY Parser FROM 'C:\Users\Сергей\Documents\Visual Studio 2015\Projects\ClassLibrary1\ClassLibrary1\Parser.dll';  
GO 
create FUNCTION getUserAccessClr(@Predicate nvarchar(50)) RETURNS bit  
 AS EXTERNAL NAME Parser.ContextParser.ExecutePredicate;   
GO  

drop function if exists [securityPredicate]
go
create FUNCTION securityPredicateOrders(@emId int)  
    RETURNS TABLE 
	 WITH SCHEMABINDING
AS  
    RETURN SELECT 1 as Resu where ((select dbo.getUserAccess('Orders')) = 1) 

go
create SECURITY POLICY dbo.[OrdersPolicy]   
ADD FILTER PREDICATE dbo.securityPredicateOrders(EmployeeId)   
    ON [dbo].[Orders]
WITH (STATE = ON);   