using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Infrastructure.Repository;
using Domain.Model;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("discipline")]
public class DisciplineController : ControllerBase
{
	private Context _context;
	private DisciplineRepository _disciplineRepository;

	public DisciplineController(Context context)
	{
		_context = context;
		_disciplineRepository = new DisciplineRepository(context);
	}

	[HttpGet("all")]
	public IActionResult getAll()
	{
		List<DisciplineDTO> disciplineDTOs = new List<DisciplineDTO>();

		List<Discipline> disciplines = _disciplineRepository.getAll();

		foreach (Discipline discipline in disciplines)
		{
            disciplineDTOs.Add(new DisciplineDTO
            {
				Id = discipline.Id,
				Name = discipline.Name,
			});
		}

		return Ok(disciplineDTOs);
	}
}


