using InsightWorks.DTOs.Common;
using InsightWorks.Models.Enums;

namespace InsightWorks.DTOs.Statistics;

/// <summary>
/// 设备状态历史查询条件
/// </summary>
public class StatusHistoryQueryDTO : PaginationQuery
{
    /// <summary>
    /// 设备ID
    /// </summary>
    public Guid? EquipmentId { get; set; }

    /// <summary>
    /// 设备编码
    /// </summary>
    public string? EquipmentCode { get; set; }

    /// <summary>
    /// 设备状态
    /// </summary>
    public EquipmentStatus? Status { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 执行人
    /// </summary>
    public string? ExecutedBy { get; set; }
}