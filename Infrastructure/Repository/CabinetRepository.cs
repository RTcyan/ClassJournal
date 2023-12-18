using System.Diagnostics;
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
		return _context.Cabinets.Include(it => it.CabinetType).ToList();
	}

	// Получение по ID
	public Cabinet? getById(Guid id)
	{
		return _context.Cabinets.Where(it => it.Id == id)
			.Include(c => c.CabinetType)
            .FirstOrDefault();
	}

    // Обновление информации о кабинете
    public Cabinet Update(Cabinet cabinet)
    {
        Cabinet? oldCabinet = this.getById(cabinet.Id);
        if (oldCabinet == null)
        {
            throw new Exception("NotFound");
        }
        oldCabinet.CabinetType = cabinet.CabinetType;
        oldCabinet.Number = cabinet.Number;
        oldCabinet.PlaceCount = cabinet.PlaceCount;
        _context.SaveChanges();

        return oldCabinet;
    }
}


