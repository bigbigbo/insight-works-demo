using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InsightWorks.Models.Enums;

namespace InsightWorks.Models;

public class EquipmentSyncRecord
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("equipment_id")]
    public Guid EquipmentId { get; set; }
    
    [Required]
    [Column("sync_type")]
    public SyncType SyncType { get; set; }
    
    [Column("sync_start_time")]
    public DateTime SyncStartTime { get; set; }
    
    [Column("sync_end_time")]
    public DateTime? SyncEndTime { get; set; }
    
    [Required]
    [Column("status")]
    public SyncStatus Status { get; set; } = SyncStatus.Failed;
    
    [Column("error_message")]
    public string? ErrorMessage { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("EquipmentId")]
    public virtual Equipment Equipment { get; set; } = null!;
} 