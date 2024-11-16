using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsightWorks.Models;

public class EquipmentStatusHistory
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("equipment_id")]
    public Guid EquipmentId { get; set; }
    
    [Required]
    [MaxLength(20)]
    [Column("status")]
    public string Status { get; set; } = null!;
    
    [Required]
    [Column("status_change_time")]
    public DateTime StatusChangeTime { get; set; }
    
    [MaxLength(50)]
    [Column("executed_by")]
    public string? ExecutedBy { get; set; }
    
    // 导航属性
    [ForeignKey("EquipmentId")]
    public virtual Equipment Equipment { get; set; } = null!;
} 