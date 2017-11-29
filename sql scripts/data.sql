INSERT INTO dbo.Employees(FullName, BirthDate, HireDate, City, Phone) VALUES 
('John D', '01/01/2008 23:59:59.999', '01/01/2015 23:59:59.999', 'Yar', '89056312980'),
('Mike A', '01/01/2007 23:59:59.999', '02/02/2014 23:59:59.999', 'Mos', '89056314330'),
('Tako A', '04/04/2005 23:59:59.999', '05/10/2016 23:59:59.999', 'Kaz', '89056314340')


INSERT INTO dbo.Customers(FullName, CompanyName, Address, City, Phone, OwnerId) VALUES 
('John Y', 'Confirmit', 'Something in Yar', 'Yar', '89056312980', 1),
('Mike J', 'Akvelon', 'Something in Mos', 'Mos', '89056213120', 1),
('Doll H', 'Tenzor', 'Something in Kaz', 'Kaz', '89056319555', 2)

INSERT INTO dbo.Orders(Description, CustomerID, EmployeeID, OrderDate) VALUES 
('A lot of things', 1, 1, '01/01/2009 23:59:59.992'),
('A lot of other things', 2, 2, '01/01/2010 23:59:59.992'),
('A lot of things', 3, 1, '01/01/2012 23:59:59.992')

insert into dbo.Categories(CategoryName, Description) values
('Taste products', 'big description asa as as as asa sa'),
('Worst products', 'blg asasasa sas as asa sa sa ')

INSERT INTO dbo.Products(Name, CategoryId, Price, Number) VALUES 
('First produce', 1, 130.12, 10),
('Second product', 2, 100.12, 20),
('Third product', 1, 95.12, 30)


INSERT INTO dbo.OrderDetails(OrderId, ProductID, Number) VALUES 
(1, 1, 4),
(2, 2, 5),
(3, 3, 3)

insert into dbo.Groups(Name,Description) values
('Yaroslavl', 'Group frm yar'),
('Moscow', 'Group from Moscow'),
('Kazan', 'Group from Kazan')

insert into dbo.EmployeeGroups(EmployeeId, GroupId) values
(1, 1),
(2, 2),
(3, 3)

insert into dbo.Predicates(TableName, Value) values
('dbo.Orders', 'R.EmployeeID = C.id and R.CustomerID = 1'),
('dbo.Employees', 'R.City = C.City'),
('dbo.Customers', 'C.id = R.OwnerId')

insert into dbo.Policies(GroupId, PredicateId) values
(1, 1),
(1, 2),
(1, 3)