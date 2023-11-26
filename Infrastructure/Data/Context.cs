using Microsoft.EntityFrameworkCore;
using Domain.Model;

namespace Infrastructure;

public class Context: DbContext
{
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<CabinetType> CabinetTypes { get; set; }
    public DbSet<GradeType> GradeTypes { get; set; }
    public DbSet<Cabinet> Cabinets { get; set; }
    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<MarkLog> MarkLogs { get; set; }

    public Context(DbContextOptions<Context> options): base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Administrator>()
            .Property(b => b.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        modelBuilder.Entity<Cabinet>()
            .Property(b => b.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        modelBuilder.Entity<CabinetType>()
            .Property(b => b.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        modelBuilder.Entity<Discipline>()
            .Property(b => b.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        modelBuilder.Entity<Grade>()
            .Property(b => b.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        modelBuilder.Entity<GradeType>()
            .Property(b => b.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        modelBuilder.Entity<MarkLog>()
            .Property(b => b.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        modelBuilder.Entity<Schedule>()
            .Property(b => b.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        modelBuilder.Entity<Student>()
            .Property(b => b.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        modelBuilder.Entity<Teacher>()
            .Property(b => b.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        base.OnModelCreating(modelBuilder);
    }
}

