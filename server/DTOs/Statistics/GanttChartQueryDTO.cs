namespace InsightWorks.DTOs.Statistics;

/// <summary>
/// 甘特图数据查询条件
/// </summary>
public class GanttChartQueryDTO
{
    /// <summary>
    /// 设备ID
    /// </summary>
    public Guid? EquipmentId { get; set; }

    /// <summary>
    /// 设备编号
    /// </summary>
    public string? EquipmentCode { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
} 