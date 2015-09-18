using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using System.Diagnostics;

namespace SISKPI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Exception LastError = Server.GetLastError();
            //String ErrMessage = LastError.ToString();
            //String LogName = "SISOPMLog";
            //String Message = "Url " + Request.Path + " Error: " + ErrMessage;

            //// Create Event Log if It Doesn't Exist
            //if (!EventLog.SourceExists(LogName))
            //{
            //    EventLog.CreateEventSource(LogName, LogName);
            //}
            //EventLog Log = new EventLog();
            //Log.Source = LogName;

            ////These are the five options that will display a different icon.
            //Log.WriteEntry(Message, EventLogEntryType.Information, 1);
            //Log.WriteEntry(Message, EventLogEntryType.Error, 2);
            //Log.WriteEntry(Message, EventLogEntryType.Warning, 3);
            //Log.WriteEntry(Message, EventLogEntryType.SuccessAudit, 4);
            //Log.WriteEntry(Message, EventLogEntryType.FailureAudit, 5);

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}