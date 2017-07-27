using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Newtonsoft.Json.Linq;

namespace DotNetHarness
{
    class Program
    {
        static Program()
        {
            var envLogValue = Environment.GetEnvironmentVariable("DOTNET_HARNESS_LOG");
            _log = bool.TryParse(envLogValue, out bool shouldLog) ? shouldLog : false;
        }

        private static bool _log;
        private static void Log(string message)
        {
            if (_log)
                Console.WriteLine(message);
        }

        // dotnet harness assembly namespace.class method        
        static void Main(string[] args)
        {
            var assemblyName = args[0];
            var fullyQualifiedClassName = args[1];
            var methodName = args[2];

            var directory = Directory.GetCurrentDirectory();
            var assemblyPath = FindFile(directory, assemblyName);
            Log($"Loading assembly {assemblyPath}");

            var myAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
            var myType = myAssembly.GetType($"{fullyQualifiedClassName}");
            var myInstance = Activator.CreateInstance(myType);
            var method = myType.GetTypeInfo().GetMethod(methodName);

            var parameters = method.GetParameters();
            if (parameters.Length > 1) throw new Exception("dotnet harness does not currently support invocations with multiple parameters");

            var param = parameters[0];
            Log($"Method {method.Name} found with {parameters.Length} params with type {param.ParameterType}");

            Log($"Deserializing {args[3]} to {param.ParameterType}");

            var instance = JObject.Parse(args[3]).ToObject(param.ParameterType);

            method.Invoke(myInstance, new[] { instance });
        }

        static string FindFile(string directory, string assembly)
        {
            Log($"Looking for {assembly} in {directory}");
            foreach (string d in Directory.GetDirectories(directory))
            {
                var files = Directory.GetFiles(d, $"{assembly}.dll");
                Log($"Found {files.Length} files");
                foreach (var filePath in files)
                    return filePath;

                return FindFile(d, assembly);
            }

            throw new Exception("Could not find the assembly");
        }
    }
}
