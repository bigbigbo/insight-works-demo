using InsightWorks.DTOs.Common;
using InsightWorks.DTOs.Equipment;
using InsightWorks.DTOs.Statistics;
using InsightWorks.Models;
using Microsoft.EntityFrameworkCore;

namespace InsightWorks.Services;

public class StatisticsService : IStatisticsService
{
    private readonly AppDbContext _context;
    private readonly ILogger<StatisticsService> _logger;

    public StatisticsService(AppDbContext context, ILogger<StatisticsService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PaginatedList<EquipmentStatusHistory>> QueryStatusHistoryAsync(StatusHistoryQueryDTO query)
    {
        var queryable = _context.EquipmentStatusHistories
            .Include(h => h.Equipment)
                .ThenInclude(e => e.Manufacturer)
            .AsQueryable();

        // 应用查询条件
        if (query.EquipmentId.HasValue)
        {
            queryable = queryable.Where(h => h.EquipmentId == query.EquipmentId);
        }

        if (!string.IsNullOrWhiteSpace(query.EquipmentCode))
        {
            queryable = queryable.Where(h => h.Equipment.EquipmentCode.Contains(query.EquipmentCode));
        }

        if (query.Status.HasValue)
        {
            queryable = queryable.Where(h => h.Status == query.Status);
        }

        if (query.StartTime.HasValue)
        {
            queryable = queryable.Where(h => h.StatusChangeTime >= query.StartTime);
        }

        if (query.EndTime.HasValue)
        {
            queryable = queryable.Where(h => h.StatusChangeTime <= query.EndTime);
        }

        if (!string.IsNullOrWhiteSpace(query.ExecutedBy))
        {
            queryable = queryable.Where(h => h.ExecutedBy!.Contains(query.ExecutedBy));
        }

        // 按时间倒序排序
        queryable = queryable.OrderByDescending(h => h.StatusChangeTime);

        // 执行分页查询
        var records = await queryable
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        // 获取总记录数
        var totalCount = await queryable.CountAsync();

        return new PaginatedList<EquipmentStatusHistory>(records, totalCount, query.PageIndex, query.PageSize);
    }

    public async Task<ProductionRecordPagedResult> QueryProductionRecordsAsync(ProductionRecordQueryDTO query)
    {
        var queryable = _context.ProductionRecords
            .Include(p => p.Equipment)
                .ThenInclude(e => e.Manufacturer)
            .Include(p => p.ProductModel)
            .AsQueryable();

        // 应用查询条件
        if (query.EquipmentId.HasValue)
        {
            queryable = queryable.Where(p => p.EquipmentId == query.EquipmentId);
        }

        if (!string.IsNullOrWhiteSpace(query.EquipmentCode))
        {
            queryable = queryable.Where(p => p.Equipment.EquipmentCode.Contains(query.EquipmentCode));
        }

        if (query.ProductModelId.HasValue)
        {
            queryable = queryable.Where(p => p.ProductModelId == query.ProductModelId);
        }

        if (!string.IsNullOrWhiteSpace(query.ModelCode))
        {
            queryable = queryable.Where(p => p.ProductModel.ModelCode.Contains(query.ModelCode));
        }

        if (!string.IsNullOrWhiteSpace(query.BatchNumber))
        {
            queryable = queryable.Where(p => p.BatchNumber.Contains(query.BatchNumber));
        }

        if (query.StartTime.HasValue)
        {
            queryable = queryable.Where(p => p.ProductionStartTime >= query.StartTime);
        }

        if (query.EndTime.HasValue)
        {
            queryable = queryable.Where(p => p.ProductionEndTime <= query.EndTime);
        }

        // 只在需要时计算平均生产时间
        double avgProductionTime = 0;
        if (query.CalculateAvgTime)
        {
            var times = await queryable
                .Select(p => new { p.ProductionStartTime, p.ProductionEndTime })
                .ToListAsync();

            avgProductionTime = times.Any()
                ? times.Average(t => (t.ProductionEndTime - t.ProductionStartTime).TotalSeconds)
                : 0;
        }

        // 按生产开始时间倒序排序
        queryable = queryable.OrderByDescending(p => p.ProductionStartTime);

        // 执行分页查询
        var records = await queryable
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new ProductionRecordDTO
            {
                Id = p.Id,
                EquipmentId = p.EquipmentId,
                EquipmentCode = p.Equipment.EquipmentCode,
                ManufacturerName = p.Equipment.Manufacturer.Name,
                ProductModelId = p.ProductModelId,
                ModelCode = p.ProductModel.ModelCode,
                BatchNumber = p.BatchNumber,
                PreLength = p.PreLength,
                PreWidth = p.PreWidth,
                PreHeight = p.PreHeight,
                PostLength = p.PostLength,
                PostWidth = p.PostWidth,
                PostHeight = p.PostHeight,
                ProductionStartTime = p.ProductionStartTime,
                ProductionEndTime = p.ProductionEndTime
            })
            .ToListAsync();

        // 获取总记录数
        var totalCount = await queryable.CountAsync();

        var summary = new ProductionSummary
        {
            AvgProductionTime = Math.Round(avgProductionTime, 2) // 保留2位小数
        };

        return new ProductionRecordPagedResult(records, totalCount, query.PageIndex, query.PageSize, summary);
    }

    public async Task<List<GanttChartDTO>> GetGanttChartDataAsync(GanttChartQueryDTO query)
    {
        // 查询设备列表
        var equipmentQuery = _context.Equipment
            .Include(e => e.Manufacturer)
            .AsQueryable();

        if (query.EquipmentId.HasValue)
        {
            equipmentQuery = equipmentQuery.Where(e => e.Id == query.EquipmentId);
        }
        else if (!string.IsNullOrWhiteSpace(query.EquipmentCode))
        {
            equipmentQuery = equipmentQuery.Where(e => e.EquipmentCode == query.EquipmentCode);
        }

        var equipments = await equipmentQuery.ToListAsync();
        if (!equipments.Any())
        {
            throw new KeyNotFoundException("未找到指定的设备");
        }

        var result = new List<GanttChartDTO>();

        foreach (var equipment in equipments)
        {
            // 查询机况记录
            var statusQuery = _context.EquipmentStatusHistories
                .Where(h => h.EquipmentId == equipment.Id);

            if (query.StartTime.HasValue)
            {
                statusQuery = statusQuery.Where(h => h.StatusChangeTime >= query.StartTime);
            }

            if (query.EndTime.HasValue)
            {
                statusQuery = statusQuery.Where(h => h.StatusChangeTime <= query.EndTime);
            }

            var statusRecords = await statusQuery
                .OrderBy(h => h.StatusChangeTime)
                .Select(h => new StatusRecord
                {
                    Status = h.Status,
                    StatusChangeTime = h.StatusChangeTime,
                    ExecutedBy = h.ExecutedBy
                })
                .ToListAsync();

            // 查询生产记录
            var productionQuery = _context.ProductionRecords
                .Include(p => p.ProductModel)
                .Where(p => p.EquipmentId == equipment.Id);

            if (query.StartTime.HasValue)
            {
                productionQuery = productionQuery.Where(p => p.ProductionStartTime >= query.StartTime);
            }

            if (query.EndTime.HasValue)
            {
                productionQuery = productionQuery.Where(p => p.ProductionEndTime <= query.EndTime);
            }

            var productionRecords = await productionQuery
                .OrderBy(p => p.ProductionStartTime)
                .Select(p => new ProductionGanttRecord
                {
                    ModelCode = p.ProductModel.ModelCode,
                    BatchNumber = p.BatchNumber,
                    ProductionStartTime = p.ProductionStartTime,
                    ProductionEndTime = p.ProductionEndTime
                })
                .ToListAsync();

            result.Add(new GanttChartDTO
            {
                Equipment = new EquipmentInfo
                {
                    Id = equipment.Id,
                    EquipmentCode = equipment.EquipmentCode,
                    ManufacturerName = equipment.Manufacturer.Name
                },
                StatusRecords = statusRecords,
                ProductionRecords = productionRecords
            });
        }

        return result;
    }
} 