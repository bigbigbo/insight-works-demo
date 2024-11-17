using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsightWorks.Models;

public class ProductionRecord
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("equipment_id")]
    public Guid EquipmentId { get; set; }
    
    [Column("product_model_id")]
    public Guid ProductModelId { get; set; }
    
    [Required]
    [MaxLength(100)]
    [Column("batch_number")]
    public string BatchNumber { get; set; } = null!;
    
    // 制程前规格
    [Required]
    [MaxLength(50)]
    [Column("pre_length")]
    public string PreLength { get; set; } = null!;
    
    [Required]
    [MaxLength(50)]
    [Column("pre_width")]
    public string PreWidth { get; set; } = null!;
    
    [Required]
    [MaxLength(50)]
    [Column("pre_height")]
    public string PreHeight { get; set; } = null!;
    
    // 制程后规格
    [Required]
    [MaxLength(50)]
    [Column("post_length")]
    public string PostLength { get; set; } = null!;
    
    [Required]
    [MaxLength(50)]
    [Column("post_width")]
    public string PostWidth { get; set; } = null!;
    
    [Required]
    [MaxLength(50)]
    [Column("post_height")]
    public string PostHeight { get; set; } = null!;
    
    [Column("production_start_time")]
    public DateTime ProductionStartTime { get; set; }
    
    [Column("production_end_time")]
    public DateTime ProductionEndTime { get; set; }
    
    // 导航属性
    [ForeignKey("EquipmentId")]
    public virtual Equipment Equipment { get; set; } = null!;
    
    [ForeignKey("ProductModelId")]
    public virtual ProductModel ProductModel { get; set; } = null!;
} 