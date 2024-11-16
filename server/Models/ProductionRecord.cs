using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsightWorks.Models;

public class ProductionRecord
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("equipment_id")]
    public Guid EquipmentId { get; set; }
    
    [Column("product_model_id")]
    public int ProductModelId { get; set; }
    
    [Required]
    [MaxLength(100)]
    [Column("batch_number")]
    public string BatchNumber { get; set; } = null!;
    
    // 制程前规格
    [Column("pre_length")]
    public decimal PreLength { get; set; }
    
    [Column("pre_width")]
    public decimal PreWidth { get; set; }
    
    [Column("pre_height")]
    public decimal PreHeight { get; set; }
    
    // 制程后规格
    [Column("post_length")]
    public decimal PostLength { get; set; }
    
    [Column("post_width")]
    public decimal PostWidth { get; set; }
    
    [Column("post_height")]
    public decimal PostHeight { get; set; }
    
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