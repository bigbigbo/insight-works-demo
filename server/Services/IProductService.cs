using InsightWorks.Models;
using InsightWorks.DTOs.Product;
using InsightWorks.DTOs.Common;

namespace InsightWorks.Services;

public interface IProductService
{
    Task<PaginatedList<ProductModel>> GetAllProductsAsync(PaginationQuery query);
    Task<ProductModel?> GetProductByIdAsync(Guid id);
    Task<ProductModel> CreateProductAsync(CreateProductDTO data);
    Task<ProductModel> UpdateProductAsync(UpdateProductDTO data);
    Task DeleteProductAsync(DeleteProductDTO data);
} 