using InsightWorks.Models;
using InsightWorks.DTOs.Equipment;

namespace InsightWorks.Services;

public interface IEquipmentService
{
    Task<IEnumerable<Equipment>> GetAllEquipmentsAsync();
    Task<Equipment?> GetEquipmentByIdAsync(Guid id);
    Task<Equipment> CreateEquipmentAsync(CreateEquipmentDTO data);
    Task<Equipment> UpdateEquipmentAsync(UpdateEquipmentDTO data);
    Task DeleteEquipmentAsync(Guid id);
} 