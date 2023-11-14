using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class CabinetRepository
{
	private readonly Context _context;

	public Context UnitOfWork
	{
		get
		{
			return _context;
		}
	}

	public CabinetRepository(Context context)
	{
		_context = context ?? throw new ArgumentException(nameof(context));
	}

	// Получение всех кабинетов
	public List<Cabinet> getAll()
	{
		return _context.Cabinets.ToList();
	}

	// Обновление информации о кабинете
	public Cabinet Update(Cabinet cabinet)
	{
		return _context.Cabinets.Update(cabinet).Entity;
	}

	// Получение по ID
	public Cabinet? GetByid(Guid id)
	{
		return _context.Cabinets.Where(it => it.Id == id)
			.Include(c => c.CabinetType)
			.FirstOrDefault();
	}
}


