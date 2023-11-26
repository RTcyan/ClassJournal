using Domain.Model;
using Infrastructure.Repository;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Tests;
public class TestHelper
{
    private Context _context;

    public TestHelper()
    {
        string dbName = $"TestDb_{DateTime.Now.ToFileTimeUtc()}";
        var dbContextOptions = new DbContextOptionsBuilder<Context>()
            .UseNpgsql("Server=127.0.0.1;Database=TestDb;Username=postgres;Password=postgres")
            .Options;

        _context = new Context(dbContextOptions);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        Grade grade = new Grade
        {
            Name = "Test",
            GradeTeacher = new Teacher
            {
                FullName = "Test",
                Birthday = DateTime.UtcNow,
                Discipline = new Discipline
                {
                    Name = "Test",
                },
                PersonalLifeNumber = 123,
                PhoneNumber = "Test",
            },
            GradeType = new GradeType
            {
                Name = "Test",
            },
        };

        _context.Grades.Add(grade);
        _context.SaveChanges();

        // Запрещаем отслеживание (разрываем связи с БД)
        _context.ChangeTracker.Clear();
    }

    public void ChangeTrackerClear()
    {
        _context.ChangeTracker.Clear();
    }

    public GradeRepository GradeRepository
    {
        get
        {
            return new GradeRepository(_context);
        }
    }
}


