namespace InsightWorks.DTOs.EquipmentSync;

public class UploadStatusDataDTO
{
    /// <summary>
    /// 设备ID
    /// </summary>
    public Guid EquipmentId { get; set; }

    /// <summary>
    /// 机况数据列表
    /// </summary>
    public List<StatusData> StatusList { get; set; } = new();
}

public class StatusData
{
    /// <summary>
    /// 设备状态
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    /// 状态变更时间
    /// </summary>
    public DateTime StatusChangeTime { get; set; }

    /// <summary>
    /// 执行人
    /// </summary>
    public string? ExecutedBy { get; set; }
} 