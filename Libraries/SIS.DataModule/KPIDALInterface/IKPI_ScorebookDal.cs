using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DataEntity;

namespace SIS.DataAccess
{
    public interface IKPI_ScorebookDal
    {
        /// <summary>
        /// 获得系统设定的月结点
        /// </summary>
        /// <param name="monthNodeName">月结点名称</param>
        /// <returns>月结点</returns>
        string GetMonthNodeValue(string monthNodeName);

        /// <summary>
        /// 根据条件查询运行值得分信息
        /// </summary>
        /// <param name="monthNode">月结点</param>
        /// <param name="searchYear">要搜索的年份</param>
        /// <param name="searchMonth">要搜索的月份份</param>
        /// <returns>KPI_ScorebookEntity 集合</returns>
        List<KPI_ScorebookEntity> GetShiftScorebook(string monthNode, string searchYear, string searchMonth);

        /// <summary>
        /// 根据条件查询岗位值得分信息
        /// </summary>
        /// <param name="monthNode">月结点</param>
        /// <param name="searchYear">要搜索的年份</param>
        /// <param name="searchMonth">要搜索的月份份</param>
        /// <returns>KPI_ScorebookEntity 集合</returns>
        List<KPI_ScorebookEntity> GetPositionScorebook(string monthNode, string searchYear, string searchMonth);

        /// <summary>
        /// 根据条件查询翻卸车辆指标得分信息
        /// </summary>
        /// <param name="searchYear">要搜索的年份</param>
        /// <param name="searchMonth">要搜索的月份</param>
        /// <returns>KPI_UncartQuotaScorebookEntity 集合</returns>
        List<KPI_UncartQuotaScorebookEntity> GetUncartQuotaScorebook(int searchYear, int searchMonth);

        /// <summary>
        /// 根据条件查询吨煤电耗指标得分信息
        /// </summary>
        /// <param name="searchYear">要搜索的年份</param>
        /// <param name="searchMonth">要搜索的月份</param>
        /// <returns>KPI_CoalElectricalQuotaScoreEntity 集合</returns>
        List<CoalElectricalQuotaScoreEntity> GetCoalElectricalQuotaScoreEntity(int searchYear, int searchMonth);
    }
}
