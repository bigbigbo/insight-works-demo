namespace InsightWorks.DTOs;

public class ProductionAnalysisDTO
{
    public string ModelCode { get; set; } = null!;
    public string EquipmentCode { get; set; } = null!;
    public double AverageProductionTime { get; set; } // 平均生产时间（分钟）
    public int TotalProductions { get; set; } // 总生产次数
} 