using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

public class EquipmentStatusHistory
{
    [Key]
    public int Id { get; set; }
    
    public int EquipmentId { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = null!; // 机况：正常、异常、警告
    
    [Required]
    public DateTime StatusChangeTime { get; set; } // 状态切换时间
    
    [MaxLength(50)]
    public string? ExecutedBy { get; set; } // 执行切换对象（人员/机台自身）
    
    // 导航属性
    [ForeignKey("EquipmentId")]
    public virtual Equipment Equipment { get; set; } = null!;
} 