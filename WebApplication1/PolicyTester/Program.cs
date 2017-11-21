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
            var rowIdentifierKeys = "[id][1][System.Int32]";
            var userIdentifierKeys = "[id][1][System.Int32]";
            var expressions = "R.EmployeeID=C.id,R.CustomerID=1";


            var engine = new Engine();
            
            engine.SetStatic(typeof(DateTime)).WithName("mm");
            var result1 = engine.Interpret("mm.create().maxValue.toString()");

            ContextParcer.ConnectionString = connectionString;
            var result = ContextParcer.ExecutePredicate(currentTable, userTable, expressions, rowIdentifierKeys,
                userIdentifierKeys);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
