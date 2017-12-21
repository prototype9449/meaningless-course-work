using System;
using SqlParser;

namespace PolicyTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=HOME;Initial Catalog=Test_10_000;Integrated Security=True";
            var currentTable = "dbo.TestTable";
            var userTable = "dbo.Employees";
            var rowIdentifierKeys = "[id][1][int]";
            var userIdentifierKeys = "[id][1][int]";
            var expressions = "R.BoolType = true as bool";


            ContextParser.ConnectionString = connectionString;
            var result1 = ContextParser.ExecuteStaticPredicate(expressions, 1, true, 1, "fdfd", DateTime.Now, DateTimeOffset.MinValue, TimeSpan.MinValue, new Guid(),
                1, true, 1, "fdfd", DateTime.Now, DateTimeOffset.MinValue, TimeSpan.MinValue, new Guid());

            var result = ContextParser.ExecutePredicate(currentTable, userTable, expressions, rowIdentifierKeys,
                userIdentifierKeys);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
