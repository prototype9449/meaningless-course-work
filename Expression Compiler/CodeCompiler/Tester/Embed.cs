using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.IO;
using System.Diagnostics;

namespace dhx 
{
	public sealed class Embed 
	{
		// Init is the only public callable method of this class.  It should be called once
		// (and only once!)
		// to register the LoadComponentAssembly method as the event handler to call should the
		// AssemblyResolve event get fired.
		public static void Init()
		{
			ResolveEventHandler loadAssembly = new ResolveEventHandler(LoadComponentAssembly);
			AppDomain.CurrentDomain.AssemblyResolve += loadAssembly;
		}

		// This method is called when an assembly fails to load via probing path, GAC, etc.
		// Note that we return null to the CLR if we have no assembly to load.  This is
		// expected behavior, so that the CLR may notify the rest of the application that
		// it could not load the assembly in question.
		static Assembly LoadComponentAssembly(Object sender, ResolveEventArgs args) 
		{
			// We'll use this reference fairly often in the future...
			Assembly assembly = Assembly.GetExecutingAssembly();
                 
			// Get the requested assembly's simple name (no namespace info or file extension)
			string simpleName = args.Name.Substring(0, args.Name.IndexOf(',') );
			string dllImageResourceName = getResourceLibName( simpleName, assembly );
			return streamFromResource(dllImageResourceName, assembly);
		}

		private static string getResourceLibName(string simpleLibName)
		{
			return getResourceLibName( simpleLibName, Assembly.GetExecutingAssembly() );
		}

		// We will go through the list of resources in the assembly and using the
		// simpleLibName, we will find if the dll resource is embedded in the assembly
		// Note that we return null on purpose if we didn't find anything.
		// This is because we also want to return null to the CLR if we have no assembly to load.
		private static string getResourceLibName(string simpleLibName, Assembly assembly) 
		{
			if ( simpleLibName == null || assembly == null )
				return null;

			simpleLibName += ".dll"; // assume that the file ends in this extension.
			string dllImageResourceName = null;

			// We will iterate through the list of resources in this assembly,
			// looking for the name of the assembly that failed to load from disk
			foreach (string resourceName in assembly.GetManifestResourceNames()) 
			{
				if (resourceName.Length < simpleLibName.Length) continue;

				// if the simpleName and resourceName end the same (we drop namespace info here),
				// then this should be the embedded assembly that we are looking for.
				if (String.Compare(simpleLibName, 0, resourceName, (resourceName.Length - simpleLibName.Length),
                    simpleLibName.Length, true) == 0 ) 
				{
					dllImageResourceName = resourceName;
				}
			}          

			return dllImageResourceName; 
		}

		private static Assembly streamFromResource(string dllImageResourceName)
		{
			return streamFromResource(dllImageResourceName, Assembly.GetExecutingAssembly() );
		}

		// this is the 'workhorse' of the class.  Once we've got a resource name in the assembly,
		// we stream the resource to a byte array, and load the Assembly from the byte array.
		private static Assembly streamFromResource(string dllImageResourceName, Assembly assembly)
		{
			if ( dllImageResourceName == null || assembly == null )
				return null;

			Stream imageStream;
			imageStream = assembly.GetManifestResourceStream(dllImageResourceName);
			long bytestreamMaxLength = imageStream.Length;

			byte[] buffer = new byte[bytestreamMaxLength];
			imageStream.Read(buffer,0,(int)bytestreamMaxLength);

			return  AssemblyBuilder.Load(buffer);
		}
	}
}