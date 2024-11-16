namespace InsightWorks.DTOs.Product;

/// <summary>
/// 创建产品型号的数据传输对象
/// </summary>
public class CreateProductDTO
{
    /// <summary>
    /// 产品型号代码
    /// </summary>
    /// <example>PM001</example>
    public string ModelCode { get; set; } = null!;

    /// <summary>
    /// 产品描述
    /// </summary>
    /// <example>示例产品</example>
    public string? Description { get; set; }
} 