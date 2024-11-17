using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InsightWorks.Models.Enums;

namespace InsightWorks.Models;

public class EquipmentStatusHistory
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("equipment_id")]
    public Guid EquipmentId { get; set; }
    
    [Required]
    [Column("status")]
    public EquipmentStatus Status { get; set; }
    
    [Required]
    [Column("status_change_time")]
    public DateTime StatusChangeTime { get; set; }
    
    [MaxLength(50)]
    [Column("executed_by")]
    public string? ExecutedBy { get; set; }
    
    [ForeignKey("EquipmentId")]
    public virtual Equipment Equipment { get; set; } = null!;
} 