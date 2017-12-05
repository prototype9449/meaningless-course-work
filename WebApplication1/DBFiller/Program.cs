using System;
using System.Collections.Generic;
using System.Linq;

namespace DBFiller
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = args.ToList();
            var dbStricture = new DBStructure
            {
                EmployeeNumber = GetNumber(arguments, "employees"),
                CustomerNumber = GetNumber(arguments, "customers"),
                OrderNumber = GetNumber(arguments, "orders"),
                GroupNumber = GetNumber(arguments, "groups"),
                OrderDetailNumber = GetNumber(arguments, "orderdetails"),
                PolicyNumber = GetNumber(arguments, "policies"),
                EmployeeGroupNumber = GetNumber(arguments, "employeegroups"),
                CategoryNumber = GetNumber(arguments, "categories")
            };


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
