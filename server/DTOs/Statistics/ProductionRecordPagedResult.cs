using InsightWorks.DTOs.Common;
using InsightWorks.Models;

namespace InsightWorks.DTOs.Statistics;

/// <summary>
/// 生产记录分页查询结果
/// </summary>
public class ProductionRecordPagedResult
{
    /// <summary>
    /// 当前页数据
    /// </summary>
    public List<ProductionRecordDTO> Items { get; set; } = null!;

    /// <summary>
    /// 总记录数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 当前页码
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 总页数
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    /// <summary>
    /// 是否有上一页
    /// </summary>
    public bool HasPreviousPage => PageIndex > 1;

    /// <summary>
    /// 是否有下一页
    /// </summary>
    public bool HasNextPage => PageIndex < TotalPages;

    /// <summary>
    /// 统计信息
    /// </summary>
    public ProductionSummary Summary { get; set; } = null!;

    public ProductionRecordPagedResult(
        List<ProductionRecordDTO> items, 
        int totalCount, 
        int pageIndex, 
        int pageSize,
        ProductionSummary summary)
    {
        Items = items;
        TotalCount = totalCount;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Summary = summary;
    }
}

/// <summary>
/// 生产统计信息
/// </summary>
public class ProductionSummary
{
    /// <summary>
    /// 平均生产时间(小时)
    /// </summary>
    public double AvgProductionTime { get; set; }
} 