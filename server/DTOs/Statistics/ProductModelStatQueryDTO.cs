namespace InsightWorks.DTOs.Statistics;

/// <summary>
/// 产品型号统计查询条件
/// </summary>
public class ProductModelStatQueryDTO
{
    /// <summary>
    /// 产品型号ID列表
    /// </summary>
    public List<Guid> ModelIds { get; set; } = new();

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
} 