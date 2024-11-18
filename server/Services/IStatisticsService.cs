using InsightWorks.DTOs.Common;
using InsightWorks.DTOs.Equipment;
using InsightWorks.DTOs.Statistics;
using InsightWorks.Models;

namespace InsightWorks.Services;

public interface IStatisticsService
{
    /// <summary>
    /// 分页查询设备状态历史
    /// </summary>
    Task<PaginatedList<EquipmentStatusHistory>> QueryStatusHistoryAsync(StatusHistoryQueryDTO query);

    /// <summary>
    /// 分页查询生产记录
    /// </summary>
    Task<ProductionRecordPagedResult> QueryProductionRecordsAsync(ProductionRecordQueryDTO query);

    /// <summary>
    /// 获取甘特图数据
    /// </summary>
    Task<List<GanttChartDTO>> GetGanttChartDataAsync(GanttChartQueryDTO query);
} 