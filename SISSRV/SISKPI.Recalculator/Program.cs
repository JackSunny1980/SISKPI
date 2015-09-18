﻿using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace SISKPI.Recalculator
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new MainService() 
			};

            ServiceBase.Run(ServicesToRun);
        }
    }
}
