using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class AdministratorRepository
{
	private readonly Context _context;

	public Context UnitOfWork
	{
		get
		{
			return _context;
		}
	}

	public AdministratorRepository(Context context)
	{
		_context = context ?? throw new ArgumentException(nameof(context));
	}

	// Получение администратора по Id
	public Administrator? getById(Guid id)
	{
		return _context.Administrators.Where(it => it.Id == id).FirstOrDefault();
	}

	// Добавление администратора
	public Administrator Add(Administrator administrator)
	{
		Administrator newAdministrator = _context.Administrators.Add(administrator).Entity;
		_context.SaveChanges();

        return newAdministrator;
	}

	// Удаление администратора
	public void Delete(Administrator administrator)
	{
		_context.Administrators.Remove(administrator);
		_context.SaveChanges();
	}
}


