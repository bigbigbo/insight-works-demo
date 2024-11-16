using InsightWorks.Models;
using InsightWorks.DTOs.Product;
using Microsoft.EntityFrameworkCore;

namespace InsightWorks.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
    {
        return await _context.ProductModels
            .OrderBy(p => p.ModelCode)
            .ToListAsync();
    }

    public async Task<ProductModel?> GetProductByIdAsync(Guid id)
    {
        return await _context.ProductModels.FindAsync(id);
    }

    public async Task<ProductModel> CreateProductAsync(CreateProductDTO data)
    {
        // 检查产品型号代码是否已存在
        if (await _context.ProductModels.AnyAsync(p => p.ModelCode == data.ModelCode))
        {
            throw new InvalidOperationException("产品型号代码已存在");
        }

        var product = new ProductModel
        {
            ModelCode = data.ModelCode,
            Description = data.Description,
            CreatedAt = DateTime.UtcNow
        };

        _context.ProductModels.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<ProductModel> UpdateProductAsync(UpdateProductDTO data)
    {
        var product = await _context.ProductModels.FindAsync(data.Id)
            ?? throw new KeyNotFoundException($"未找到ID为 {data.Id} 的产品型号");

        // 检查更新后的产品型号代码是否与其他产品冲突
        if (await _context.ProductModels.AnyAsync(p =>
            p.ModelCode == data.ModelCode && p.Id != data.Id))
        {
            throw new InvalidOperationException("产品型号代码已存在");
        }

        product.ModelCode = data.ModelCode;
        product.Description = data.Description;

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task DeleteProductAsync(DeleteProductDTO data)
    {
        var product = await _context.ProductModels
            .Include(p => p.ProductionRecords)
            .FirstOrDefaultAsync(p => p.Id == data.Id)
            ?? throw new KeyNotFoundException($"未找到ID为 {data.Id} 的产品型号");

        // 检查是否存在关联的生产记录
        if (product.ProductionRecords?.Any() == true)
        {
            throw new InvalidOperationException("无法删除已有生产记录的产品型号");
        }

        _context.ProductModels.Remove(product);
        await _context.SaveChangesAsync();
    }
} 