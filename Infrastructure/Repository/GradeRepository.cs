using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class GradeRepository
{
	private readonly Context _context;

	public Context UnitOfWork
	{
		get
		{
			return _context;
		}
	}

	public GradeRepository(Context context)
	{
		_context = context ?? throw new ArgumentException(nameof(context));
	}

    // Получение всех классов
    public List<Grade> getAll()
    {
        return _context.Grades.Include(it => it.GradeTeacher).Include(it => it.GradeType).ToList();
    }

    // Получение класса по Id
    public Grade? getById(Guid id)
	{
		return _context.Grades.Include(it => it.GradeTeacher).Include(it => it.GradeType).Where(it => it.Id == id).FirstOrDefault();
	}

	// Добавление класса
	public Grade Add(Grade grade)
	{
		return _context.Grades.Add(grade).Entity;
	}

    // Удаление класса
    public void Delete(Grade grade)
	{
		_context.Grades.Remove(grade);
	}

	// Обновление класса
	public Grade Update(Grade grade)
	{
		return _context.Grades.Update(grade).Entity;
	}
}


