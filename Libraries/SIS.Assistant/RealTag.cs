using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SIS.Arithmetic;
using SIS.Evaluator;

namespace SIS.Assistant
{
    public class RealTag
    {
        public String RealCode;
        public String RealDesc;
        public int  RealQulity;  //0-good, 1-alarm
        public double RealValue;
        public String RealTime;
        public String RealNote;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rcode"></param>
        /// <param name="rdesc"></param>
        /// <param name="rqulity"></param>
        /// <param name="rvalue"></param>
        /// <param name="rtime"></param>
        /// <param name="snote"></param>
        public RealTag(String rcode, String rdesc, int rqulity, double rvalue, String rtime, String snote)
        {
            RealCode =rcode;
            RealDesc =rdesc;
            RealQulity =rqulity;
            RealValue =rvalue;
            RealTime =rtime;
            RealNote = snote;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rcode"></param>
        /// <param name="rdesc"></param>
        public RealTag(String rcode, String rdesc)
        {
            RealCode = rcode;
            RealDesc = rdesc;
            RealQulity = 0;
            RealValue = 0;
            RealTime = "";
            RealNote = "";
        }

    }
}
