using Microsoft.EntityFrameworkCore;
using InsightWorks.Models;
using InsightWorks.DTOs.Equipment;

namespace InsightWorks.Services;

public class EquipmentService : IEquipmentService
{
    private readonly AppDbContext _context;

    public EquipmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Equipment>> GetAllEquipmentsAsync()
    {
        return await _context.Equipment.ToListAsync();
    }

    public async Task<Equipment?> GetEquipmentByIdAsync(Guid id)
    {
        return await _context.Equipment.FindAsync(id);
    }

    public async Task<Equipment> CreateEquipmentAsync(CreateEquipmentDTO data)
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

        // 检查设备代码是否已存在
        if (await _context.Equipment.AnyAsync(e => e.EquipmentCode == data.EquipmentCode))
        {
            throw new InvalidOperationException("Equipment code already exists");
        }

        _context.Equipment.Add(equipment);
        await _context.SaveChangesAsync();

        return equipment;
    }

    public async Task<Equipment> UpdateEquipmentAsync(UpdateEquipmentDTO data)
    {
        var equipment = await _context.Equipment.FindAsync(data.Id) 
            ?? throw new KeyNotFoundException($"Equipment with ID {data.Id} not found");

        // 检查更新后的设备代码是否与其他设备冲突
        if (await _context.Equipment.AnyAsync(e => e.EquipmentCode == data.EquipmentCode && e.Id != data.Id))
        {
            throw new InvalidOperationException("Equipment code already exists");
        }

        equipment.EquipmentCode = data.EquipmentCode;
        equipment.Manufacturer = data.Manufacturer;
        equipment.ContactPerson = data.ContactPerson;
        equipment.ContactPhone = data.ContactPhone;
        equipment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        
        return equipment;
    }

    public async Task DeleteEquipmentAsync(Guid id)
    {
        var equipment = await _context.Equipment.FindAsync(id)
            ?? throw new KeyNotFoundException($"Equipment with ID {id} not found");

        _context.Equipment.Remove(equipment);
        await _context.SaveChangesAsync();
    }
} 