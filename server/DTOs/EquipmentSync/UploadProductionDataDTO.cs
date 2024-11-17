namespace InsightWorks.DTOs.EquipmentSync;

public class UploadProductionDataDTO
{
    /// <summary>
    /// 设备ID
    /// </summary>
    public Guid EquipmentId { get; set; }

    /// <summary>
    /// 生产数据列表
    /// </summary>
    public List<ProductionData> ProductionList { get; set; } = new();
}

public class ProductionData
{
    /// <summary>
    /// 产品型号ID
    /// </summary>
    public Guid ProductModelId { get; set; }

    /// <summary>
    /// 批次号
    /// </summary>
    public string BatchNumber { get; set; } = null!;

    /// <summary>
    /// 制程前长度
    /// </summary>
    public decimal PreLength { get; set; }

    /// <summary>
    /// 制程前宽度
    /// </summary>
    public decimal PreWidth { get; set; }

    /// <summary>
    /// 制程前高度
    /// </summary>
    public decimal PreHeight { get; set; }

    /// <summary>
    /// 制程后长度
    /// </summary>
    public decimal PostLength { get; set; }

    /// <summary>
    /// 制程后宽度
    /// </summary>
    public decimal PostWidth { get; set; }

    /// <summary>
    /// 制程后高度
    /// </summary>
    public decimal PostHeight { get; set; }

    /// <summary>
    /// 生产开始时间
    /// </summary>
    public DateTime ProductionStartTime { get; set; }

    /// <summary>
    /// 生产结束时间
    /// </summary>
    public DateTime ProductionEndTime { get; set; }
} 