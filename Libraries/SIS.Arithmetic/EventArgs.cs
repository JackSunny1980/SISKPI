using System;

namespace SIS.Arithmetic
{
	public class FunctionEventArgs : EventArgs 
	{
		public string FunctionName;// �Զ��庯����
		public int ParameterCnt;// ��������
	}
	public class ErrorEventArgs : EventArgs 
	{
		public string Expression;//���ʽ�ַ���
		public string ErrorMessage;// ������Ϣ
		public int ErrorType;//��������
	}
}
