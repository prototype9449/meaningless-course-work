using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlParcer;
using Mages.Core;

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
            var expressions = "R.EmployeeID = C.id,R.CustomerID = 1";

            var expres = " 12 as int > 10 and 10 < 8 as int + 1 or true";
            var tokens = ReversePolishNotation.GetTokens(expres);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>()
            {
                {"f", new SqlResult(true, typeof(bool))},
                {"r", new SqlResult(false, typeof(bool))},
                {"g", new SqlResult(true, typeof(bool))}
            });


            ContextParcer.ConnectionString = connectionString;
            var result = ContextParcer.ExecutePredicate(currentTable, userTable, expressions, rowIdentifierKeys,
                userIdentifierKeys);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
