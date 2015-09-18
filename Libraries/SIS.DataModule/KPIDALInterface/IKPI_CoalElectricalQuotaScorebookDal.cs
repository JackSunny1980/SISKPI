using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DataEntity;

namespace SIS.DataAccess
{
    public interface IKPI_CoalElectricalQuotaScorebookDal
    {
        /// <summary>
        /// 分页得到吨煤电耗指标得分信息
        /// </summary>
        /// <param name="startIndex">页码</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>KPI_CoalElectricalQuotaScoreEntity 集合</returns>
        List<CoalElectricalQuotaScoreEntity> GetCoalElectricalQuotaScorebookList(DateTime startTime,DateTime endTime,int startIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 查询得到吨煤电耗指标得分信息
        /// <param name="startIndex">页码</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="shiftValue">值别</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>KPI_CoalElectricalQuotaScoreEntity 集合</returns>
        List<CoalElectricalQuotaScoreEntity> SearchUncartQuotaScorebook(int startIndex, int pageSize, string shiftValue, string beginTime, string endTime, ref int totalCount);

        /// <summary>
        /// 新增吨煤电耗指标得分信息记录
        /// </summary>
        /// <param name="coalElectricalQuotaScoreEntity">吨煤电耗指标得分信息</param>
        /// <returns></returns>
        bool Insert(CoalElectricalQuotaScoreEntity coalElectricalQuotaScoreEntity);

        /// <summary>
        ///  更新吨煤电耗指标得分信息记录
        /// </summary>
        /// <param name="coalElectricalQuotaScoreEntity">吨煤电耗指标得分信息</param>
        /// <returns></returns>
        bool Update(CoalElectricalQuotaScoreEntity coalElectricalQuotaScoreEntity);

        /// <summary>
        ///  删除吨煤电耗指标得分信息记录
        /// </summary>
        /// <param name="deleteId">主键</param>
        /// <returns></returns>
        bool Delete(string deleteId);
    }
}
