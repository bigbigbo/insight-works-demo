namespace InsightWorks.DTOs.Equipment;

public class CreateEquipmentDTO
{
    public string EquipmentCode { get; set; } = null!;
    public Guid ManufacturerId { get; set; }
    public string? ContactPerson { get; set; }
    public string? ContactPhone { get; set; }
} 