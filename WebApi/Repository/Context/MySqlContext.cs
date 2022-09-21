using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Repository.Context;

public class MySqlContext : DbContext
{
#nullable disable
    public virtual DbSet<Person> People { get; set; }
    public virtual DbSet<User> User { get; set; }
#nullable enable

    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().ToTable("People");
    }
}