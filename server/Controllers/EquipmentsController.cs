using Microsoft.AspNetCore.Mvc;
using InsightWorks.Models;
using InsightWorks.DTOs.Common;
using InsightWorks.DTOs.Equipment;
using InsightWorks.Services;

namespace InsightWorks.Controllers;

[ApiController]
[Route("equipments")]
public class EquipmentsController : ControllerBase
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentsController(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<Equipment>>>> GetEquipments()
    {
        var equipments = await _equipmentService.GetAllEquipmentsAsync();
        return Ok(ApiResponse<IEnumerable<Equipment>>.Ok(equipments));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<Equipment>>> GetEquipment(Guid id)
    {
        var equipment = await _equipmentService.GetEquipmentByIdAsync(id);

        if (equipment == null)
        {
            return NotFound(ApiResponse<Equipment>.Fail($"Equipment with ID {id} not found"));
        }

        return Ok(ApiResponse<Equipment>.Ok(equipment));
    }

    [HttpPost("create")]
    public async Task<ActionResult<ApiResponse<Equipment>>> CreateEquipment([FromBody] CreateEquipmentDTO data)
    {
        try 
        {
            var equipment = await _equipmentService.CreateEquipmentAsync(data);
            return Ok(ApiResponse<Equipment>.Ok(equipment, "Equipment created successfully"));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<Equipment>.Fail(ex.Message));
        }
    }

    [HttpPost("update")]
    public async Task<ActionResult<ApiResponse<Equipment>>> UpdateEquipment([FromBody] UpdateEquipmentDTO data)
    {
        try
        {
            var equipment = await _equipmentService.UpdateEquipmentAsync(data);
            return Ok(ApiResponse<Equipment>.Ok(equipment, "Equipment updated successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<Equipment>.Fail(ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<Equipment>.Fail(ex.Message));
        }
    }

    [HttpPost("delete")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteEquipment([FromBody] DeleteEquipmentDTO data)
    {
        try
        {
            await _equipmentService.DeleteEquipmentAsync(data.Id);
            return Ok(ApiResponse<object>.Ok(null, "Equipment deleted successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<object>.Fail(ex.Message));
        }
    }
} 