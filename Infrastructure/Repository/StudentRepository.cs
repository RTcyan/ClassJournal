using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class StudentRepository
{
	private readonly Context _context;

	public Context UnitOfWork
	{
		get
		{
			return _context;
		}
	}

	public StudentRepository(Context context)
	{
		_context = context ?? throw new ArgumentException(nameof(context));
	}

    // Получение всех студентов
    public List<Student> getAll()
    {
        return _context.Students.Include(it => it.Grade).ToList();
    }

    // Получение студента по Id
    public Student? getById(Guid id)
	{
		return _context.Students.Where(it => it.Id == id)
			.Include(it => it.Grade)
			.FirstOrDefault();
	}

	// Добавление студента
	public Student Add(Student student)
	{
		Student newStudent = _context.Students.Add(student).Entity;
		_context.SaveChanges();

        return newStudent;
	}

    // Удаление студента
    public void Delete(Student student)
	{
		_context.Students.Remove(student);
		_context.SaveChanges();
	}

	// Обновление студента
	public Student Update(Student student)
	{
        Student newStudent = _context.Students.Update(student).Entity;
        _context.SaveChanges();

        return newStudent;
	}
}


