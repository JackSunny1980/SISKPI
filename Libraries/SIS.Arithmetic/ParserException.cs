using System;

namespace SIS.Arithmetic
{
	/// <summary>
	/// ParserException 的摘要说明。
	/// </summary>
	class ParserException:ApplicationException
	{
		public ParserException(string str) : base(str) { }  
 
		public override string ToString() 
		{ 
			return Message; 
		} 
	}
}
