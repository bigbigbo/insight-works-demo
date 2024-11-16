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
        return await _context.Equipment
            .Include(e => e.Manufacturer)
            .OrderBy(e => e.EquipmentCode)
            .ToListAsync();
    }

    public async Task<Equipment?> GetEquipmentByIdAsync(Guid id)
    {
        return await _context.Equipment
            .Include(e => e.Manufacturer)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Equipment> CreateEquipmentAsync(CreateEquipmentDTO data)
    {
        // 检查设备代码是否已存在
        if (await _context.Equipment.AnyAsync(e => e.EquipmentCode == data.EquipmentCode))
        {
            throw new InvalidOperationException("设备代码已存在");
        }

        // 检查厂商是否存在
        if (!await _context.Manufacturers.AnyAsync(m => m.Id == data.ManufacturerId))
        {
            throw new InvalidOperationException("指定的厂商不存在");
        }

        var equipment = new Equipment
        {
            EquipmentCode = data.EquipmentCode,
            ManufacturerId = data.ManufacturerId,
            ContactPerson = data.ContactPerson,
            ContactPhone = data.ContactPhone,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Equipment.Add(equipment);
        await _context.SaveChangesAsync();

        // 重新获取包含厂商信息的设备数据
        return await GetEquipmentByIdAsync(equipment.Id) ?? equipment;
    }

    public async Task<Equipment> UpdateEquipmentAsync(UpdateEquipmentDTO data)
    {
        var equipment = await _context.Equipment.FindAsync(data.Id) 
            ?? throw new KeyNotFoundException($"未找到ID为 {data.Id} 的设备");

        // 检查更新后的设备代码是否与其他设备冲突
        if (await _context.Equipment.AnyAsync(e => 
            e.EquipmentCode == data.EquipmentCode && e.Id != data.Id))
        {
            throw new InvalidOperationException("设备代码已存在");
        }

        // 检查厂商是否存在
        if (!await _context.Manufacturers.AnyAsync(m => m.Id == data.ManufacturerId))
        {
            throw new InvalidOperationException("指定的厂商不存在");
        }

        equipment.EquipmentCode = data.EquipmentCode;
        equipment.ManufacturerId = data.ManufacturerId;
        equipment.ContactPerson = data.ContactPerson;
        equipment.ContactPhone = data.ContactPhone;
        equipment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        
        // 重新获取包含厂商信息的设备数据
        return await GetEquipmentByIdAsync(equipment.Id) ?? equipment;
    }

    public async Task DeleteEquipmentAsync(Guid id)
    {
        var equipment = await _context.Equipment.FindAsync(id)
            ?? throw new KeyNotFoundException($"未找到ID为 {id} 的设备");

        _context.Equipment.Remove(equipment);
        await _context.SaveChangesAsync();
    }
} 