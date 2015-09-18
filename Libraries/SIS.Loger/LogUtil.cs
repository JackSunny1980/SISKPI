using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using System.Reflection;

namespace SIS.Loger
{
    public class LogUtil
    {
        static object lockObj = new object();

        /// <summary>
        /// ��ǰ��������Ŀ¼
        /// </summary>
        public static string ExeDir
        {
            get
            {
                string ExePath = Assembly.GetEntryAssembly().Location;
                return ExePath.Substring(0, ExePath.LastIndexOf("\\")) + "\\";
            }
        }

        /// <summary>
        /// �õ�·���ļ�
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static string getpath(string path)
        {
            if (!Directory.Exists(path + "log"))
            {
                Directory.CreateDirectory(path + "log");
            }

            path = path + @"log\log1.txt";

            FileStream fs;
            if (!File.Exists(path))
            {
                //fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
                fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);

                fs.Close();
            }

            DirectoryInfo di = Directory.GetParent(path);
            string dir = di.FullName;

            string[] Files;
            Files = Directory.GetFiles(dir, "log" + "*.txt");

            int count = Files.Length;
            float size = 0;
            bool flag = false;

            for (int i = 0; i < count; i++)
            {
                FileInfo f = new FileInfo(Files[i]);
                //���ļ���С�Ƿ����0.98M,����������γ��µ��ļ�
                size = f.Length / (1024 * 1000);
                if (size > 1)
                    continue;
                else
                {
                    path = Files[i];
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                count = count + 1;
                path = dir + "\\log" + count.ToString() + ".txt";
            }
            return path;
        }

        /// <summary>
        /// ��¼log��Ϣ
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="msg">log</param>
        public static void LogMessage(string path, string msg)
        {
            lock (lockObj)
            {
                path = getpath(path);
                FileStream fs;
                //if (File.Exists(path))
                //{
                //    fs = File.Open(path, FileMode.Append, FileAccess.Write);
                //}
                //else
                //{
                //    fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
                //}
                if (File.Exists(path))
                {
                    fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }
                else
                {
                    fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                }

                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                msg = DateTime.Now.ToString() + "    " + msg;
                sw.WriteLine(msg);

                sw.Close();

                fs.Close();
            }
        }

        /// <summary>
        /// ��¼log��Ϣ
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="ex">exception</param>
        public static void LogMessage(string path, Exception ex)
        {
            LogMessage(path, ex.Message + "||" + ex.InnerException + "||" + ex.Source + "\r\n" + ex.TargetSite + "\r\n" + ex.StackTrace);
        }


        /// <summary>
        /// ��¼log��Ϣ
        /// </summary>
        /// <param name="msg">��Ϣ����</param>
        public static void LogMessage(string msg)
        {
            lock (lockObj)
            {
                string path = getpath(ExeDir);
                FileStream fs;
                //if (File.Exists(path))
                //{
                //    fs = File.Open(path, FileMode.Append, FileAccess.Write);
                //}
                //else
                //{
                //    fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
                //}

                if (File.Exists(path))
                {
                    fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }
                else
                {
                    fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                }

                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                msg = DateTime.Now.ToString() + "    " + msg;
                sw.WriteLine(msg);

                sw.Close();
                fs.Close();
            }
        }

        /// <summary>
        /// ��¼log��Ϣ
        /// </summary>
        /// <param name="msg">��Ϣ����</param>
        public static void LogMessage(string msg, DateTime dateTime)
        {
            lock (lockObj)
            {
                string path = getpath(ExeDir);
                FileStream fs;
                //if (File.Exists(path))
                //{
                //    fs = File.Open(path, FileMode.Append, FileAccess.Write);
                //}
                //else
                //{
                //    fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
                //}

                if (File.Exists(path))
                {
                    fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }
                else
                {
                    fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                }

                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                msg = dateTime.ToString() + "    " + msg;
                sw.WriteLine(msg);

                sw.Close();
                fs.Close();
            }
        }

        /// <summary>
        /// ��¼log��Ϣ
        /// </summary>
        /// <param name="ex">exception</param>
        public static void LogMessage(Exception ex)
        {
            LogMessage(ExeDir, ex.Message + "||" + ex.InnerException + "||" + ex.Source + "\r\n" + ex.TargetSite + "\r\n" + ex.StackTrace);
        }	
        
    }
}
