using InsightWorks.DTOs.Common;

namespace InsightWorks.DTOs.Statistics;

/// <summary>
/// 生产记录查询条件
/// </summary>
public class ProductionRecordQueryDTO : PaginationQuery
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
    /// 产品型号ID
    /// </summary>
    public Guid? ProductModelId { get; set; }

    /// <summary>
    /// 产品型号代码
    /// </summary>
    public string? ModelCode { get; set; }

    /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNumber { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
} 