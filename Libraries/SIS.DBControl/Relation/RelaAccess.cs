using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SIS.DBControl
{
    public class RelaAccess 
    {
        public static RelaInterface Create(string type)
        {
            if (type.Equals("sql"))
            {
                return sql.Instance();
            }
            else if (type.Equals("oracle"))
            {
                return oracle.Instance();
            }
            return null;
        }        
    }
}
