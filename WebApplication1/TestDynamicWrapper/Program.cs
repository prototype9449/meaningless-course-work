using System;
using DynamicWrapper;
using ExpressionBuilder;

namespace TestDynamicWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic context = new DynamicDictionary();
            dynamic row = new DynamicDictionary();
        
            context.SetValue("name1", 12121612);
            row.SetValue("name2", 1331231);

            var expression = "C.name1 > R.name2";

            var bytes = Expressions.BuildAssemblyFromExpression(expression);

            var result = Expressions.ExecuteExpression(bytes, context, row);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
