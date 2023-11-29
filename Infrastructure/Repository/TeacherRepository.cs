using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class TeacherRepository
{
	private readonly Context _context;

	public Context UnitOfWork
	{
		get
		{
			return _context;
		}
	}

	public TeacherRepository(Context context)
	{
		_context = context ?? throw new ArgumentException(nameof(context));
	}

    // Получение всех учителей
    public List<Teacher> getAll()
    {
        return _context.Teachers.Include(it => it.Discipline).ToList();
    }

    // Получение учителя по Id
    public Teacher? getById(Guid id)
	{
		return _context.Teachers.Include(it => it.Discipline).Where(it => it.Id == id).FirstOrDefault();
	}

	// Добавление учителя
	public Teacher Add(Teacher teacher)
	{
		Teacher newTeacher = _context.Teachers.Add(teacher).Entity;
		_context.SaveChanges();

        return newTeacher;
	}

    // Удаление учителя
    public void Delete(Teacher teacher)
	{
		_context.Teachers.Remove(teacher);
		_context.SaveChanges();
	}

	// Обновление учителя
	public Teacher Update(Teacher teacher)
	{
        Teacher? oldTeacher = getById(teacher.Id);
        if (oldTeacher == null)
        {
            throw new Exception("NotFound");
        }
        oldTeacher.FullName = teacher.FullName;
        oldTeacher.PersonalLifeNumber = teacher.PersonalLifeNumber;
        oldTeacher.PhoneNumber = teacher.PhoneNumber;
        oldTeacher.Birthday = teacher.Birthday;
        oldTeacher.Discipline = teacher.Discipline;
        _context.SaveChanges();

        return oldTeacher;
	}
}


