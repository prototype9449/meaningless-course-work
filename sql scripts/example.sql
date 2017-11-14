USE [OnlineShop]
GO

/****** Object:  Table [dbo].[Policies]    Script Date: 06.05.2017 11:54:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Policies](
	[id] [nvarchar](50) NOT NULL,
	[GroupId] [int] NOT NULL,
	[PredicateId] [int] NOT NULL,
	[TableName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Policies] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Policies]  WITH CHECK ADD  CONSTRAINT [FK_Policies_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([id])
GO

ALTER TABLE [dbo].[Policies]  WITH CHECK ADD  CONSTRAINT [FK_Policies_Predicates] FOREIGN KEY([PredicateId])
REFERENCES [dbo].[Predicates] ([id])
GO

ALTER TABLE [dbo].[Policies] CHECK CONSTRAINT [FK_Policies_Groups]
GO

ALTER TABLE [dbo].[Policies] CHECK CONSTRAINT [FK_Policies_Predicates]
GO


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
	and  dbo.Predicates.TableName = @TableName
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
	EXEC sp_set_session_context 'UserId', '2'; 

	select dbo.getUserAccessClr('City = "Vologda"')
	select SESSION_CONTEXT(N'UserId') as UserId

drop function if exists getUserAccessClr
drop ASSEMBLY if exists Parser
create ASSEMBLY Parser FROM 'C:\Users\Сергей\Documents\Visual Studio 2015\Projects\ClassLibrary1\ClassLibrary1\Parser.dll';  
GO 
create FUNCTION getUserAccessClr(@Predicate nvarchar(50)) RETURNS bit  
 AS EXTERNAL NAME Parser.ContextParser.ExecutePredicate;   
GO  

 select dbo.getUserAccess('Orders')

   select SESSION_CONTEXT(N'UserId') as UserId, SESSION_CONTEXT(N'UserId1') as UserId1


EXEC sp_configure 'clr enabled', 1  
RECONFIGURE;

 EXEC sp_set_session_context 'UserId', '1';  
 EXEC sp_set_session_context 'UserId1', '2'; 

declare @r bit
SELECT @r = dbo.getUserAccessClr('Phone = "9056312939"');  
print(@r)
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

EXECUTE  setInitialContext 2

drop function if exists securityPredicateOrders
go
create FUNCTION securityPredicateOrders(@emId int)  
    RETURNS TABLE 
	 WITH SCHEMABINDING
AS  
    RETURN SELECT 1 as Resu where ((select dbo.getUserAccess('Orders')) = 1) 

go
drop SECURITY Policy dbo.[OrdersPolicy]   
create SECURITY POLICY dbo.[OrdersPolicy]   
ADD FILTER PREDICATE dbo.securityPredicateOrders(EmployeeId)   
    ON [dbo].[Orders]
WITH (STATE = ON);  


select SESSION_CONTEXT(N'City')

EXECUTE  setInitialContext 2
select SESSION_CONTEXT(N'UserId')
select SESSION_CONTEXT(N'Phone')
select dbo.getUserAccess('Orders')
select * from Orders



 Declare @result bit;
 DECLARE @predicates nvarchar(4000);
 select @predicates = COALESCE(@predicates + ',', '') + dbo.Predicates.Value from 
	dbo.EmployeeGroups join dbo.Policies on
	dbo.EmployeeGroups.GroupId = dbo.Policies.GroupId
	and dbo.EmployeeGroups.EmployeeId = CAST(SESSION_CONTEXT(N'UserId') AS int)
	join dbo.Predicates on  dbo.Predicates.id =  dbo.Policies.PredicateId 
	and  dbo.Predicates.TableName = 'Orders'
		
	print(@predicates)


	select Predicates.Value from 
	dbo.EmployeeGroups join dbo.Policies on
	dbo.EmployeeGroups.GroupId = dbo.Policies.GroupId
	join dbo.Predicates on dbo.Predicates.TableName = 'Orders'
	and dbo.EmployeeGroups.EmployeeId = CAST(SESSION_CONTEXT(N'UserId') AS int)
	