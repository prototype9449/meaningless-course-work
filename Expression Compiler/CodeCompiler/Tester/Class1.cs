using System;

namespace dhx
{
    class Class1
    {
        static bool dyn(dynamic t1, dynamic t2)
        {
            var t3 = "1212";
            var t4 = "1231231";
            return t3.IndexOf("12") != -1;
        }

        static string getCode(string expression) =>
            @"namespace test
    { 
        public class Test
        {
            public static bool Compare(dynamic C, dynamic R)
            {
                return " + expression + @";
            }
        }
    }";

        [STAThread]
        static void Main(string[] args)
        {
            var expression = "C.name1.IndexOf(\"12\") != -1 && R.name2.IndexOf(\"13\") != -1";
            var code = getCode(expression);

            CodeCompiler cc = new CodeCompiler();
            dynamic t1 = new DynamicDict.DynamicDict();
            dynamic t2 = new DynamicDict.DynamicDict();

            t1.SetValue("name1", "121212");
            t2.SetValue("name2", "1331231");

            bool o = cc.ExecuteCode(code, "test", "Test", "Compare", true, t1, t2);

            Console.WriteLine(o);

            //Embed.Init();
            //Eval2();
        }
    }
}
