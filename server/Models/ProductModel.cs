using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class ProductModel
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string ModelCode { get; set; } = null!; // 产品型号
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // 导航属性
    public virtual ICollection<ProductionRecord> ProductionRecords { get; set; } = null!;
} 