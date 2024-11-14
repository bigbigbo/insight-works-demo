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

        // 配置表名
        modelBuilder.Entity<Equipment>().ToTable("equipment");
        modelBuilder.Entity<EquipmentStatusHistory>().ToTable("equipment_status_history");
        modelBuilder.Entity<ProductModel>().ToTable("product_model");
        modelBuilder.Entity<ProductionRecord>().ToTable("production_record");

        // 设置索引
        modelBuilder.Entity<Equipment>()
            .HasIndex(e => e.EquipmentCode)
            .IsUnique()
            .HasDatabaseName("ix_equipment_code");

        modelBuilder.Entity<ProductModel>()
            .HasIndex(p => p.ModelCode)
            .IsUnique()
            .HasDatabaseName("ix_model_code");

        modelBuilder.Entity<ProductionRecord>()
            .HasIndex(p => new { p.EquipmentId, p.BatchNumber })
            .HasDatabaseName("ix_equipment_batch");

        modelBuilder.Entity<EquipmentStatusHistory>()
            .HasIndex(e => new { e.EquipmentId, e.StatusChangeTime })
            .HasDatabaseName("ix_equipment_status_time");

        // 配置关系
        modelBuilder.Entity<EquipmentStatusHistory>()
            .HasOne(e => e.Equipment)
            .WithMany(e => e.StatusHistories)
            .HasForeignKey(e => e.EquipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductionRecord>()
            .HasOne(p => p.Equipment)
            .WithMany(e => e.ProductionRecords)
            .HasForeignKey(p => p.EquipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductionRecord>()
            .HasOne(p => p.ProductModel)
            .WithMany(m => m.ProductionRecords)
            .HasForeignKey(p => p.ProductModelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 