using InsightWorks.DTOs;

namespace InsightWorks.Services;

public interface IAnalysisService
{
    Task<List<GanttChartDTO>> GetEquipmentStatusGanttChart(DateTime startTime, DateTime endTime, string? equipmentCode = null);
    Task<List<ProductionAnalysisDTO>> GetProductionTimeAnalysis(string timeUnit, DateTime startTime, DateTime endTime, string? modelCode = null);
} 