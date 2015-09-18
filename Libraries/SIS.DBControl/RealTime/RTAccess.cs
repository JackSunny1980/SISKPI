using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.DBControl 
{
    class RTAccess<T> where T : new()
    {
        private static T _t;

        public static T instance()
        {
            if (_t == null)
            {
                _t = new T();
            }
            return _t;
        }

    }

    public class InterfaceImp
    {
        /// <summary>
        /// ʵʱ������
        /// </summary>
        public enum rtType
        {
            edna,
            pi,
            pSpace
        }

        /// <summary>
        /// �������ͳ�ʼ��ʵʱ���ʿ��ƽӿ�
        /// </summary>
        /// <param name="type">ö������</param>
        /// <returns></returns>
        public static RTInterface Create(rtType type)
        {
            if(type ==rtType.edna )
            {
                return EDNAHelper.Instance();
            }
            else if(type ==rtType.pi)
            {
                return PIHelper.Instance();
            }
            else if (type == rtType.pSpace)
            {
                return pSpaceHelper.Instance();
            }
            return null;
        }

        /// <summary>
        /// �������ͳ�ʼ��ʵʱ���ʿ��ƽӿ�
        /// </summary>
        /// <param name="type">�ַ���</param>
        /// <returns></returns>
        public static RTInterface Create(string type)
        {
            if (type.ToLower().Equals("edna"))
            {
                return EDNAHelper.Instance();
            }
            else if (type.ToLower().Equals("pi"))
            {
                return PIHelper.Instance();
            }
            else if (type.ToLower().Equals("pspace"))
            {
                return pSpaceHelper.Instance();
            }
            return null;
        }

        public static RTInterface Create(string type,bool needclose)
        {
            if (type.ToLower().Equals("edna"))
            {
                return EDNAHelper.Instance();
            }
            else if (type.ToLower().Equals("pi"))
            {
                return PIHelper.Instance(needclose);
            }
            else if (type.ToLower().Equals("pspace"))
            {
                return pSpaceHelper.Instance();
            }
            return null;
        }      
    }
}
