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
} 