using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFiller
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = args.ToList();
            var dbStricture = new DBStructure();
            dbStricture.EmployeeNumber = GetNumber(arguments, "employees");
            dbStricture.CustomerNumber = GetNumber(arguments, "customers");
            dbStricture.OrderNumber = GetNumber(arguments, "orders");
            dbStricture.GroupNumber = GetNumber(arguments, "groups");
            dbStricture.OrderDetailNumber = GetNumber(arguments, "orderdetails");
            dbStricture.PolicyNumber = GetNumber(arguments, "policies");
            dbStricture.EmployeeGroupNumber = GetNumber(arguments, "employeegroups");
            dbStricture.CategoryNumber = GetNumber(arguments, "categories");


            new Filler(dbStricture).FillDB();
            Console.ReadKey();
        }

        static int GetNumber(List<string> args, string key)
        {
            var index = args.IndexOf(key);
            if(index == -1 || args.Count == index + 1)
            {
                return 0;
            }

            int number;
            if(!int.TryParse(args[index + 1], out number))
            {
                throw new Exception(String.Format("impossibly to convert {0} into int", args[index + 1]));
            }

            return number;
        }
    }
}
