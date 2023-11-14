using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class MarkLogRepository
{
	private readonly Context _context;

	public Context UnitOfWork
	{
		get
		{
			return _context;
		}
	}

	public MarkLogRepository(Context context)
	{
		_context = context ?? throw new ArgumentException(nameof(context));
	}

    // Получение всех оценок
    public List<MarkLog> getAll()
    {
        return _context.MarkLogs.Include(it => it.ScheduleItem).ToList();
    }

    // Получение оценки по Id
    public MarkLog? getById(Guid id)
	{
		return _context.MarkLogs
			.Include(it => it.ScheduleItem)
			.Include(it => it.Student)
			.Where(it => it.Id == id)
			.FirstOrDefault();
	}

    // Получение оценки по Id ученику
    public MarkLog? getByStudentId(Guid id)
    {
        return _context.MarkLogs
			.Include(it => it.ScheduleItem)
			.Include(it => it.Student)
			.Where(it => it.Student.Id == id)
			.FirstOrDefault();
    }

    // Добавление оценки
    public MarkLog Add(MarkLog markLog)
	{
		return _context.MarkLogs.Add(markLog).Entity;
	}

    // Удаление оценки
    public void Delete(MarkLog markLog)
	{
		_context.MarkLogs.Remove(markLog);
	}

	// Обновление оценки
	public MarkLog Update(MarkLog markLog)
	{
		return _context.MarkLogs.Update(markLog).Entity;
	}
}


