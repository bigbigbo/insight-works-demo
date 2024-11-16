using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsightWorks.Models;

public class ProductModel
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    [Column("model_code")]
    public string ModelCode { get; set; } = null!;
    
    [MaxLength(200)]
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // 导航属性
    public virtual ICollection<ProductionRecord> ProductionRecords { get; set; } = null!;
} 