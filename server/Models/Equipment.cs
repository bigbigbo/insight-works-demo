using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Equipment
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string EquipmentCode { get; set; } = null!; // 设备编号，如"智能A-1"
    
    [Required]
    [MaxLength(50)]
    public string Manufacturer { get; set; } = null!; // 制造商，如"厂商A"
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // 导航属性
    public virtual ICollection<EquipmentStatusHistory> StatusHistories { get; set; } = null!;
    public virtual ICollection<ProductionRecord> ProductionRecords { get; set; } = null!;
} 