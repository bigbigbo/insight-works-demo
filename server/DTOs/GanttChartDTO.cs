namespace InsightWorks.DTOs;

public class GanttChartDTO
{
    public string EquipmentCode { get; set; } = null!;
    public List<StatusPeriodDTO> StatusPeriods { get; set; } = new();
}

public class StatusPeriodDTO
{
    public string Status { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? ExecutedBy { get; set; }
} 