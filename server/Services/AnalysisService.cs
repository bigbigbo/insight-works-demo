using Microsoft.EntityFrameworkCore;
using InsightWorks.Models;
using InsightWorks.DTOs;

namespace InsightWorks.Services;

public class AnalysisService : IAnalysisService
{
    private readonly AppDbContext _context;

    public AnalysisService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<GanttChartDTO>> GetEquipmentStatusGanttChart(DateTime startTime, DateTime endTime, string? equipmentCode = null)
    {
        var query = _context.EquipmentStatusHistories
            .Include(h => h.Equipment)
            .Where(h => h.StatusChangeTime >= startTime && h.StatusChangeTime <= endTime);

        if (!string.IsNullOrEmpty(equipmentCode))
        {
            query = query.Where(h => h.Equipment.EquipmentCode == equipmentCode);
        }

        var statusHistories = await query
            .OrderBy(h => h.Equipment.EquipmentCode)
            .ThenBy(h => h.StatusChangeTime)
            .ToListAsync();

        var result = new List<GanttChartDTO>();
        var equipmentGroups = statusHistories.GroupBy(h => h.Equipment.EquipmentCode);

        foreach (var group in equipmentGroups)
        {
            var ganttChart = new GanttChartDTO
            {
                EquipmentCode = group.Key,
                StatusPeriods = new List<StatusPeriodDTO>()
            };

            var orderedHistories = group.OrderBy(h => h.StatusChangeTime).ToList();
            for (int i = 0; i < orderedHistories.Count - 1; i++)
            {
                ganttChart.StatusPeriods.Add(new StatusPeriodDTO
                {
                    Status = orderedHistories[i].Status,
                    StartTime = orderedHistories[i].StatusChangeTime,
                    EndTime = orderedHistories[i + 1].StatusChangeTime,
                    ExecutedBy = orderedHistories[i].ExecutedBy
                });
            }

            result.Add(ganttChart);
        }

        return result;
    }

    public async Task<List<ProductionAnalysisDTO>> GetProductionTimeAnalysis(string timeUnit, DateTime startTime, DateTime endTime, string? modelCode = null)
    {
        if (!new[] { "hour", "day", "week", "month" }.Contains(timeUnit.ToLower()))
        {
            throw new ArgumentException("Invalid time unit. Must be one of: hour, day, week, month");
        }

        var query = _context.ProductionRecords
            .Include(r => r.Equipment)
            .Include(r => r.ProductModel)
            .Where(r => r.ProductionStartTime >= startTime && r.ProductionEndTime <= endTime);

        if (!string.IsNullOrEmpty(modelCode))
        {
            query = query.Where(r => r.ProductModel.ModelCode == modelCode);
        }

        var records = await query.ToListAsync();

        var analysis = records
            .GroupBy(r => new { r.ProductModel.ModelCode, r.Equipment.EquipmentCode })
            .Select(g => new ProductionAnalysisDTO
            {
                ModelCode = g.Key.ModelCode,
                EquipmentCode = g.Key.EquipmentCode,
                AverageProductionTime = g.Average(r => (r.ProductionEndTime - r.ProductionStartTime).TotalMinutes),
                TotalProductions = g.Count()
            })
            .ToList();

        return analysis;
    }
} 