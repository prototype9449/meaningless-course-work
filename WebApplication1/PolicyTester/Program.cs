using System;
using SqlParcer;

namespace PolicyTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=HOME;Initial Catalog=NewOnlineShop;Integrated Security=True";
            var currentTable = "dbo.Orders";
            var userTable = "dbo.Employees";
            var rowIdentifierKeys = "[id][1][int]";
            var userIdentifierKeys = "[id][1][int]";
            var expressions = "R.EmployeeID = C.id and R.CustomerID = 1";

            var r = 12.2 / 10;

            ContextParcer.ConnectionString = connectionString;
            var result = ContextParcer.ExecutePredicate(currentTable, userTable, expressions, rowIdentifierKeys,
                userIdentifierKeys);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
