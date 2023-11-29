using Domain.Model;

namespace Infrastructure.Repository;
public class GradeTypeRepository
{
	private readonly Context _context;

	public Context UnitOfWork
	{
		get
		{
			return _context;
		}
	}

	public GradeTypeRepository(Context context)
	{
		_context = context ?? throw new ArgumentException(nameof(context));
	}

	// Получение всех типов класса
	public List<GradeType> getAll()
	{
		return _context.GradeTypes.ToList();
	}

	public GradeType? getById(Guid guid)
	{
        return _context.GradeTypes.Where(it => it.Id == guid).FirstOrDefault();
    }
}


