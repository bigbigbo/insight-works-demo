using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;
using server.DTOs;
using server.DTOs.Common;
using server.DTOs.Equipment;

namespace server.Controllers;

[ApiController]
[Route("equipments")]
public class EquipmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EquipmentsController(AppDbContext context)
    {
        _context = context;
    }

    // 获取所有设备
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<Equipment>>>> GetEquipments()
    {
        var equipments = await _context.Equipment.ToListAsync();
        return Ok(ApiResponse<IEnumerable<Equipment>>.Ok(equipments));
    }

    // 获取单个设备
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<Equipment>>> GetEquipment(Guid id)
    {
        var equipment = await _context.Equipment.FindAsync(id);

        if (equipment == null)
        {
            return NotFound(ApiResponse<Equipment>.Fail($"Equipment with ID {id} not found"));
        }

        return Ok(ApiResponse<Equipment>.Ok(equipment));
    }

    // 创建设备
    [HttpPost("create")]
    public async Task<ActionResult<ApiResponse<Equipment>>> CreateEquipment([FromBody] CreateEquipmentDTO data)
    {
        try 
        {
            var equipment = new Equipment
            {
                EquipmentCode = data.EquipmentCode,
                Manufacturer = data.Manufacturer,
                ContactPerson = data.ContactPerson,
                ContactPhone = data.ContactPhone,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Equipment.Add(equipment);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<Equipment>.Ok(equipment, "Equipment created successfully"));
        }
        catch (DbUpdateException)
        {
            if (await _context.Equipment.AnyAsync(e => e.EquipmentCode == data.EquipmentCode))
            {
                return Conflict(ApiResponse<Equipment>.Fail("Equipment code already exists"));
            }
            throw;
        }
    }

    // 更新设备
    [HttpPost("update")]
    public async Task<ActionResult<ApiResponse<Equipment>>> UpdateEquipment([FromBody] UpdateEquipmentDTO data)
    {
        var equipment = await _context.Equipment.FindAsync(data.Id);
        
        if (equipment == null)
        {
            return NotFound(ApiResponse<Equipment>.Fail($"Equipment with ID {data.Id} not found"));
        }

        try
        {
            equipment.EquipmentCode = data.EquipmentCode;
            equipment.Manufacturer = data.Manufacturer;
            equipment.ContactPerson = data.ContactPerson;
            equipment.ContactPhone = data.ContactPhone;
            equipment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            
            return Ok(ApiResponse<Equipment>.Ok(equipment, "Equipment updated successfully"));
        }
        catch (DbUpdateException)
        {
            if (await _context.Equipment.AnyAsync(e => e.EquipmentCode == data.EquipmentCode && e.Id != data.Id))
            {
                return Conflict(ApiResponse<Equipment>.Fail("Equipment code already exists"));
            }
            throw;
        }
    }

    // 删除设备
    [HttpPost("delete")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteEquipment([FromBody] DeleteEquipmentDTO data)
    {
        var equipment = await _context.Equipment.FindAsync(data.Id);
        if (equipment == null)
        {
            return NotFound(ApiResponse<object>.Fail($"Equipment with ID {data.Id} not found"));
        }

        _context.Equipment.Remove(equipment);
        await _context.SaveChangesAsync();

        return Ok(ApiResponse<object>.Ok(null, "Equipment deleted successfully"));
    }

    private async Task<bool> EquipmentExists(Guid id)
    {
        return await _context.Equipment.AnyAsync(e => e.Id == id);
    }
} 