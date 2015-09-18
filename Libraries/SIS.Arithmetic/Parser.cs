using System;
using System.Collections; 
using System.Reflection;


namespace SIS.Arithmetic
{

	public delegate void FunctionHandler(object sender,FunctionEventArgs e);
	public delegate void ErrorHandler(object sender,ErrorEventArgs e);

	public class Parser
	{
		// 枚举计算单元类型. 
		enum Types { NONE, DELIMITER, VARIABLE,FUNCTION, NUMBER, STRING	}; 

		string exp;	   // 计算表达式 
		int	expIdx;	   // 当前表达式解析位置	
		string token;  // 当前记号
		Types tokType; // 当前记号类型
		string FunctionName; //当前函数名称
		int FunctionIndex;  //当前函数序号
		ArrayList FunctionParameters; //当前函数参数
		MethodInfo[] mm; //内部函数信息

		Object FunctionResult; //函数结果
		SortedList InterFunctions; // 内部函数集合
		SortedList ExterFunctions; // 外部函数集合

		InterFunction InterF;

		public event FunctionHandler CustomFunction;
		public event ErrorHandler OnError;
		
		//构造函数
		public Parser() 
		{
			FunctionResult = null;
			InterF = new InterFunction(); 
			InterFunctions = new SortedList();
			ExterFunctions = new SortedList(); 
			//添加内部函数
			GetInterFunctions();
		}
		// 解析器程序入口. 
		public object Evaluate(string expstr) 
		{ 
			object result=null;	
   
			exp	= expstr; 
			expIdx = 0;	 
  
			try	
			{  
				GetToken();	
				if(tokType == Types.NONE && token == "") throw new ParserException("No Expression Present!") ;
 
				EvalExp2(out result); 
 
				if(token !=	"")	throw new ParserException("Syntax Error!") ;
				return result;
 
			} 
			catch (ParserException exc)	
			{ 
				FireOnError(exc.Message,1);	
				return null; 
			} 
		} 
   
		// 输入外部函数计算值
		public void CFSetResult(object result)
		{
			FunctionResult = result;
		}

		// 添加外部函数
		public void AddCustomFunction(string FunctionName,int ParmCnt)
		{
			try
			{
				if(InterFunctions.IndexOfKey(FunctionName) != -1) throw new Exception();
				ExterFunctions.Add(FunctionName,ParmCnt);
			}
			catch
			{
				FireOnError("Add CustomFunction Error",3);
			}
		}
		//取double参数
		public double GetDoubleParameter(int ParamIndex)
		{
			try
			{
				if(ParamIndex >= FunctionParameters.Count || ParamIndex < 0) throw new Exception(); 
				if(FunctionParameters[ParamIndex].GetType().ToString() != "System.Double") throw new Exception(); 
				return (double)FunctionParameters[ParamIndex];
			}
			catch
			{
				FireOnError("Get Double Parameter Error!",2);
				return 0.0;
			}
		}
		//取string参数
		public string GetStringParameter(int ParamIndex)
		{
			try
			{
				if(ParamIndex >= FunctionParameters.Count || ParamIndex < 0) throw new Exception(); 
				if(FunctionParameters[ParamIndex].GetType().ToString() != "System.String") throw new Exception(); 
				return FunctionParameters[ParamIndex].ToString() ;
			}
			catch
			{
				FireOnError("Get Double Parameter Error!",2);
				return null;
			}
		}
		//取参数信息
		public string GetParameterInfo(int ParamIndex)
		{
			try
			{
				if(ParamIndex >= FunctionParameters.Count || ParamIndex < 0) throw new Exception(); 
				return FunctionParameters[ParamIndex].GetType().ToString();
			}
			catch
			{
				FireOnError("Get Parameter Information Error!",2);
				return null;
			}
		}

		// 取计算单元标记 
		void GetToken()	
		{ 
			tokType	= Types.NONE; 
			token =	"";	
	
			if(expIdx == exp.Length) return; //	到了表达式末端 

			// 忽略前置空格	
			while(expIdx < exp.Length && Char.IsWhiteSpace(exp[expIdx])) expIdx++; 
 
			if(expIdx == exp.Length) return; //	到了表达式末端
 
			if(IsDelim(exp[expIdx])) 
			{ // 分隔符	
				token += exp[expIdx]; 
				expIdx++; 
				tokType	= Types.DELIMITER;
				return;
			} 
			if(Char.IsLetter(exp[expIdx])) 
			{ // 函数名
				token += exp[expIdx];
				expIdx++;
				if(expIdx == exp.Length) return;
				while(Char.IsLetter(exp[expIdx]) ||	Char.IsDigit(exp[expIdx])) 
				{ 
					token += exp[expIdx]; 
					expIdx++; 
					if(expIdx == exp.Length) break;	
				} 
				tokType	= Types.FUNCTION ; 
				return;
			} 
			if(Char.IsDigit(exp[expIdx])) 
			{ // 数字 
				while(!IsDelim(exp[expIdx])) 
				{ 
					token += exp[expIdx]; 
					expIdx++; 
					if(expIdx >= exp.Length) break;	
				} 
				tokType	= Types.NUMBER;	
				return;
			} 
			if(exp[expIdx] == '"')
			{
				expIdx++;
				while(exp[expIdx] != '"')
				{
					token +=exp[expIdx];
					expIdx++; 
					if(expIdx >	exp.Length)	break; 
				}
				expIdx++;
				tokType	= Types.STRING;
				return;
			}
		} 
   
		// 检验是否指定分隔符.	
		bool IsDelim(char c) 
		{ 
			if((" +-/*&(),".IndexOf(c) != -1)) 
				return true; 
			return false; 
		}

		// 处理加法或减法 
		void EvalExp2(out object result) 
		{ 
			string op; 
			object partialResult = null; 
			
			EvalExp3(out result); 
			while((op =	token) == "+" || op	== "-" || op ==	"&") 
			{ 
				GetToken();	
				EvalExp3(out partialResult); 
				switch(op) 
				{ 
					case "-": 
						if(result.GetType().ToString() == "System.String") throw new ParserException("Syntax Error!");
						result = (double)result	- (double)partialResult; 
						break; 
					case "+": 
						if(result.GetType().ToString() == "System.String") throw new ParserException("Syntax Error!");
						result = (double)result	+ (double)partialResult; 
						break;
					case "&":
						result = result.ToString() + partialResult.ToString();
						break;
				} 
			} 
		} 
   
		// 处理乘法或除法. 
		void EvalExp3(out object result) 
		{ 
			string op; 
			object partialResult = null;	
   
			EvalExp4(out result); 
			while((op =	token) == "*" || op	== "/" ) 
			{ 
				GetToken();	
				EvalExp4(out partialResult); 
				switch(op) 
				{ 
					case "*": 
						if(result.GetType().ToString() == "System .String")	throw new ParserException("Syntax Error!");
						result = (double)result	* (double)partialResult; 
						break; 
					case "/": 
						if(result.GetType().ToString() == "System.String") throw new ParserException("Syntax Error!");
						if((double)partialResult ==	0.0) throw new ParserException("Division by Zero!"); 
						result = (double)result	/ (double)partialResult; 
						break; 
				} 
			} 
		} 
   
		// 处理函数. 
		void EvalExp4(out object result) 
		{ 
			object pramResult=null; 
			int i,k;
			string FunctionNameA = "";

			if(tokType == Types.FUNCTION)
			{
				FunctionNameA = token.ToUpper(); 
				GetToken();
				if(tokType == Types.DELIMITER && token == "(")
				{
					//取函数参数列表
					ArrayList p = new ArrayList();
					k = expIdx;
					GetToken();
					if(!(tokType == Types.DELIMITER && token == ")"))
					{
						expIdx = k;
						do
						{
							GetToken();
							if(tokType == Types.NONE && token == "") throw new ParserException("Syntax Error!");

							p.Add(token);
							if(tokType == Types.DELIMITER && (token == "," || token == ")")) continue;

							EvalExp2(out pramResult);
							p[p.Count-1] = pramResult;
						}
						while(tokType == Types.DELIMITER && token == ",");
					}

					if(!(tokType == Types.DELIMITER && token == ")")) throw new ParserException("Syntax Error!");
					//					Console.WriteLine("参数个数为：{0}",p.Count);
					//					foreach(object o in p)
					//					{
					//						Console.WriteLine(o);
					//					}
					//					Console.WriteLine("");


					GetToken();
					//指定函数是否属于内部函数
					i = InterFunctions.IndexOfKey(FunctionNameA);
					if( i != -1)
					{
						FunctionName = FunctionNameA;
						FunctionIndex = i;
						FunctionParameters = p;
						EvalInterFunction(out result);
						FunctionParameters.Clear(); 
						return;
					}
					//指定函数是否属于外部自定义函数
					i = ExterFunctions.IndexOfKey(FunctionNameA);
					if( i != -1)
					{
						FunctionName = FunctionNameA;
						FunctionParameters = p;
						EvalExterFunction(out result);
						FunctionParameters.Clear(); 
					}
					else throw new ParserException("Unkown Function!");
				}
				else throw new ParserException("Syntax Error!");
			}
			else EvalExp5(out result); 
		} 
   
		// 处理一元	+ 、 -.	
		void EvalExp5(out object result) 
		{ 
			string	op;	
   
			op = ""; 
			if((tokType	== Types.DELIMITER)	&& 
				token == "+" ||	token == "-") 
			{ 
				op = token;	
				GetToken();	
			} 
			EvalExp6(out result); 
			if(op == "-") result = -1 *	(double)result;	
		} 
   
		// 处理括号 
		void EvalExp6(out object result) 
		{ 
			if((token == "(")) 
			{ 
				GetToken();	
				EvalExp2(out result); 
				if(token !=	")") throw new ParserException("Syntax Error!");
				GetToken();
			}
			else 
			{
				if(tokType == Types.FUNCTION) EvalExp4(out result);
				else Atom(out result); 
			}
		} 
   
		// 处理数字或字符串 
		void Atom(out object result) 
		{ 
			switch(tokType)	
			{ 
				case Types.NUMBER: 
					try	
					{ 
						result = Double.Parse(token); 
					} 
					catch (FormatException)	
					{ 
						throw new ParserException("Syntax Error!"); 
					} 
					GetToken();	
					break;
				case Types.STRING:
					result = token;
					GetToken();
					break;
				default: 
					result = null; 
					throw new ParserException("Syntax Error!"); 
			} 
		} 
   
		// 引发CustomFunction事件
		void FireCustomFunction(FunctionEventArgs e)
		{
			if( CustomFunction != null )
			{
				CustomFunction(this,e);
			}
		}
		// 引发OnError事件
		void FireOnError(string ErrorMessage,int ErrorType)
		{
			ErrorEventArgs e = new ErrorEventArgs();
			e.Expression = exp;
			e.ErrorMessage = ErrorMessage;
			e.ErrorType = ErrorType;
			if( OnError != null )
			{
				OnError(this,e);
			}
		}
		// 计算内部函数
		void EvalInterFunction(out object result)
		{
			object[] args = new Object[1];
			args[0] = FunctionParameters;
			try
			{
				result = mm[(int)InterFunctions.GetByIndex(FunctionIndex)].Invoke(InterF,args);
			}
			catch
			{
				throw new ParserException(FunctionName + "() Evalute Error!"); 
			}
		}
		// 计算外部函数
		void EvalExterFunction(out object result)
		{
			FunctionResult = null;
			FunctionEventArgs e = new FunctionEventArgs();
			e.FunctionName = FunctionName;
			e.ParameterCnt = FunctionParameters.Count;
			FireCustomFunction(e);
			if(FunctionResult == null) throw new ParserException(FunctionName + "() Evalute Error!"); 
			result = FunctionResult;

		}
		// 取内部可用函数列表
		void GetInterFunctions()
		{
			mm = typeof(InterFunction).GetMethods();
			for(int i=0;i<mm.Length;i++) 
			{
				if(char.IsUpper(mm[i].Name,1)) InterFunctions.Add(mm[i].Name,i); 
			}
		}

	}
}
