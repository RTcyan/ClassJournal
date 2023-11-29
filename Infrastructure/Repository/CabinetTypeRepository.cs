using Domain.Model;
using Microsoft.EntityFrameworkCore;

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

    public CabinetType? getById(Guid guid)
    {
        return _context.CabinetTypes.Where(it => it.Id == guid).FirstOrDefault();
    }
}


