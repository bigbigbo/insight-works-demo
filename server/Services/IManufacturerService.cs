using InsightWorks.Models;
using InsightWorks.DTOs.Manufacturer;

namespace InsightWorks.Services;

public interface IManufacturerService
{
    Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync();
    Task<Manufacturer?> GetManufacturerByIdAsync(Guid id);
    Task<Manufacturer> CreateManufacturerAsync(CreateManufacturerDTO data);
    Task<Manufacturer> UpdateManufacturerAsync(UpdateManufacturerDTO data);
    Task DeleteManufacturerAsync(DeleteManufacturerDTO data);
} 