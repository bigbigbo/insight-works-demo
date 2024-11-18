using InsightWorks.Models.Enums;

namespace InsightWorks.DTOs.Statistics;

/// <summary>
/// 甘特图数据
/// </summary>
public class GanttChartDTO
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public EquipmentInfo Equipment { get; set; } = null!;

    /// <summary>
    /// 机况记录列表
    /// </summary>
    public List<StatusRecord> StatusRecords { get; set; } = new();

    /// <summary>
    /// 生产记录列表
    /// </summary>
    public List<ProductionGanttRecord> ProductionRecords { get; set; } = new();
}

public class EquipmentInfo
{
    /// <summary>
    /// 设备ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 设备编号
    /// </summary>
    public string EquipmentCode { get; set; } = null!;

    /// <summary>
    /// 厂商名称
    /// </summary>
    public string ManufacturerName { get; set; } = null!;
}

public class StatusRecord
{
    /// <summary>
    /// 设备状态
    /// </summary>
    public EquipmentStatus Status { get; set; }

    /// <summary>
    /// 状态变更时间
    /// </summary>
    public DateTime StatusChangeTime { get; set; }

    /// <summary>
    /// 执行人
    /// </summary>
    public string? ExecutedBy { get; set; }
}

public class ProductionGanttRecord
{
    /// <summary>
    /// 产品型号代码
    /// </summary>
    public string ModelCode { get; set; } = null!;

    /// <summary>
    /// 批次号
    /// </summary>
    public string BatchNumber { get; set; } = null!;

    /// <summary>
    /// 生产开始时间
    /// </summary>
    public DateTime ProductionStartTime { get; set; }

    /// <summary>
    /// 生产结束时间
    /// </summary>
    public DateTime ProductionEndTime { get; set; }
} 