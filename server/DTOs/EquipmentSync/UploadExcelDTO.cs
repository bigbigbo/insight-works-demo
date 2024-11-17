namespace InsightWorks.DTOs.EquipmentSync;

public class UploadExcelDTO
{
    /// <summary>
    /// 设备ID
    /// </summary>
    public Guid EquipmentId { get; set; }

    /// <summary>
    /// 同步类型：Status-机况数据, Production-生产数据
    /// </summary>
    public string SyncType { get; set; } = null!;

    /// <summary>
    /// Excel文件
    /// </summary>
    public IFormFile File { get; set; } = null!;
}