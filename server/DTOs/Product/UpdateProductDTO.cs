namespace InsightWorks.DTOs.Product;

/// <summary>
/// 更新产品型号的数据传输对象
/// </summary>
public class UpdateProductDTO
{
    /// <summary>
    /// 产品型号ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 产品型号代码
    /// </summary>
    public string ModelCode { get; set; } = null!;

    /// <summary>
    /// 产品描述
    /// </summary>
    public string? Description { get; set; }
} 