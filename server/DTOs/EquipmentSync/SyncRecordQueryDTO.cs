using InsightWorks.DTOs.Common;
using InsightWorks.Models.Enums;

namespace InsightWorks.DTOs.EquipmentSync;

/// <summary>
/// 同步记录查询条件
/// </summary>
public class SyncRecordQueryDTO : PaginationQuery
{
    /// <summary>
    /// 设备ID
    /// </summary>
    public Guid? EquipmentId { get; set; }

    /// <summary>
    /// 同步类型
    /// </summary>
    public SyncType? SyncType { get; set; }

    /// <summary>
    /// 同步状态
    /// </summary>
    public SyncStatus? Status { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
} 