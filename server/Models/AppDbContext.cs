using Microsoft.EntityFrameworkCore;

namespace YourNamespace.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // 在这里添加你的DbSet属性
    // public DbSet<User> Users { get; set; }
} 