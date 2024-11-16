namespace InsightWorks.DTOs.Equipment;

public class UpdateEquipmentDTO
{
    public Guid Id { get; set; }
    public string EquipmentCode { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public string? ContactPerson { get; set; }
    public string? ContactPhone { get; set; }
} 