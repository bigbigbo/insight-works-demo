using Microsoft.EntityFrameworkCore;
using server.Models;
using server.DTOs;

namespace server.Services;

public class AnalysisService : IAnalysisService
{
    private readonly AppDbContext _context;

    public AnalysisService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<GanttChartDTO>> GetEquipmentStatusGanttChart(
        DateTime startTime, 
        DateTime endTime, 
        string? equipmentCode = null)
    {
        var query = _context.Equipment
            .Include(e => e.StatusHistories)
            .AsQueryable();

        if (!string.IsNullOrEmpty(equipmentCode))
        {
            query = query.Where(e => e.EquipmentCode == equipmentCode);
        }

        var equipmentList = await query.ToListAsync();
        var result = new List<GanttChartDTO>();

        foreach (var equipment in equipmentList)
        {
            var ganttChart = new GanttChartDTO
            {
                EquipmentCode = equipment.EquipmentCode,
                StatusPeriods = equipment.StatusHistories
                    .Where(h => h.StatusChangeTime >= startTime && h.StatusChangeTime <= endTime)
                    .OrderBy(h => h.StatusChangeTime)
                    .Select((h, i) => new StatusPeriodDTO
                    {
                        Status = h.Status,
                        StartTime = h.StatusChangeTime,
                        EndTime = i < equipment.StatusHistories.Count - 1 
                            ? equipment.StatusHistories.ElementAt(i + 1).StatusChangeTime 
                            : endTime,
                        ExecutedBy = h.ExecutedBy
                    })
                    .ToList()
            };
            result.Add(ganttChart);
        }

        return result;
    }

    private DateTime GetTimePeriodStart(DateTime date, string timeUnit)
    {
        switch (timeUnit.ToLower())
        {
            case "day":
                return date.Date;
            case "week":
                return date.AddDays(-(int)date.DayOfWeek).Date;
            case "month":
                return new DateTime(date.Year, date.Month, 1);
            default:
                throw new ArgumentException("Invalid time unit. Use 'day', 'week', or 'month'.");
        }
    }

    public async Task<List<ProductionAnalysisDTO>> GetProductionTimeAnalysis(
        string timeUnit,
        DateTime startTime,
        DateTime endTime,
        string? modelCode = null)
    {
        if (!new[] { "day", "week", "month" }.Contains(timeUnit.ToLower()))
        {
            throw new ArgumentException("Invalid time unit. Use 'day', 'week', or 'month'.");
        }

        var query = _context.ProductionRecords
            .Include(p => p.Equipment)
            .Include(p => p.ProductModel)
            .Where(p => p.ProductionStartTime >= startTime && p.ProductionEndTime <= endTime);

        if (!string.IsNullOrEmpty(modelCode))
        {
            query = query.Where(p => p.ProductModel.ModelCode == modelCode);
        }

        // 在内存中执行时间分组
        var records = await query.ToListAsync();
        var groupedRecords = records
            .GroupBy(p => new
            {
                p.ProductModel.ModelCode,
                p.Equipment.EquipmentCode,
                TimePeriod = GetTimePeriodStart(p.ProductionStartTime, timeUnit)
            })
            .Select(g => new ProductionAnalysisDTO
            {
                ModelCode = g.Key.ModelCode,
                EquipmentCode = g.Key.EquipmentCode,
                AverageProductionTime = g.Average(p => 
                    (p.ProductionEndTime - p.ProductionStartTime).TotalMinutes),
                TotalProductions = g.Count()
            })
            .ToList();

        return groupedRecords;
    }
} 