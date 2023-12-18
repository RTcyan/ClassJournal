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
        return _context.MarkLogs
			.Include(it => it.ScheduleItem)
				.ThenInclude(it => it.Cabinet)
			.Include(it => it.ScheduleItem)
				.ThenInclude(it => it.Grade)
			.Include(it => it.ScheduleItem)
                .ThenInclude(it => it.Teacher)
                    .ThenInclude(it => it.Discipline)
            .Include(it => it.Student)
			.ToList();
    }

    // Получение оценки по Id
    public MarkLog? getById(Guid id)
	{
		return _context.MarkLogs
             .Include(it => it.ScheduleItem)
                .ThenInclude(it => it.Cabinet)
            .Include(it => it.ScheduleItem)
                .ThenInclude(it => it.Grade)
            .Include(it => it.ScheduleItem)
                .ThenInclude(it => it.Teacher)
                    .ThenInclude(it => it.Discipline)
            .Include(it => it.Student)
			.Where(it => it.Id == id)
            .FirstOrDefault();
	}

    // Получение оценок по Id ученику
    public List<MarkLog> getByStudentId(Guid id)
    {
		return _context.MarkLogs
            .Include(it => it.ScheduleItem)
                .ThenInclude(it => it.Cabinet)
            .Include(it => it.ScheduleItem)
                .ThenInclude(it => it.Grade)
            .Include(it => it.ScheduleItem)
                .ThenInclude(it => it.Teacher)
                    .ThenInclude(it => it.Discipline)
            .Where(it => it.Student.Id == id)
			.ToList();
    }

    // Добавление оценки
    public MarkLog Add(MarkLog markLog)
	{
		MarkLog newMarkLog = _context.MarkLogs.Add(markLog).Entity;
		_context.SaveChanges();

        return getById(newMarkLog.Id)!;
	}

    // Удаление оценки
    public void Delete(MarkLog markLog)
	{
		_context.MarkLogs.Remove(markLog);
		_context.SaveChanges();
	}

	// Обновление оценки
	public MarkLog Update(MarkLog markLog)
	{
        MarkLog? oldMarkLog = getById(markLog.Id);
        if (oldMarkLog == null)
        {
            throw new Exception("NotFound");
        }
        oldMarkLog.ScheduleItem = markLog.ScheduleItem;
        oldMarkLog.Student = markLog.Student;
        oldMarkLog.value = markLog.value;
        _context.SaveChanges();

        return oldMarkLog;
	}
}


