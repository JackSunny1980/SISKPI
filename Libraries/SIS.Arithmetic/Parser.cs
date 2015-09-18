using System;
using System.Collections; 
using System.Reflection;


namespace SIS.Arithmetic
{

	public delegate void FunctionHandler(object sender,FunctionEventArgs e);
	public delegate void ErrorHandler(object sender,ErrorEventArgs e);

	public class Parser
	{
		// ö�ټ��㵥Ԫ����. 
		enum Types { NONE, DELIMITER, VARIABLE,FUNCTION, NUMBER, STRING	}; 

		string exp;	   // ������ʽ 
		int	expIdx;	   // ��ǰ���ʽ����λ��	
		string token;  // ��ǰ�Ǻ�
		Types tokType; // ��ǰ�Ǻ�����
		string FunctionName; //��ǰ��������
		int FunctionIndex;  //��ǰ�������
		ArrayList FunctionParameters; //��ǰ��������
		MethodInfo[] mm; //�ڲ�������Ϣ

		Object FunctionResult; //�������
		SortedList InterFunctions; // �ڲ���������
		SortedList ExterFunctions; // �ⲿ��������

		InterFunction InterF;

		public event FunctionHandler CustomFunction;
		public event ErrorHandler OnError;
		
		//���캯��
		public Parser() 
		{
			FunctionResult = null;
			InterF = new InterFunction(); 
			InterFunctions = new SortedList();
			ExterFunctions = new SortedList(); 
			//����ڲ�����
			GetInterFunctions();
		}
		// �������������. 
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
   
		// �����ⲿ��������ֵ
		public void CFSetResult(object result)
		{
			FunctionResult = result;
		}

		// ����ⲿ����
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
		//ȡdouble����
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
		//ȡstring����
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
		//ȡ������Ϣ
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

		// ȡ���㵥Ԫ��� 
		void GetToken()	
		{ 
			tokType	= Types.NONE; 
			token =	"";	
	
			if(expIdx == exp.Length) return; //	���˱��ʽĩ�� 

			// ����ǰ�ÿո�	
			while(expIdx < exp.Length && Char.IsWhiteSpace(exp[expIdx])) expIdx++; 
 
			if(expIdx == exp.Length) return; //	���˱��ʽĩ��
 
			if(IsDelim(exp[expIdx])) 
			{ // �ָ���	
				token += exp[expIdx]; 
				expIdx++; 
				tokType	= Types.DELIMITER;
				return;
			} 
			if(Char.IsLetter(exp[expIdx])) 
			{ // ������
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
			{ // ���� 
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
   
		// �����Ƿ�ָ���ָ���.	
		bool IsDelim(char c) 
		{ 
			if((" +-/*&(),".IndexOf(c) != -1)) 
				return true; 
			return false; 
		}

		// ����ӷ������ 
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
   
		// ����˷������. 
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
   
		// ������. 
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
					//ȡ���������б�
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
					//					Console.WriteLine("��������Ϊ��{0}",p.Count);
					//					foreach(object o in p)
					//					{
					//						Console.WriteLine(o);
					//					}
					//					Console.WriteLine("");


					GetToken();
					//ָ�������Ƿ������ڲ�����
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
					//ָ�������Ƿ������ⲿ�Զ��庯��
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
   
		// ����һԪ	+ �� -.	
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
   
		// �������� 
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
   
		// �������ֻ��ַ��� 
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
   
		// ����CustomFunction�¼�
		void FireCustomFunction(FunctionEventArgs e)
		{
			if( CustomFunction != null )
			{
				CustomFunction(this,e);
			}
		}
		// ����OnError�¼�
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
		// �����ڲ�����
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
		// �����ⲿ����
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
		// ȡ�ڲ����ú����б�
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
