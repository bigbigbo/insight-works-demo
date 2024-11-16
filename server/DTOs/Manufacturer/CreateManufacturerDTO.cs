namespace InsightWorks.DTOs.Manufacturer;

/// <summary>
/// 创建厂商的数据传输对象
/// </summary>
public class CreateManufacturerDTO
{
    /// <summary>
    /// 厂商代码
    /// </summary>
    /// <example>MFR001</example>
    public string ManufacturerCode { get; set; } = null!;

    /// <summary>
    /// 厂商名称
    /// </summary>
    /// <example>示例厂商</example>
    public string Name { get; set; } = null!;

    /// <summary>
    /// 厂商地址
    /// </summary>
    /// <example>示例地址</example>
    public string? Address { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    /// <example>张三</example>
    public string? ContactPerson { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    /// <example>13800138000</example>
    public string? ContactPhone { get; set; }
} 