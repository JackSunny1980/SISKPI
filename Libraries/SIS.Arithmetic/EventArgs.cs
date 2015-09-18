using System;

namespace SIS.Arithmetic
{
	public class FunctionEventArgs : EventArgs 
	{
		public string FunctionName;// 自定义函数名
		public int ParameterCnt;// 参数个数
	}
	public class ErrorEventArgs : EventArgs 
	{
		public string Expression;//表达式字符串
		public string ErrorMessage;// 错误消息
		public int ErrorType;//错误类型
	}
}
