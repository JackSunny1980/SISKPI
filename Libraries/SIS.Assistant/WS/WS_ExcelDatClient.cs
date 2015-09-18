using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;


namespace SIS.Assistant.WS
{
    /// <summary>
    /// 从文件中读取接口断开时间以便补采
    /// </summary>
    public class WS_ExcelDatClient
    {
        public static string strPath;

        static WS_ExcelDatClient()
        {
            strPath = @"dat\archive.dat";

            if (!Directory.Exists("dat"))
            {
                Directory.CreateDirectory("dat");
            }
        }

        public static bool ReadDatFile(out ArrayList LineList)
        {
            LineList = new ArrayList();

            if (!File.Exists(strPath))
            {
                return true;
            }

            //////////////////////////////////////////////////////////////
            try
            {
                StreamReader objReader = new StreamReader(strPath);
                string sLine = "";
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null && !sLine.Equals(""))
                        LineList.Add(sLine);
                }
                objReader.Close();

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }



        public static bool WriteDatFile(ArrayList LineList)
        {
            try
            {
                //如果此值为false，则创建一个新文件，如果存在原文件，则覆盖。
                //如果此值为true，则打开文件保留原来数据，如果找不到文件，则创建新文件。
                StreamWriter objWriter = new StreamWriter(strPath, false);
                foreach (string sLine in LineList)
                {
                    objWriter.WriteLine(sLine);
                }

                objWriter.Close();

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

    }
}
