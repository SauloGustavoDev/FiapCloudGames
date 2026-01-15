using Domain.Entities;
using Infrastructure.Mappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;
    public class ContextDb : IdentityDbContext<User>
{
    public DbSet<Game> Game { get; set; }
    public ContextDb(DbContextOptions<ContextDb> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.ApplyConfiguration(new GameMapping());
    }
}

