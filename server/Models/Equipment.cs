using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace server.Models;

public class Equipment
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(50)]
    [Column("equipment_code")]
    public string EquipmentCode { get; set; } = null!;
    
    [Required]
    [MaxLength(50)]
    [Column("manufacturer")]
    public string Manufacturer { get; set; } = null!;
    
    [MaxLength(50)]
    [Column("contact_person")]
    public string? ContactPerson { get; set; }
    
    [MaxLength(20)]
    [Column("contact_phone")]
    public string? ContactPhone { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // 导航属性
    [JsonIgnore]
    public virtual ICollection<EquipmentStatusHistory>? StatusHistories { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<ProductionRecord>? ProductionRecords { get; set; }
} 