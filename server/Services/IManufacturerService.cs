using InsightWorks.Models;
using InsightWorks.DTOs.Manufacturer;
using InsightWorks.DTOs.Common;

namespace InsightWorks.Services;

public interface IManufacturerService
{
    Task<PaginatedList<Manufacturer>> GetAllManufacturersAsync(PaginationQuery query);
    Task<Manufacturer?> GetManufacturerByIdAsync(Guid id);
    Task<Manufacturer> CreateManufacturerAsync(CreateManufacturerDTO data);
    Task<Manufacturer> UpdateManufacturerAsync(UpdateManufacturerDTO data);
    Task DeleteManufacturerAsync(DeleteManufacturerDTO data);
} 