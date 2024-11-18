namespace InsightWorks.DTOs.Statistics;

/// <summary>
/// 产品型号统计结果
/// </summary>
public class ProductModelStatDTO
{
    /// <summary>
    /// 产品型号ID
    /// </summary>
    public Guid ModelId { get; set; }

    /// <summary>
    /// 产品型号代码
    /// </summary>
    public string ModelCode { get; set; } = null!;

    /// <summary>
    /// 产品描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 各设备的生产统计
    /// </summary>
    public List<EquipmentProductionStat> EquipmentStats { get; set; } = new();
}

/// <summary>
/// 设备生产统计
/// </summary>
public class EquipmentProductionStat
{
    /// <summary>
    /// 设备ID
    /// </summary>
    public Guid EquipmentId { get; set; }

    /// <summary>
    /// 设备编号
    /// </summary>
    public string EquipmentCode { get; set; } = null!;

    /// <summary>
    /// 厂商名称
    /// </summary>
    public string ManufacturerName { get; set; } = null!;

    /// <summary>
    /// 生产批次数
    /// </summary>
    public int BatchCount { get; set; }

    /// <summary>
    /// 平均生产时间(小时)
    /// </summary>
    public double AvgProductionTime { get; set; }
} 