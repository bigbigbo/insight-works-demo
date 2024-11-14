using Microsoft.EntityFrameworkCore;

namespace server.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Equipment> Equipment { get; set; } = null!;
    public DbSet<EquipmentStatusHistory> EquipmentStatusHistories { get; set; } = null!;
    public DbSet<ProductModel> ProductModels { get; set; } = null!;
    public DbSet<ProductionRecord> ProductionRecords { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 设置索引
        modelBuilder.Entity<Equipment>()
            .HasIndex(e => e.EquipmentCode)
            .IsUnique();

        modelBuilder.Entity<ProductModel>()
            .HasIndex(p => p.ModelCode)
            .IsUnique();

        modelBuilder.Entity<ProductionRecord>()
            .HasIndex(p => new { p.EquipmentId, p.BatchNumber });

        modelBuilder.Entity<EquipmentStatusHistory>()
            .HasIndex(e => new { e.EquipmentId, e.StatusChangeTime });
    }
} 