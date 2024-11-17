namespace InsightWorks.DTOs.EquipmentSync;

public class SyncCommandDTO
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
    /// 开始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    public bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(SyncType))
            return false;

        if (SyncType != "Status" && SyncType != "Production")
            return false;

        if (StartTime >= EndTime)
            return false;

        return true;
    }
} 