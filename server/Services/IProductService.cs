using InsightWorks.Models;
using InsightWorks.DTOs.Product;

namespace InsightWorks.Services;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> GetAllProductsAsync();
    Task<ProductModel?> GetProductByIdAsync(Guid id);
    Task<ProductModel> CreateProductAsync(CreateProductDTO data);
    Task<ProductModel> UpdateProductAsync(UpdateProductDTO data);
    Task DeleteProductAsync(DeleteProductDTO data);
} 