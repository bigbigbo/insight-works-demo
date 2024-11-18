using Microsoft.AspNetCore.Mvc;
using InsightWorks.Services;
using InsightWorks.DTOs.Equipment;
using InsightWorks.DTOs.Common;
using InsightWorks.DTOs.Statistics;
using InsightWorks.Models;

namespace InsightWorks.Controllers;

[ApiController]
[Route("statistics")]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    /// <summary>
    /// 分页查询设备状态历史
    /// </summary>
    [HttpGet("equipment/status-history")]
    public async Task<ActionResult<ApiResponse<PaginatedList<EquipmentStatusHistory>>>> QueryStatusHistory([FromQuery] StatusHistoryQueryDTO query)
    {
        try
        {
            var records = await _statisticsService.QueryStatusHistoryAsync(query);
            return Ok(ApiResponse<PaginatedList<EquipmentStatusHistory>>.Ok(records));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<PaginatedList<EquipmentStatusHistory>>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// 分页查询生产记录
    /// </summary>
    [HttpGet("production/records")]
    public async Task<ActionResult<ApiResponse<ProductionRecordPagedResult>>> QueryProductionRecords([FromQuery] ProductionRecordQueryDTO query)
    {
        try
        {
            var result = await _statisticsService.QueryProductionRecordsAsync(query);
            return Ok(ApiResponse<ProductionRecordPagedResult>.Ok(result));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<ProductionRecordPagedResult>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// 获取甘特图数据
    /// </summary>
    [HttpGet("gantt-chart")]
    public async Task<ActionResult<ApiResponse<List<GanttChartDTO>>>> GetGanttChartData([FromQuery] GanttChartQueryDTO query)
    {
        try
        {
            var data = await _statisticsService.GetGanttChartDataAsync(query);
            return Ok(ApiResponse<List<GanttChartDTO>>.Ok(data));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<List<GanttChartDTO>>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<List<GanttChartDTO>>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// 获取产品型号生产统计
    /// </summary>
    [HttpPost("product-model/stats")]
    public async Task<ActionResult<ApiResponse<List<ProductModelStatDTO>>>> GetProductModelStats([FromBody] ProductModelStatQueryDTO query)
    {
        try
        {
            var stats = await _statisticsService.GetProductModelStatsAsync(query);
            return Ok(ApiResponse<List<ProductModelStatDTO>>.Ok(stats));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<List<ProductModelStatDTO>>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<List<ProductModelStatDTO>>.Fail(ex.Message));
        }
    }
} 