using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Text;
using DynamicWrapper;
using Microsoft.CSharp;

namespace ExpressionBuilder
{
    public class Expressions
    {

        public static bool ExecuteExpression(byte[] expression, dynamic param1, dynamic param2)
        {
            var assembly = Assembly.Load(expression);
            Type type = assembly.GetType("PredicateExpression.Executer");
            MethodInfo method = type.GetMethod("Resolve");
            bool returnval = (bool)method.Invoke(null, new[] { param1, param2 });
            return returnval;
        }

        static string GetCode(string expression) =>
            @"
    namespace PredicateExpression
        { 
            public class Executer
            {
                public static bool Resolve(dynamic C, dynamic R)
                {
                    return " + expression + @";
                }
            }
        }";

        public static byte[] BuildAssemblyFromExpression(string expression)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            ICodeCompiler compiler = provider.CreateCompiler();
            CompilerParameters compilerparams = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = false,
                OutputAssembly = "myLib.dll"
            };

            compilerparams.ReferencedAssemblies.Add(typeof(DynamicObject).Assembly.Location);
            compilerparams.ReferencedAssemblies.Add(typeof(DynamicDictionary).Assembly.Location);
            compilerparams.ReferencedAssemblies.Add(typeof(Dictionary<string, object>).Assembly.Location);
            compilerparams.ReferencedAssemblies.Add("Microsoft.CSharp.dll");

            CompilerResults results = compiler.CompileAssemblyFromSource(compilerparams, GetCode(expression));

            if (results.Errors.HasErrors)
            {
                StringBuilder errors = new StringBuilder("Compiler Errors :\r\n");
                foreach (CompilerError error in results.Errors)
                {
                    errors.AppendFormat("Line {0},{1}\t: {2}\n", error.Line, error.Column, error.ErrorText);
                }
                throw new Exception(errors.ToString());
            }
            else
            {
                var bytes = File.ReadAllBytes(results.CompiledAssembly.Location);
                return bytes;
            }
        }
    }
}
