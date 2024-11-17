using InsightWorks.Models;
using InsightWorks.DTOs.Equipment;
using InsightWorks.DTOs.Common;

namespace InsightWorks.Services;

public interface IEquipmentService
{
    Task<PaginatedList<Equipment>> GetAllEquipmentsAsync(PaginationQuery query);
    Task<Equipment?> GetEquipmentByIdAsync(Guid id);
    Task<Equipment> CreateEquipmentAsync(CreateEquipmentDTO data);
    Task<Equipment> UpdateEquipmentAsync(UpdateEquipmentDTO data);
    Task DeleteEquipmentAsync(Guid id);
} 