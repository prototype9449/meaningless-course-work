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
            Console.WriteLine(ContextParser.ExecutePredicate("UserId=\"1\", UserId1 = \"2\""));
        }
    }
}
