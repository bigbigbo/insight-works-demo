namespace InsightWorks.DTOs.EquipmentSync;
using InsightWorks.Models.Enums;

public class SyncCommandDTO
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
    /// 开始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    public bool IsValid()
    {
        if (StartTime >= EndTime)
            return false;

        return true;
    }
} 