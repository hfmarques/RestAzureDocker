using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Repository.Context;

public class SqlServerContext : DbContext
{
#nullable disable
    public virtual DbSet<Person> People { get; set; }
#nullable enable

    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().ToTable("People");
    }
}