using Domain.Model;

namespace Infrastructure.Repository;
public class CabinetTypeRepository
{
	private readonly Context _context;

	public Context UnitOfWork
	{
		get
		{
			return _context;
		}
	}

	public CabinetTypeRepository(Context context)
	{
		_context = context ?? throw new ArgumentException(nameof(context));
	}

	// Получение всех типов кабинета
	public List<CabinetType> getAll()
	{
		return _context.CabinetTypes.OrderBy(it => it.Name).ToList();
	}
}


