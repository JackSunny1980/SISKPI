using System;
using System.Collections;

namespace SIS.Arithmetic
{

	class InterFunction
	{
		public object ABS(ArrayList param)
		{
			if(param.Count != 1) throw new Exception(); 
			return Math.Abs((double)param[0]);
		}
		public object ACOS(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			if(x > 1 || x < -1) throw new Exception();
			return Math.Acos(x); 
		}
		public object ASIN(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			if(x > 1 || x < -1) throw new Exception();
			return Math.Asin(x); 
		}
		public object ATAN(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			return Math.Atan(x); 
		}
		public object AVERAGE(ArrayList param)
		{
			double x = 0.0;
			for(int i = 0 ; i < param.Count ; i++) x += (double)param[i];
			return param.Count == 0 ? 0.0 : x/param.Count; 
		}
		public object CHAR(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			int x = (int)((double)param[0]);
			if(x < 1 || x > 255) throw new Exception();
			return ((char)x).ToString();  
		}
		public object COS(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			return Math.Cos(x); 
		}
		public object DATE(ArrayList param)
		{
			if(param.Count != 3) throw new Exception();
			return (double)DateTime.Parse(param[1].ToString() + "/" +param[2].ToString() + "/" + param[0].ToString()).Ticks;   
		}
		public object DEGREE(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			return x / Math.PI * 180; 
		}

		public object EXP(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			return Math.Exp(x); 
		}

		public object FACT(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			int x = (int)((double)param[0]);
			for(int i = 1;i < x + 1;i++) x *=i; 
			return x; 
		}

		public object FLOOR(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			return Math.Floor(x); 
		}

		public object INT(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			int i = (int)x;
			return x > i ? i : i-1;				 
		}

		public object INV(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			if(x == 0) throw new Exception(); 
			return 1/x; 
		}

		public object LEFT(ArrayList param)
		{
			if(param.Count != 2) throw new Exception();
			return param[0].ToString().Substring(0,(int)((double)param[1])); 
		}
		
		public object LEN(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			if(param[0].GetType().ToString() != "System.String") throw new Exception();  
			return param[0].ToString().Length; 
		}
		
		public object LN(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			return Math.Log(x); 
		}
		public object LOG(ArrayList param)
		{
			if(param.Count != 2) throw new Exception();
			double x = (double)param[0];
			double y = (double)param[1];
			if(x < 0) throw new Exception(); 
			return Math.Log(x,y); 
		}
		public object LOG10(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			return Math.Log10(x); 
		}
		
		public object LOWER(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			return param[0].ToString().ToLower(); 
		}
		
		public object MAX(ArrayList param)
		{
			double x1 = 0.0,x2 = 0.0;
			if(param.Count == 0) throw new Exception();
			x1 = (double)param[0];
			for(int i=1;i<param.Count;i++)
			{
				x2 = (double)param[i];
				if(x1 < x2) x1 = x2;
			}
			return x1; 
		}
		
		public object MIN(ArrayList param)
		{
			double x1 = 0.0,x2 = 0.0;
			if(param.Count == 0) throw new Exception();
			x1 = (double)param[0];
			for(int i=1;i<param.Count;i++)
			{
				x2 = (double)param[i];
				if(x1 > x2) x1 = x2;
			}
			return x1; 
		}
		
		public object MOD(ArrayList param)
		{
			if(param.Count != 2) throw new Exception();
			double x1 = (double)param[0];
			double x2 = (double)param[1];
			if(x2 == 0) throw new Exception(); 
			return x1 % x2;	
		}
		
		public object NOW(ArrayList param)
		{
			if(param.Count == 0) return DateTime.Now.ToString();  	
			else 
			{
				if(param.Count != 1) throw new Exception(); 
				else return DateTime.Now.ToString(param[0].ToString());
			}
		}
		
		public object PI(ArrayList param)
		{
			if(param.Count != 0) throw new Exception();
			return Math.PI;  	
		}
		
		public object POWER(ArrayList param)
		{
			if(param.Count != 2) throw new Exception();
			double x = (double)param[0];
			double y = (double)param[1];
			if(x <= 0 || y <= 0) throw new Exception(); 
			return Math.Pow(x,y);	
		}
		
		public object PRODUCT(ArrayList param)
		{
			double x = 1.0;
			for(int i = 0 ; i < param.Count ; i++) x *= (double)param[i];
			return x ;
		}

		public object RADIANS(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			return x * Math.PI / 180; 
		}

		public object ROUND(ArrayList param)
		{
			if(param.Count != 2) throw new Exception();
			double x = (double)param[0];
			int y = (int)((double)param[1]);
			return  Math.Round(x,y); 
		}
		public object SIN(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			return Math.Sin(x); 
		}

		public object SQRT(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			if(x < 0) throw new Exception(); 
			return Math.Sqrt(x); 
		}
		public object SQUARE(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			return x * x; 
		}
		public object SUBSTRING(ArrayList param)
		{
			if(param.Count != 3) throw new Exception();
			string s = param[0].ToString();
			int x = (int)((double)param[1]);
			int y = (int)((double)param[2]);
			return s.Substring(x,y); 
		}
		
		public object SUM(ArrayList param)
		{
			double x = 0.0;
			for(int i = 0 ; i < param.Count ; i++) x += (double)param[i];
			return x; 
		}

		public object TAN(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			double x = (double)param[0];
			return Math.Tan(x); 
		}
		
		public object TRIM(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			return param[0].ToString().Trim(); 
		}
		
		public object UPPER(ArrayList param)
		{
			if(param.Count != 1) throw new Exception();
			return param[0].ToString().ToUpper(); 
		}
		

	}
}
