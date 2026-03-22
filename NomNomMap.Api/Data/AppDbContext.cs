using NomNomMap.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace NomNomMap.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Place> Places => Set<Place>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Place>()
            .HasIndex(p => p.Location)
            .HasMethod("GIST");
    }
}
