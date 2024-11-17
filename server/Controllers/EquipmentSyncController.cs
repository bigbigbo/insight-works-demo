using Microsoft.AspNetCore.Mvc;
using InsightWorks.Services;
using InsightWorks.DTOs.EquipmentSync;
using InsightWorks.DTOs.Common;
using InsightWorks.Models;

namespace InsightWorks.Controllers;

[ApiController]
[Route("equipment-sync")]
public class EquipmentSyncController : ControllerBase
{
    private readonly IEquipmentSyncService _equipmentSyncService;

    public EquipmentSyncController(IEquipmentSyncService equipmentSyncService)
    {
        _equipmentSyncService = equipmentSyncService;
    }

    /// <summary>
    /// 发送同步命令给设备
    /// </summary>
    [HttpPost("command")]
    public async Task<ActionResult<ApiResponse<object>>> SendSyncCommand([FromBody] SyncCommandDTO command)
    {
        try
        {
            await _equipmentSyncService.SendSyncCommandAsync(command);
            return Ok(ApiResponse<object>.Ok(null, "同步命令已发送"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<object>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// 上传机况数据
    /// </summary>
    [HttpPost("upload/status")]
    public async Task<ActionResult<ApiResponse<object>>> UploadStatusData([FromBody] UploadStatusDataDTO data)
    {
        try
        {
            await _equipmentSyncService.UploadStatusDataAsync(data);
            return Ok(ApiResponse<object>.Ok(null, "机况数据上传成功"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<object>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// 上传生产数据
    /// </summary>
    [HttpPost("upload/production")]
    public async Task<ActionResult<ApiResponse<object>>> UploadProductionData([FromBody] UploadProductionDataDTO data)
    {
        try
        {
            await _equipmentSyncService.UploadProductionDataAsync(data);
            return Ok(ApiResponse<object>.Ok(null, "生产数据上传成功"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<object>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// 上传Excel数据
    /// </summary>
    [HttpPost("upload/excel")]
    public async Task<ActionResult<ApiResponse<object>>> UploadExcelData([FromForm] UploadExcelDTO data)
    {
        try
        {
            await _equipmentSyncService.UploadExcelDataAsync(data);
            return Ok(ApiResponse<object>.Ok(null, "Excel数据上传成功"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<object>.Fail(ex.Message));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse<object>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// 分页查询同步记录
    /// </summary>
    [HttpGet("records")]
    public async Task<ActionResult<ApiResponse<PaginatedList<EquipmentSyncRecord>>>> QuerySyncRecords([FromQuery] SyncRecordQueryDTO query)
    {
        try
        {
            var records = await _equipmentSyncService.QuerySyncRecordsAsync(query);
            return Ok(ApiResponse<PaginatedList<EquipmentSyncRecord>>.Ok(records));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<PaginatedList<EquipmentSyncRecord>>.Fail(ex.Message));
        }
    }
} 