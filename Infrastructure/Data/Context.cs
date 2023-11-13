using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class Context: DbContext
{
    public DbSet<Domain.Model.Administrator> Administrators { get; set; }
    public DbSet<Domain.Model.CabinetType> CabinetTypes { get; set; }
    public DbSet<Domain.Model.GradeType> GradeTypes { get; set; }
    public DbSet<Domain.Model.Cabinet> Cabinets { get; set; }
    public DbSet<Domain.Model.Discipline> Disciplines { get; set; }
    public DbSet<Domain.Model.Teacher> Teachers { get; set; }
    public DbSet<Domain.Model.Grade> Grades { get; set; }
    public DbSet<Domain.Model.Student> Students { get; set; }
    public DbSet<Domain.Model.Schedule> Schedules { get; set; }
    public DbSet<Domain.Model.MarkLog> MarkLogs { get; set; }

    public Context(DbContextOptions<Context> options): base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        base.OnModelCreating(modelBuilder);
    }
}

