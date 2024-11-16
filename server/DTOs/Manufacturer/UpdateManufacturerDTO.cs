namespace InsightWorks.DTOs.Manufacturer;

/// <summary>
/// 更新厂商的数据传输对象
/// </summary>
public class UpdateManufacturerDTO
{
    /// <summary>
    /// 厂商ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 厂商代码
    /// </summary>
    public string ManufacturerCode { get; set; } = null!;

    /// <summary>
    /// 厂商名称
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// 厂商地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    public string? ContactPerson { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? ContactPhone { get; set; }
} 