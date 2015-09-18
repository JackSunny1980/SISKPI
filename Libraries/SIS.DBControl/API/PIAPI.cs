using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Runtime.InteropServices;

/// <summary>
/// PI 的摘要说明


namespace SIS.DBControl
{
    /// </summary>
    public class PIAPI
    {
        
        private static PIAPI _papi = null;
        public static PIAPI instance()
        {
            if (_papi == null)
                _papi = new PIAPI();

            return _papi;
        }


        [DllImport("piapi32.dll")]
        public static extern Int32 piut_setservernode(string constr);

        [DllImport("piapi32.dll")]
        public static extern Int32 piut_disconnectnode(string constr);         

        [DllImport("piapi32.dll")]
        public static extern Int32 piut_connect(string constr);

        [DllImport("piapi32.dll")]
        public static extern Int32 piut_disconnect();

        [DllImport("piapi32.dll")]
        public static extern Int32 pipt_findpoint(string tagname, ref Int32 pt);

        [DllImport("piapi32.dll")]
        public static extern Int32 pipt_pointid(Int32 pt, ref Int32 id);

        [DllImport("piapi32.dll")]
        public static extern Int32 pipt_ptexist(Int32 pt);

        [DllImport("piapi32.dll")]
        public static extern Int32 pitm_parsetime(string time, Int32 rtime, ref Int32 timedate);

        [DllImport("piapi32.dll")]
        public static extern Int32 piar_summary(Int32 pt, ref Int32 t1, ref Int32 t2, out float rv, out float pg, Int32 type);

        [DllImport("piapi32.dll")]
        public static extern Int32 pipt_pointtype(Int32 pt, ref char type);

        [DllImport("piapi32.dll")]
        public static extern Int32 pipt_digcodefortag(Int32 pt,out Int32 digcode, string digstr);

        [DllImport("piapi32.dll")]
        public static extern Int32 piar_value(Int32 pt, ref Int32 etime, Int32 mode, out float val, out Int32 isstat);

        [DllImport("piapi32.dll")]
        public static extern Int32 pisn_getsnapshot(Int32 pt, out float val, out Int32 isstat, out Int32 timedate);

        [DllImport("piapi32.dll")]
        public static extern Int32 pipt_digstate(Int32 digcode, out byte[] digstate, Int32 len);

        [DllImport("piapi32.dll")]
        public static extern Int32 pipt_digstate(Int32 digcode, out char[] digstate, Int32 len);

        [DllImport("piapi32.dll")]
        public static extern Int32 pipt_digstate(Int32 digcode, out char digstate, Int32 len);

        [DllImport("piapi32.dll")]
        public static extern Int32 pipt_digstate(Int32 digcode, out string[] digstate, Int32 len);

        [DllImport("piapi32.dll")]
        public static extern Int32 piar_timedvalues(Int32 pt, ref Int32 arrlen, ref Int32[] time, out float[] rval, out Int32[] istats, Int32 code);

        [DllImport("piapi32.dll")]
        public static extern Int32 piar_timedvalues(Int32 pt, ref Int32 arrlen, ref Int32 time, out float rval, out Int32 istats, Int32 code);        
    }
}
