using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsightWorks.Models;

public class EquipmentSyncRecord
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("equipment_id")]
    public Guid EquipmentId { get; set; }
    
    [Required]
    [MaxLength(20)]
    [Column("sync_type")]
    public string SyncType { get; set; } = null!;  // Status/Production
    
    [Column("sync_start_time")]
    public DateTime SyncStartTime { get; set; }
    
    [Column("sync_end_time")]
    public DateTime? SyncEndTime { get; set; }
    
    [Required]
    [MaxLength(20)]
    [Column("status")]
    public string Status { get; set; } = "Failed";  // Success/Failed
    
    [Column("error_message")]
    public string? ErrorMessage { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // 导航属性
    [ForeignKey("EquipmentId")]
    public virtual Equipment Equipment { get; set; } = null!;
} 