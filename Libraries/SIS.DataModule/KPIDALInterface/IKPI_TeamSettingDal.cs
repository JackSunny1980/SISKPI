using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DataEntity;

namespace SIS.DataAccess
{
    public interface IKPI_TeamSettingDal
    {
        /// <summary>
        /// 分页得到所有的班组配置信息
        /// </summary>
        /// <param name="startIndex">页码</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>KPI_TeamEntity 集合</returns>
        List<KPI_TeamEntity> GetTeamList(int startIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 根据主键得到班组配置信息
        /// </summary>
        /// <param name="teamId">主键</param>
        /// <returns>KPI_TeamEntity</returns>
        KPI_TeamEntity GetTeamEntityByID(string teamId);

        /// <summary>
        /// 得到所有的单元组
        /// </summary>
        /// <returns>KPI_PlantEntity</returns>
        List<KPI_PlantEntity> GetPlantList();

        /// <summary>
        ///  得到所有的运行值
        /// </summary>
        /// <returns>KPI_ShiftEntity</returns>
        List<KPI_ShiftEntity> GetShiftList();

        /// <summary>
        /// 得到所有的人员
        /// </summary>
        /// <returns>KPI_PersonEntity</returns>
        List<KPI_PersonEntity> GetPersonList();

        /// <summary>
        /// 根据岗位ID获得班组信息的运行值
        /// </summary>
        /// <param name="positionID">岗位ID</param>
        /// <returns>KPI_TeamEntity 集合</returns>
        List<KPI_TeamEntity> GetTeamWithShiftByPosition(string positionID);

        /// <summary>
        /// 新增班组配置信息记录
        /// </summary>
        /// <param name="teamEntity">班组配置信息</param>
        /// <returns></returns>
        bool Insert(KPI_TeamEntity teamEntity);

        /// <summary>
        ///  更新班组配置信息记录
        /// </summary>
        /// <param name="teamEntity">班组配置信息</param>
        /// <returns></returns>
        bool Update(KPI_TeamEntity teamEntity);

        /// <summary>
        ///  删除班组配置信息记录
        /// </summary>
        /// <param name="deleteId">主键</param>
        /// <returns></returns>
        bool Delete(string deleteId);

        /// <summary>
        ///  删除所有班组配置信息记录
        /// </summary>
        /// <returns></returns>
        bool Delete();
    }
}
