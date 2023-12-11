using GameStore.game.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

public class DataContext : DbContext
{
    protected readonly IConfiguration configuration;

    public DataContext(IConfiguration _configuration, DbContextOptions<DataContext> options) : base(options)
    {
        configuration = _configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Game> games { get; set; }
}
