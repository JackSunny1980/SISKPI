using System;

namespace SIS.Arithmetic
{
	/// <summary>
	/// ParserException ��ժҪ˵����
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
