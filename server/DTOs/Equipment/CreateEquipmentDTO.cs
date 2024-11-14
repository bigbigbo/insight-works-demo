namespace server.DTOs;

public class CreateEquipmentDTO
{
    public string EquipmentCode { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public string? ContactPerson { get; set; }
    public string? ContactPhone { get; set; }
} 