using Microsoft.EntityFrameworkCore;
using MyNumsApi.Models;

namespace MyNumsApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Num> Nums{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Num>().HasKey(n => n.Number);
        base.OnModelCreating(modelBuilder);
    }
}
