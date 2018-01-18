using System;
using SqlParser;

namespace PolicyTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=CO-YAR-WS208;Initial Catalog=OnlineShop;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
            var currentTable = "dbo.Orders";
            var userTable = "dbo.Employees";
            var rowIdentifierKeys = "[id][1][int]";
            var userIdentifierKeys = "[id][1][int]";
            var expressions = "12.4 / 2 = 1.24";


            //ContextParser.ConnectionString = connectionString;
            //var result1 = ContextParser.ExecuteStaticPredicate(expressions, 1, true, 1, "fdfd", DateTime.Now, DateTimeOffset.MinValue, TimeSpan.MinValue, new Guid(),
            //    1, true, 1, "fdfd", DateTime.Now, DateTimeOffset.MinValue, TimeSpan.MinValue, new Guid());

            var result = ContextParser.ExecutePredicate(currentTable, userTable, expressions, rowIdentifierKeys,
                userIdentifierKeys);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
