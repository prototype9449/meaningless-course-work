using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var predicate1 = "UserId=\"1\", UserId1 = \"2\"";
            var rowValues1 = "UserId=\"1\", UserId1 = \"2\"";
            Console.WriteLine(ContextParser.ExecutePredicate(predicate1, rowValues1));

            var predicate2 = "City=\"Yar\", Phone = \"123\"";
            var rowValues2 = "Phone=\"123\", City = \"Moscow\"";
            Console.WriteLine(ContextParser.ExecutePredicate(predicate2, rowValues2));

            var predicate3 = "City=City, Phone = Phone";
            var rowValues3 = "Phone=\"123\", City = \"Moscow\"";
            Console.WriteLine(ContextParser.ExecutePredicate(predicate3, rowValues3));

            Console.ReadLine();
        }
    }
}
