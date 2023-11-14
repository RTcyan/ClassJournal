using Domain.Model;

namespace Infrastructure.Repository;
public class DisciplineRepository
{
	private readonly Context _context;

	public Context UnitOfWork
	{
		get
		{
			return _context;
		}
	}

	public DisciplineRepository(Context context)
	{
		_context = context ?? throw new ArgumentException(nameof(context));
	}

	// Получение всех дисциплин
	public List<Discipline> getAll()
	{
		return _context.Disciplines.OrderBy(it => it.Name).ToList();
	}
}


