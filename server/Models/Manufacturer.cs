using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InsightWorks.Models;

public class Manufacturer
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(50)]
    [Column("manufacturer_code")]
    public string ManufacturerCode { get; set; } = null!;
    
    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = null!;
    
    [MaxLength(200)]
    [Column("address")]
    public string? Address { get; set; }
    
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
    public virtual ICollection<Equipment>? Equipments { get; set; }
} 