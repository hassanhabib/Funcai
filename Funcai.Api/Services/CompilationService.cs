using Funcai.Api.Models;
using Microsoft.CodeDom.Providers.DotNetCompilerPlatform;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace Funcai.Api.Services
{
    public class CompilationService
    {
        public List<string> ExecuteRequest(Request request, List<string> nativeValues)
        {
            string code = CodeBuilder(request);
            var asm = CompileSourceCodeDom(code);
            return (List<string>)ExecuteFromAssembly(asm, request.Parameters, nativeValues);
        }

        public static string CodeBuilder(Request request)
        {
            string usings = string.Empty;
            request.Usings.ForEach(@using =>
            {
                usings += "using " + @using + ";";
            });
            string functionStr = $"{usings} public class Foo {{ public List<string> MyFunc(";
            request.Parameters.ForEach(p => functionStr += $" {p.Type} {p.Name},");
            functionStr += $"List<string> values){{{request.Code}}}}}";

            return functionStr;
        }

        private static Assembly CompileSourceCodeDom(string sourceCode)
        {
            CodeDomProvider cpd = new CSharpCodeProvider();
            var cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("System.dll");
            cp.GenerateExecutable = false;
            CompilerResults cr = cpd.CompileAssemblyFromSource(cp, sourceCode);

            return cr.CompiledAssembly;
        }

        private static object ExecuteFromAssembly(Assembly assembly, List<InputParameter> parameters, List<string> nativeValues)
        {
            parameters.Add(new InputParameter
            {
                Value = nativeValues
            });

            Type fooType = assembly.GetType("Foo");
            MethodInfo printMethod = fooType.GetMethod("MyFunc");
            object foo = assembly.CreateInstance("Foo");
            return printMethod.Invoke(foo, BindingFlags.InvokeMethod, null, parameters.Select(p => p.Value).ToArray(), CultureInfo.CurrentCulture);
        }
    }
}