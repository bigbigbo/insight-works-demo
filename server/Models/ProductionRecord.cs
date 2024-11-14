using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

public class ProductionRecord
{
    [Key]
    public int Id { get; set; }
    
    public int EquipmentId { get; set; }
    public int ProductModelId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string BatchNumber { get; set; } = null!; // 批次号
    
    // 制程前规格
    public decimal PreLength { get; set; }
    public decimal PreWidth { get; set; }
    public decimal PreHeight { get; set; }
    
    // 制程后规格
    public decimal PostLength { get; set; }
    public decimal PostWidth { get; set; }
    public decimal PostHeight { get; set; }
    
    public DateTime ProductionStartTime { get; set; }
    public DateTime ProductionEndTime { get; set; }
    
    // 导航属性
    [ForeignKey("EquipmentId")]
    public virtual Equipment Equipment { get; set; } = null!;
    
    [ForeignKey("ProductModelId")]
    public virtual ProductModel ProductModel { get; set; } = null!;
} 