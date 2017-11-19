using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlParcer;

namespace PolicyTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=HOME;Initial Catalog=NewOnlineShop;Integrated Security=True";
            var currentTable = "Orders";
            var userTable = "dbo.Employees";
            var rowIdentifierKeys = "[id][1][System.Int32]";
            var userIdentifierKeys = "[id][1][System.Int32]";
            var expressions = "R.EmployeeID=C.id,R.CustomerID=1";

            ContextParcer.ConnectionString = connectionString;
            var result = ContextParcer.ExecutePredicate(currentTable, userTable, expressions, rowIdentifierKeys,
                userIdentifierKeys);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
