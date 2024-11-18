namespace InsightWorks.DTOs.Statistics;

/// <summary>
/// 生产记录数据传输对象
/// </summary>
public class ProductionRecordDTO
{
    /// <summary>
    /// 记录ID
    /// </summary>
    public Guid Id { get; set; }

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
    /// 产品型号ID
    /// </summary>
    public Guid ProductModelId { get; set; }

    /// <summary>
    /// 产品型号代码
    /// </summary>
    public string ModelCode { get; set; } = null!;

    /// <summary>
    /// 批次号
    /// </summary>
    public string BatchNumber { get; set; } = null!;

    /// <summary>
    /// 制程前长度
    /// </summary>
    public string PreLength { get; set; } = null!;

    /// <summary>
    /// 制程前宽度
    /// </summary>
    public string PreWidth { get; set; } = null!;

    /// <summary>
    /// 制程前高度
    /// </summary>
    public string PreHeight { get; set; } = null!;

    /// <summary>
    /// 制程后长度
    /// </summary>
    public string PostLength { get; set; } = null!;

    /// <summary>
    /// 制程后宽度
    /// </summary>
    public string PostWidth { get; set; } = null!;

    /// <summary>
    /// 制程后高度
    /// </summary>
    public string PostHeight { get; set; } = null!;

    /// <summary>
    /// 生产开始时间
    /// </summary>
    public DateTime ProductionStartTime { get; set; }

    /// <summary>
    /// 生产结束时间
    /// </summary>
    public DateTime ProductionEndTime { get; set; }
} 