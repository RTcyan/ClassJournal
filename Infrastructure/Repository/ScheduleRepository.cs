using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class ScheduleRepository
{
	private readonly Context _context;

	public Context UnitOfWork
	{
		get
		{
			return _context;
		}
	}

	public ScheduleRepository(Context context)
	{
		_context = context ?? throw new ArgumentException(nameof(context));
	}

    // Получение всего расписание
    public List<Schedule> getAll()
    {
        return _context.Schedules
			.Include(it => it.Cabinet)
			.Include(it => it.Grade)
			.Include(it => it.Teacher)
				.ThenInclude(it => it.Discipline)
            .ToList();
    }

    // Получение записи в рассписании по Id
    public Schedule? getById(Guid id)
	{
		return _context.Schedules
            .Include(it => it.Cabinet)
            .Include(it => it.Grade)
            .Include(it => it.Teacher)
			.Where(it => it.Id == id)
            .FirstOrDefault();
	}

	// Добавление запись в расписание
	public Schedule Add(Schedule schedule)
	{
		Schedule newSchedule = _context.Schedules.Add(schedule).Entity;
		_context.SaveChanges();

        return newSchedule;
	}

    // Удаление расписания
    public void Delete(Schedule schedule)
	{
		_context.Schedules.Remove(schedule);
		_context.SaveChanges();
	}

	// Обновление расписания
	public Schedule Update(Schedule schedule)
	{
        Schedule? oldSchedule = getById(schedule.Id);
        if (oldSchedule == null)
        {
            throw new Exception("NotFound");
        }
        oldSchedule.Cabinet = schedule.Cabinet;
        oldSchedule.DateTime = schedule.DateTime;
        oldSchedule.Grade = schedule.Grade;
        oldSchedule.Teacher = schedule.Teacher;
        _context.SaveChanges();

        return oldSchedule;
	}
}


