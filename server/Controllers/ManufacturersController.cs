using Microsoft.AspNetCore.Mvc;
using InsightWorks.Services;
using InsightWorks.Models;
using InsightWorks.DTOs.Manufacturer;
using InsightWorks.DTOs.Common;

namespace InsightWorks.Controllers;

[ApiController]
[Route("manufacturers")]
public class ManufacturersController : ControllerBase
{
    private readonly IManufacturerService _manufacturerService;

    public ManufacturersController(IManufacturerService manufacturerService)
    {
        _manufacturerService = manufacturerService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<Manufacturer>>>> GetAllManufacturers()
    {
        try
        {
            var manufacturers = await _manufacturerService.GetAllManufacturersAsync();
            return Ok(ApiResponse<IEnumerable<Manufacturer>>.Ok(manufacturers));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<IEnumerable<Manufacturer>>.Fail(ex.Message));
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<Manufacturer>>> GetManufacturerById(Guid id)
    {
        try
        {
            var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(id);
            if (manufacturer == null)
            {
                return NotFound(ApiResponse<Manufacturer>.Fail($"未找到ID为 {id} 的厂商"));
            }
            return Ok(ApiResponse<Manufacturer>.Ok(manufacturer));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<Manufacturer>.Fail(ex.Message));
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult<ApiResponse<Manufacturer>>> CreateManufacturer([FromBody] CreateManufacturerDTO data)
    {
        try
        {
            var manufacturer = await _manufacturerService.CreateManufacturerAsync(data);
            return Ok(ApiResponse<Manufacturer>.Ok(manufacturer, "厂商创建成功"));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<Manufacturer>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<Manufacturer>.Fail(ex.Message));
        }
    }

    [HttpPost("update")]
    public async Task<ActionResult<ApiResponse<Manufacturer>>> UpdateManufacturer([FromBody] UpdateManufacturerDTO data)
    {
        try
        {
            var manufacturer = await _manufacturerService.UpdateManufacturerAsync(data);
            return Ok(ApiResponse<Manufacturer>.Ok(manufacturer, "厂商更新成功"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<Manufacturer>.Fail(ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<Manufacturer>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<Manufacturer>.Fail(ex.Message));
        }
    }

    [HttpPost("delete")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteManufacturer([FromBody] DeleteManufacturerDTO data)
    {
        try
        {
            await _manufacturerService.DeleteManufacturerAsync(data);
            return Ok(ApiResponse<object>.Ok(null, "厂商删除成功"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<object>.Fail(ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<object>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.Fail(ex.Message));
        }
    }
} 