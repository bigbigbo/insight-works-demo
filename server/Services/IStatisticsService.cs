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
} 