using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//IAPWSIF97 DLL
using System.Runtime.InteropServices;

namespace SIS.IFCRefer
{
    public class IAPWSIF97
    {
        /////////////////////////////////////////////////////////////////////////////////
        //2012.12.08
        //吴观辉，北京
        //暂时不完善，请不要使用

        //标准
        //IAPWS-IF97

        //Engunit
        //压力, P, MPa;
        //温度, T, DegC;
        //  焓, H, kJ/kg;
        //  熵, S, Kj/(kg-DegC);
        //  V,
        //  X,

        #region  IAPWSIF97 C++ DLL Function Example

        //extern "C" double WINAPI ReyndolNumberCal(double dblFlow, double dblDiameter, double dblViscosity);
        //extern "C" double WINAPI TemparetureCalSaturation(double dblPress);
        //extern "C" double WINAPI EnthalpyCal (double dblPress, double dblTemp, int nType);
        //extern "C" double WINAPI ViscosityCal (double dblDensity, double dblTemp);
        //extern "C" double WINAPI DensityCal (double dblPress, double dblTemp, int nType);

        //nType???

        #endregion

        #region IAPWSIF97 C# DLL Function Import


        /// <summary>
        /// 已知饱和压力求温度
        /// </summary>
        /// <param name="P"></param>
        /// <returns></returns>
        [DllImport("IAPWSIF97.dll", EntryPoint = "TemparetureCalSaturation")]
        public static extern double TemparetureCalSaturation(double P);

        /// <summary>
        /// 已知压力、温度 求焓
        /// </summary>
        /// <param name="P"></param>
        /// <param name="T"></param>
        /// <param name="V"></param>
        /// <param name="H"></param>
        /// <param name="S"></param>
        [DllImport("IAPWSIF97.dll", EntryPoint = "EnthalpyCal")]
        public static extern double EnthalpyCal(double P, double T, int N);


        #endregion

        #region IAPWSIF97 C# Fucntion 

        ///// <summary>
        ///// 过冷水、饱和水：已知压力、温度 求焓H
        ///// </summary>
        ///// <param name="P"></param>
        ///// <param name="T"></param>
        //public static double H_PTF(ref double P, ref double T)
        //{
        //    double V=0;
        //    double H=0;
        //    double S=0;

        //    PTF(ref P, ref T, ref V, ref H, ref S);

        //    return H;
        //}


        ///// <summary>
        ///// 过热蒸汽、饱和蒸汽：已知压力、温度 求焓H
        ///// </summary>
        ///// <param name="P"></param>
        ///// <param name="T"></param>
        //public static double H_PTG(ref double P, ref double T)
        //{
        //    double V = 0;
        //    double H = 0;
        //    double S = 0;

        //    PTG(ref P, ref T, ref V, ref H, ref S);

        //    return H;
        //}


        ///// <summary>
        ///// 过冷水、过热蒸汽：已知压力、温度 求焓H
        ///// </summary>
        ///// <param name="P"></param>
        ///// <param name="T"></param>
        //public static double H_PT(ref double P, ref double T)
        //{
        //    double X = 0;
        //    double V = 0;
        //    double H = 0;
        //    double S = 0;
            
        //    PT(ref P, ref T, ref X, ref V, ref H, ref S);

        //    return H;
        //}


        ///// <summary>
        ///// 过冷水、饱和水、过热蒸汽、饱和蒸汽、湿蒸汽：已知压力、焓 求温度T
        ///// </summary>
        ///// <param name="P"></param>
        ///// <param name="H"></param>
        //public static double T_PH(ref double P, ref double H)
        //{
        //    double X = 0;
        //    double T = 0;
        //    double V = 0;
        //    double S = 0;

        //    PH(ref P, ref H, ref X, ref T, ref V, ref S);

        //    return T;
        //}
        

        #endregion


    }



    /*

     *  DllImport所在的名字空间 using System.Runtime.InteropServices;
    MSDN中对DllImportAttribute的解释是这样的：可将该属性应用于方法。DllImportAttribute 属性提供对从非托管 DLL 导出的函数进行调用所必需的信息。作为最低要求，必须提供包含入口点的 DLL 的名称。
    DllImport 属性定义如下： 
     namespace System.Runtime.InteropServices 
   { 
   　 [AttributeUsage(AttributeTargets.Method)] 
   　 public class DllImportAttribute: System.Attribute 
   　 { 
   　 　public DllImportAttribute(string dllName) {...} 
   　 　public CallingConvention CallingConvention; 
   　 　public CharSet CharSet; 
   　　 public string EntryPoint; 
   　 　public bool ExactSpelling; 
   　 　public bool PreserveSig; 
   　 　public bool SetLastError; 
   　 　public string Value { get {...} } 
   　 } 
   }    
   　　说明：    
   　　1、DllImport只能放置在方法声明上。   
   　　2、DllImport具有单个定位参数：指定包含被导入方法的 dll 名称的 dllName 参数。   
   　　3、DllImport具有五个命名参数：    
   　　　a、CallingConvention 参数指示入口点的调用约定。如果未指定 CallingConvention，则使用默认值 CallingConvention.Winapi。    
   　　　b、CharSet 参数指示用在入口点中的字符集。如果未指定 CharSet，则使用默认值 CharSet.Auto。   
   　　　c、EntryPoint 参数给出 dll 中入口点的名称。如果未指定 EntryPoint，则使用方法本身的名称。    
   　　　d、ExactSpelling 参数指示 EntryPoint 是否必须与指示的入口点的拼写完全匹配。如果未指定 ExactSpelling，则使用默认值 false。    
   　　　e、PreserveSig 参数指示方法的签名应当被保留还是被转换。当签名被转换时，它被转换为一个具有 HRESULT 返回值和该返回值的一个名为 retval 的附加输出参数的签名。如果未指定 PreserveSig，则使用默认值 true。    
   　　　f、SetLastError 参数指示方法是否保留 Win32"上一错误"。如果未指定 SetLastError，则使用默认值 false。    
   　　4、它是一次性属性类。    
   　　5、此外，用 DllImport 属性修饰的方法必须具有 extern 修饰符。

    DllImport的用法：
       DllImport("MyDllImport.dll")]
        private static extern int mySum(int a,int b);
    */
}
