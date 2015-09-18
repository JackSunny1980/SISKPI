using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace SIS.DataAccess
{
    public sealed class DataModuleFactory
    {
        private static readonly string dataModulePath = ConfigurationManager.AppSettings["DataModulePath"];

        public static IKPI_OverLimitRecordDal CreateKPI_OverLimitRecordDal() 
        {
            string className = dataModulePath + ".KPI_OverLimitRecordDal";
            return (IKPI_OverLimitRecordDal)Assembly.Load(dataModulePath).CreateInstance(className);
        }

        public static IKPI_OverLimitConfigDal CreateKPI_OverLimitConfigDal() 
        {
            string className = dataModulePath + ".KPI_OverLimitConfigDal";
            return (IKPI_OverLimitConfigDal)Assembly.Load(dataModulePath).CreateInstance(className);
        }

        public static IKPI_TeamSettingDal CreateKPI_TeamSettingDal() 
        {
            string className = dataModulePath + ".KPI_TeamSettingDal";
            return (IKPI_TeamSettingDal)Assembly.Load(dataModulePath).CreateInstance(className);
        }

        public static IKPI_ScorebookDal CreateKPI_ScorebookDal() 
        {
            string className = dataModulePath + ".KPI_ScorebookDal";
            return (IKPI_ScorebookDal)Assembly.Load(dataModulePath).CreateInstance(className);
        }

        public static IKPI_UncartQuotaScorebookDal CreateKPI_UncartQuotaScorebookDal()
        {
            string className = dataModulePath + ".KPI_UncartQuotaScorebookDal";
            return (IKPI_UncartQuotaScorebookDal)Assembly.Load(dataModulePath).CreateInstance(className);
        }

        public static IKPI_CoalElectricalQuotaScorebookDal CreateKPI_CoalElectricalQuotaScorebookDal()
        {
            string className = dataModulePath + ".KPI_CoalElectricalQuotaScorebookDal";
            return (IKPI_CoalElectricalQuotaScorebookDal)Assembly.Load(dataModulePath).CreateInstance(className);
        }

        public static IKPI_PersonScoreDal CreatePersonScoreDal()
        {
            string className = dataModulePath + ".KPI_PersonScoreDal";
            return (IKPI_PersonScoreDal)Assembly.Load(dataModulePath).CreateInstance(className);
        }
    }
}
