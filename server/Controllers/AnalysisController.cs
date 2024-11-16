using Microsoft.AspNetCore.Mvc;
using InsightWorks.Services;
using InsightWorks.DTOs;

namespace InsightWorks.Controllers;

[ApiController]
[Route("analysis")]
public class AnalysisController : ControllerBase
{
    private readonly IAnalysisService _analysisService;

    public AnalysisController(IAnalysisService analysisService)
    {
        _analysisService = analysisService;
    }

    [HttpGet("gantt-chart")]
    public async Task<ActionResult<List<GanttChartDTO>>> GetGanttChart(
        [FromQuery] DateTime startTime,
        [FromQuery] DateTime endTime,
        [FromQuery] string? equipmentCode = null)
    {
        try
        {
            var result = await _analysisService.GetEquipmentStatusGanttChart(startTime, endTime, equipmentCode);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("production-time")]
    public async Task<ActionResult<List<ProductionAnalysisDTO>>> GetProductionTimeAnalysis(
        [FromQuery] string timeUnit,
        [FromQuery] DateTime startTime,
        [FromQuery] DateTime endTime,
        [FromQuery] string? modelCode = null)
    {
        try
        {
            var result = await _analysisService.GetProductionTimeAnalysis(timeUnit, startTime, endTime, modelCode);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
} 