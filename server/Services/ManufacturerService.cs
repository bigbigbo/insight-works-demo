using InsightWorks.Models;
using InsightWorks.DTOs.Manufacturer;
using InsightWorks.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace InsightWorks.Services;

public class ManufacturerService : IManufacturerService
{
    private readonly AppDbContext _context;

    public ManufacturerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<Manufacturer>> GetAllManufacturersAsync(PaginationQuery query)
    {
        var manufacturers = _context.Manufacturers
            .OrderBy(m => m.ManufacturerCode);

        var totalCount = await manufacturers.CountAsync();
        
        var items = await manufacturers
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return new PaginatedList<Manufacturer>(items, totalCount, query.PageIndex, query.PageSize);
    }

    public async Task<Manufacturer?> GetManufacturerByIdAsync(Guid id)
    {
        return await _context.Manufacturers.FindAsync(id);
    }

    public async Task<Manufacturer> CreateManufacturerAsync(CreateManufacturerDTO data)
    {
        // 检查厂商代码是否已存在
        if (await _context.Manufacturers.AnyAsync(m => m.ManufacturerCode == data.ManufacturerCode))
        {
            throw new InvalidOperationException("厂商代码已存在");
        }

        var manufacturer = new Manufacturer
        {
            ManufacturerCode = data.ManufacturerCode,
            Name = data.Name,
            Address = data.Address,
            ContactPerson = data.ContactPerson,
            ContactPhone = data.ContactPhone,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Manufacturers.Add(manufacturer);
        await _context.SaveChangesAsync();

        return manufacturer;
    }

    public async Task<Manufacturer> UpdateManufacturerAsync(UpdateManufacturerDTO data)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(data.Id) 
            ?? throw new KeyNotFoundException($"未找到ID为 {data.Id} 的厂商");

        // 检查更新后的厂商代码是否与其他厂商冲突
        if (await _context.Manufacturers.AnyAsync(m => 
            m.ManufacturerCode == data.ManufacturerCode && m.Id != data.Id))
        {
            throw new InvalidOperationException("厂商代码已存在");
        }

        manufacturer.ManufacturerCode = data.ManufacturerCode;
        manufacturer.Name = data.Name;
        manufacturer.Address = data.Address;
        manufacturer.ContactPerson = data.ContactPerson;
        manufacturer.ContactPhone = data.ContactPhone;
        manufacturer.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        
        return manufacturer;
    }

    public async Task DeleteManufacturerAsync(DeleteManufacturerDTO data)
    {
        var manufacturer = await _context.Manufacturers
            .Include(m => m.Equipments)
            .FirstOrDefaultAsync(m => m.Id == data.Id)
            ?? throw new KeyNotFoundException($"未找到ID为 {data.Id} 的厂商");

        // 检查是否存在关联的设备
        if (manufacturer.Equipments?.Any() == true)
        {
            throw new InvalidOperationException("无法删除已有关联设备的厂商");
        }

        _context.Manufacturers.Remove(manufacturer);
        await _context.SaveChangesAsync();
    }
} 