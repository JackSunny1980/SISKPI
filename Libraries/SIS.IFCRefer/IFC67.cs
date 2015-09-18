using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//IFC DLL
using System.Runtime.InteropServices;

namespace SIS.IFCRefer
{
    public class IFC67
    {
        //标准
        //IFC-67

        //Engunit
        //压力, P, bar;
        //温度, T, DegC;
        //  焓, H, kJ/kg;
        //  熵, S, Kj/(kg-DegC);
        //  V,
        //  X,

        #region  IFC67 C++ DLL Function Example

        ////已知饱和温度求压力
        //    double __stdcall PSK(double *T);

        ////已知饱和压力求温度
        //    double __stdcall TSK(double *P);

        ////过冷水、饱和水：已知压力、温度 求比容、焓、熵
        //    void __stdcall PTF(double *P,double *T,double *V,double *H,double *S);

        ////过热蒸汽、饱和蒸汽：已知压力、温度 求比容、焓、熵
        //    void __stdcall PTG(double *P,double *T,double *V,double *H,double *S);

        ////过冷水、过热蒸汽：已知压力、温度 求比容、焓、熵、干度
        //    void __stdcall PT(double *P,double *T,double *X,double *V,double *H,double *S);

        ////过冷水、饱和水、过热蒸汽、饱和蒸汽、湿蒸汽：已知压力、焓 求比容、温度、熵、干度
        //    void __stdcall PH(double *P,double *H,double *X,double *T,double *V,double *S);

        ////过冷水、饱和水、过热蒸汽、饱和蒸汽、湿蒸汽：已知压力、熵 求比容、温度、焓、干度
        //    void __stdcall PS(double *P,double *S,double *X,double *T,double *V,double *H);

        ////过热蒸汽、饱和蒸汽、、湿蒸汽：已知焓、熵 求比容、温度、压力、干度
        //    void __stdcall HS(double *H,double *S,double *X,double *P,double *T,double *V);

        ////饱和水、饱和蒸汽、、湿蒸汽：已知压力、干度 求比容、温度、熵、焓
        //    void __stdcall PX(double *P,double *X,double *T,double *V,double *H,double *S);

        ////当X〈 0 ，表示过冷水或未饱和水，且其值为过冷度(t-ts).
        ////当X 〉1 ，表示为过热蒸汽，过热度为(t-ts);
        ////当0=〈X〈=1表示为湿蒸汽，其值为湿蒸汽的干度

        #endregion

        #region IFC67 C# DLL Function Import

        /// <summary>
        /// 已知饱和温度求压力
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [DllImport("IFC67.dll", EntryPoint = "PSK")]
        public static extern double PSK(ref double T);

        /// <summary>
        /// 已知饱和压力求温度
        /// </summary>
        /// <param name="P"></param>
        /// <returns></returns>
        [DllImport("IFC67.dll", EntryPoint = "TSK")]
        public static extern double TSK(ref double P);

        /// <summary>
        /// 过冷水、饱和水：已知压力、温度 求比容、焓、熵
        /// </summary>
        /// <param name="P"></param>
        /// <param name="T"></param>
        /// <param name="V"></param>
        /// <param name="H"></param>
        /// <param name="S"></param>
        [DllImport("IFC67.dll", EntryPoint = "PTF")]
        public static extern void PTF(ref double P, ref double T, ref double V, ref double H, ref double S);

        /// <summary>
        /// 过热蒸汽、饱和蒸汽：已知压力、温度 求比容、焓、熵
        /// </summary>
        /// <param name="P"></param>
        /// <param name="T"></param>
        /// <param name="V"></param>
        /// <param name="H"></param>
        /// <param name="S"></param>
        [DllImport("IFC67.dll", EntryPoint = "PTG")]
        public static extern void PTG(ref double P, ref double T, ref double V, ref double H, ref double S);


        /// <summary>
        /// 过冷水、过热蒸汽：已知压力、温度 求比容、焓、熵、干度
        /// </summary>
        /// <param name="P"></param>
        /// <param name="T"></param>
        /// <param name="X"></param>
        /// <param name="V"></param>
        /// <param name="H"></param>
        /// <param name="S"></param>
        [DllImport("IFC67.dll", EntryPoint = "PT")]
        public static extern void PT(ref double P, ref double T, ref double X, ref double V, ref double H, ref double S);

        /// <summary>
        /// 过冷水、饱和水、过热蒸汽、饱和蒸汽、湿蒸汽：已知压力、焓 求比容、温度、熵、干度
        /// </summary>
        /// <param name="P"></param>
        /// <param name="H"></param>
        /// <param name="X"></param>
        /// <param name="T"></param>
        /// <param name="V"></param>
        /// <param name="S"></param>
        [DllImport("IFC67.dll", EntryPoint = "PH")]
        public static extern void PH(ref double P, ref double H, ref double X, ref double T, ref double V, ref double S);

        /// <summary>
        /// 过冷水、饱和水、过热蒸汽、饱和蒸汽、湿蒸汽：已知压力、熵 求比容、温度、焓、干度
        /// </summary>
        /// <param name="P"></param>
        /// <param name="S"></param>
        /// <param name="X"></param>
        /// <param name="T"></param>
        /// <param name="V"></param>
        /// <param name="H"></param>
        [DllImport("IFC67.dll", EntryPoint = "PS")]
        public static extern void PS(ref double P, ref double S, ref double X, ref double T, ref double V, ref double H);


        /// <summary>
        /// 过热蒸汽、饱和蒸汽、、湿蒸汽：已知焓、熵 求比容、温度、压力、干度
        /// </summary>
        /// <param name="H"></param>
        /// <param name="S"></param>
        /// <param name="X"></param>
        /// <param name="P"></param>
        /// <param name="T"></param>
        /// <param name="V"></param>
        [DllImport("IFC67.dll", EntryPoint = "HS")]
        public static extern void HS(ref double H, ref double S, ref double X, ref double P, ref double T, ref double V);

        /// <summary>
        /// 饱和水、饱和蒸汽、、湿蒸汽：已知压力、干度 求比容、温度、熵、焓
        /// </summary>
        /// <param name="P"></param>
        /// <param name="X"></param>
        /// <param name="T"></param>
        /// <param name="V"></param>
        /// <param name="H"></param>
        /// <param name="S"></param>
        [DllImport("IFC67.dll", EntryPoint = "PX")]
        public static extern void PX(ref double P, ref double X, ref double T, ref double V, ref double H, ref double S);

        #endregion

        #region IFC67 C# Fucntion

        /// <summary>
        /// 过冷水、饱和水：已知压力、温度 求焓H
        /// </summary>
        /// <param name="P"></param>
        /// <param name="T"></param>
        public static double H_PTF(ref double P, ref double T)
        {
            double V=0;
            double H=0;
            double S=0;

            PTF(ref P, ref T, ref V, ref H, ref S);

            return H;
        }


        /// <summary>
        /// 过热蒸汽、饱和蒸汽：已知压力、温度 求焓H
        /// </summary>
        /// <param name="P"></param>
        /// <param name="T"></param>
        public static double H_PTG(ref double P, ref double T)
        {
            double V = 0;
            double H = 0;
            double S = 0;

            PTG(ref P, ref T, ref V, ref H, ref S);

            return H;
        }


        /// <summary>
        /// 过冷水、过热蒸汽：已知压力、温度 求焓H
        /// </summary>
        /// <param name="P"></param>
        /// <param name="T"></param>
        public static double H_PT(ref double P, ref double T)
        {
            double X = 0;
            double V = 0;
            double H = 0;
            double S = 0;
            
            PT(ref P, ref T, ref X, ref V, ref H, ref S);

            return H;
        }


        /// <summary>
        /// 过冷水、饱和水、过热蒸汽、饱和蒸汽、湿蒸汽：已知压力、焓 求温度T
        /// </summary>
        /// <param name="P"></param>
        /// <param name="H"></param>
        public static double T_PH(ref double P, ref double H)
        {
            double X = 0;
            double T = 0;
            double V = 0;
            double S = 0;

            PH(ref P, ref H, ref X, ref T, ref V, ref S);

            return T;
        }
        

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
