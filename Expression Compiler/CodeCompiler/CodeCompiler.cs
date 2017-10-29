using System;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Dynamic;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Binary;

namespace dhx
{
	
	public class CodeCompiler
	{
		public CodeCompiler()
		{
		}

		public object ExecuteCode(string code, string namespacename, string classname,
			string functionname, bool isstatic, dynamic param1, dynamic param2)
		{
		    var compiledAssembly = BuildAssembly(code);

            var bytes = File.ReadAllBytes(compiledAssembly.Location);
            //File.Delete("myLib.dll");
		    var assembly = Assembly.Load(bytes);
			object returnval = null;
		    var asm = assembly;

            object instance = null;
			Type type = null;
			if (isstatic)
			{
				type = asm.GetType(namespacename + "." + classname);
			}
			else
			{
				instance = asm.CreateInstance(namespacename + "." + classname);
				type = instance.GetType();
			}
			MethodInfo method = type.GetMethod(functionname);
			returnval = method.Invoke(instance, new [] { param1, param2 });
			return returnval;
		}

		
		private Assembly BuildAssembly(string code)
		{
			Microsoft.CSharp.CSharpCodeProvider provider = new CSharpCodeProvider();
			ICodeCompiler compiler = provider.CreateCompiler();
			CompilerParameters compilerparams = new CompilerParameters();
			compilerparams.GenerateExecutable = false;
			compilerparams.GenerateInMemory = true;
		    compilerparams.ReferencedAssemblies.Add(typeof(DynamicObject).Assembly.Location);
		    compilerparams.ReferencedAssemblies.Add(typeof(DynamicDict.DynamicDict).Assembly.Location);
		    compilerparams.ReferencedAssemblies.Add(typeof(Dictionary<string, object>).Assembly.Location);
		    compilerparams.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            CompilerResults results = compiler.CompileAssemblyFromSource(compilerparams, code);
			if (results.Errors.HasErrors)
			{
				StringBuilder errors = new StringBuilder("Compiler Errors :\r\n");
				foreach (CompilerError error in results.Errors )
				{
					errors.AppendFormat("Line {0},{1}\t: {2}\n", error.Line, error.Column, error.ErrorText);
				}
				throw new Exception(errors.ToString());
			}
			else
			{
				return results.CompiledAssembly;
			}
		}
	}
}
