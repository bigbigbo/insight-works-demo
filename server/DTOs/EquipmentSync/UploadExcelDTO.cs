namespace InsightWorks.DTOs.EquipmentSync;
using InsightWorks.Models.Enums;

public class UploadExcelDTO
{
    /// <summary>
    /// 设备ID
    /// </summary>
    public Guid EquipmentId { get; set; }

    /// <summary>
    /// 同步类型
    /// </summary>
    public SyncType SyncType { get; set; }

    /// <summary>
    /// Excel文件
    /// </summary>
    public IFormFile File { get; set; } = null!;
}