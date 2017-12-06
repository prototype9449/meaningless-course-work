
DELCARE @BoolType bit = 1
DELCARE @IntType int = 12345
DELCARE @StringType nvarchar(400) = ' fsdf sdf sdf sdfasdf sadf sad fsd fasdfs dfsdf adf sad f'
DELCARE @DateTimeType datetime = '01/01/2015 23:59:59.999'
DELCARE @DateTimeOffsetType datetimeoffset = '2012-10-25 12:24:32 +10:0'
DELCARE @TimeType time = '12:34:54.1237'
DELCARE @GuidType uniqueidentifier = '0E984725-C51C-4BF4-9960-E1C80E27ABA0wrong'


declare @i int
declare @rows_to_insert int
set @i = 0
set @rows_to_insert = 1

while @i < @rows_to_insert
    begin
    INSERT INTO dbo.Employees(BoolType, IntType, StringType, DateTimeType, DateTimeOffsetType, TimeType, GuidType) VALUES 
		(@BoolType, @IntType, @StringType, @DateTimeType, @DateTimeType, @DateTimeOffsetType, @TimeType, @GuidType)

    set @i = @i + 1
    end




insert into dbo.Groups(Name,Description) values
('BoolType row', ''),
('IntType row', '')
('StringType row', ''),
('DateTimeType row', ''),
('DateTimeOffsetType row', ''),
('TimeType row', ''),
('GuidType row', ''),

insert into dbo.Groups(Name,Description) values
('BoolType row and context', ''),
('IntType row and context', '')
('StringType row and context', ''),
('DateTimeType row and context', ''),
('DateTimeOffsetType row and context', ''),
('TimeType row and context', ''),
('GuidType row and context', ''),


insert into dbo.Groups(Name,Description) values
('row and context 7', ''),
('row and context 14', '')
('row and context 21', ''),
('row and context 28', '')

insert into dbo.EmployeeGroups(EmployeeId, GroupId) values
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 11),
(12, 12),
(13, 13),
(14, 14),
(15, 15),
(16, 16),
(17, 17),
(18, 18),
(19, 19),
(20, 20),
(21, 21),
(22, 22),

declare @condition7 nvarchar(1000) = 'R.BoolType = C.BoolType and R.IntType = C.IntType and R.StringType = C.StringType and R.DateTimeType = C.DateTimeType and R.DateTimeType = C.DateTimeType and R.GuidType = C.GuidType'

declare @condition14 nvarchar(1000) = CONCAT(@condition7, ' and ', @condition7)

declare @condition21 nvarchar(1000) = CONCAT(@condition7, ' and ', @condition7, ' and ', @condition7)

declare @condition28 nvarchar(1000) = CONCAT(@condition7, ' and ', @condition7, ' and ', @condition7, ' and ', @condition7)

insert into dbo.Predicates(TableName, Value) values
('dbo.TestTable', 'R.BoolType = true'),
('dbo.TestTable', 'R.IntType = 12345'),
('dbo.TestTable', 'R.StringType = "fsd fdsf sdf sdfsdf s"'),
('dbo.TestTable', 'R.DateTimeType = "01/01/2015" as datetime'),
('dbo.TestTable', 'R.DateTimeOffsetType = "4/3/2007 2:23:57 AM" as datetimeoffset'),
('dbo.TestTable', 'R.TimeType = "12:12:12" as timespan'),
('dbo.TestTable', 'R.GuidType = "0E984725-C51C-4BF4-9960-E1C80E27ABA0wrong" as guid'),

('dbo.TestTable', 'R.BoolType = C.BoolType'),
('dbo.TestTable', 'R.IntType = C.IntType'),
('dbo.TestTable', 'R.StringType = C.StringType'),
('dbo.TestTable', 'R.DateTimeType = C.DateTimeType'),
('dbo.TestTable', 'R.DateTimeOffsetType = C.DateTimeOffsetType'),
('dbo.TestTable', 'R.DateTimeType = C.DateTimeType'),
('dbo.TestTable', 'R.GuidType = C.GuidType'),

('dbo.TestTable', @condition7),
('dbo.TestTable', @condition14),
('dbo.TestTable', @condition21),
('dbo.TestTable', @condition28),

insert into dbo.Policies(GroupId, PredicateId) values
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 11),
(12, 12),
(13, 13),
(14, 14),
(15, 15),
(16, 16),
(17, 17),
(18, 18),
(19, 19),
(20, 20),
(21, 21),
(22, 22),

