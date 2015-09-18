using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Text;
using System.Reflection;

namespace SIS.Evaluator {
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Evaluator {
		#region Construction
		public Evaluator(EvaluatorItem[] items) {
			ConstructEvaluator(items);
		}

		public Evaluator(Type returnType, string expression, string name) {
			EvaluatorItem[] items = { new EvaluatorItem(returnType, expression, name) };
			ConstructEvaluator(items);
		}

		public Evaluator(EvaluatorItem item) {
			EvaluatorItem[] items = { item };
			ConstructEvaluator(items);
		}

		private void ConstructEvaluator(EvaluatorItem[] items) {
			CSharpCodeProvider comp = new CSharpCodeProvider();
			CompilerParameters cp = new CompilerParameters();
			cp.ReferencedAssemblies.Add("system.dll");
			cp.ReferencedAssemblies.Add("system.data.dll");
			cp.ReferencedAssemblies.Add("system.xml.dll");
			cp.GenerateExecutable = false;
			cp.GenerateInMemory = true;

			StringBuilder code = new StringBuilder();
			code.Append("using System; \n");
			code.Append("using System.Data; \n");
			code.Append("using System.Data.SqlClient; \n");
			code.Append("using System.Data.OleDb; \n");
			code.Append("using System.Xml; \n");
			code.Append("namespace ADOGuy { \n");
			code.Append("  public class _Evaluator { \n");
			foreach (EvaluatorItem item in items) {
				code.AppendFormat("    public {0} {1}() ",
								  item.ReturnType.Name,
								  item.Name);
				code.Append("{ ");
				code.AppendFormat("      return ({0}); ", item.Expression);
				code.Append("}\n");
			}
			code.Append("} }");

			CompilerResults cr = comp.CompileAssemblyFromSource(cp, code.ToString());
			cp.ReferencedAssemblies.Clear();
			cp = null;
			code.Clear();
			code = null;
			if (cr.Errors.HasErrors) {
				StringBuilder error = new StringBuilder();
				error.Append("Error Compiling Expression: ");
				foreach (CompilerError err in cr.Errors) {
					error.AppendFormat("{0}\n", err.ErrorText);
				}
				throw new Exception("Error Compiling Expression: " + error.ToString());
			}
			Assembly a = cr.CompiledAssembly;
			_Compiled = a.CreateInstance("ADOGuy._Evaluator");
			comp = null;
			cp = null;
			cr = null;
			a = null;
			for (int i = 0; i < items.Length; i++) {
				items[i] = null;
			}
			items = null;
		}

		#endregion

		#region Public Members
		public double EvaluateDouble(string name) {
			return (double)Evaluate(name);
		}

		public int EvaluateInt(string name) {
			return (int)Evaluate(name);
		}

		public string EvaluateString(string name) {
			return (string)Evaluate(name);
		}

		public bool EvaluateBool(string name) {
			return (bool)Evaluate(name);
		}

		public object Evaluate(string name) {
			MethodInfo mi = _Compiled.GetType().GetMethod(name);
			object result=mi.Invoke(_Compiled, null);
			_Compiled = null;
			mi = null;
			return result;
		}
		#endregion

		#region Static Members
		static public double EvaluateToDouble(string code) {
			Evaluator eval = new Evaluator(typeof(double), code, staticMethodName);
			return (double)eval.Evaluate(staticMethodName);
		}

		static public int EvaluateToInteger(string code) {
			Evaluator eval = new Evaluator(typeof(int), code, staticMethodName);
			return (int)eval.Evaluate(staticMethodName);
		}

		static public string EvaluateToString(string code) {
			Evaluator eval = new Evaluator(typeof(string), code, staticMethodName);
			return (string)eval.Evaluate(staticMethodName);
		}

		static public bool EvaluateToBool(string code) {
			Evaluator eval = new Evaluator(typeof(bool), code, staticMethodName);
			bool result = (bool)eval.Evaluate(staticMethodName);
			eval = null;
			return result;
		}

		static public object EvaluateToObject(string code) {
			Evaluator eval = new Evaluator(typeof(object), code, staticMethodName);
			return eval.Evaluate(staticMethodName);
		}
		#endregion

		#region Private
		const string staticMethodName = "__foo";
		Type _CompiledType = null;
		object _Compiled = null;
		#endregion
	}

	public class EvaluatorItem {
		public EvaluatorItem(Type returnType, string expression, string name) {
			ReturnType = returnType;
			Expression = expression;
			Name = name;
		}

		public Type ReturnType;
		public string Name;
		public string Expression;
	}
}
