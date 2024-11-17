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
} 