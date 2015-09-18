using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DataEntity;

namespace SIS.DataAccess
{
    public interface IKPI_UncartQuotaScorebookDal
    {
        /// <summary>
        /// 分页得到所有的翻卸车辆指标得分信息
        /// </summary>
        /// <param name="startIndex">页码</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>KPI_UncartQuotaScorebookEntity 集合</returns>
        List<KPI_UncartQuotaScorebookEntity> GetUncartQuotaScorebookList(DateTime startTime, DateTime endTime, int startIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 查询翻卸车辆指标得分信息
        /// </summary>
        /// <param name="startIndex">页码</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="quotaCode">指标</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>KPI_UncartQuotaScorebookEntity 集合</returns>
        List<KPI_UncartQuotaScorebookEntity> SearchUncartQuotaScorebook(int startIndex, int pageSize, string quotaCode, string beginTime, string endTime, ref int totalCount);
        
        /// <summary>
        /// 新增翻卸车辆指标得分信息记录
        /// </summary>
        /// <param name="uncartQuotaScorebookEntity">翻卸车辆指标得分信息</param>
        /// <returns></returns>
        bool Insert(KPI_UncartQuotaScorebookEntity uncartQuotaScorebookEntity);

        /// <summary>
        ///  更新翻卸车辆指标得分信息记录
        /// </summary>
        /// <param name="uncartQuotaScorebookEntity">翻卸车辆指标得分信息</param>
        /// <returns></returns>
        bool Update(KPI_UncartQuotaScorebookEntity uncartQuotaScorebookEntity);

        /// <summary>
        ///  删除翻卸车辆指标得分信息记录
        /// </summary>
        /// <param name="deleteId">主键</param>
        /// <returns></returns>
        bool Delete(string deleteId);
    }
}
