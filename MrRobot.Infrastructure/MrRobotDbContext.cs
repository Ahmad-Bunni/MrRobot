using Microsoft.EntityFrameworkCore;
using MrRobot.Domain.Entities;

namespace MrRobot.Infrastructure;

public class MrRobotDbContext : DbContext
{
    public MrRobotDbContext(DbContextOptions<MrRobotDbContext> options)
           : base(options) { }

    public DbSet<Robot> Robots { get; set; } = null!;
    public DbSet<ControlCenter> ControlCenters { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Robot>()
            .HasOne(p => p.ControlCenter)
            .WithMany(b => b.Robots);

        modelBuilder.Entity<Robot>().OwnsOne(x => x.AreaSize);

        modelBuilder.Entity<Robot>().Property(e => e.Id).ValueGeneratedNever();

        // initialize aggregate root
        modelBuilder.Entity<ControlCenter>().HasData(new ControlCenter { Id = Guid.NewGuid() });

        modelBuilder.Entity<ControlCenter>().Navigation(e => e.Robots).AutoInclude();
    }
}
