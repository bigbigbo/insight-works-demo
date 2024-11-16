namespace InsightWorks.DTOs.Manufacturer;

/// <summary>
/// 厂商查询条件
/// </summary>
public class ManufacturerQueryDTO
{
    /// <summary>
    /// 厂商代码
    /// </summary>
    public string? ManufacturerCode { get; set; }

    /// <summary>
    /// 厂商名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    public string? ContactPerson { get; set; }
} 