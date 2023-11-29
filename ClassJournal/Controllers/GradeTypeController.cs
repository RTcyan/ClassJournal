using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Infrastructure.Repository;
using Domain.Model;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("gradeType")]
public class GradeTypeController : ControllerBase
{
	private Context _context;
	private GradeTypeRepository _gradeTypeRepository;

	public GradeTypeController(Context context)
	{
		_context = context;
		_gradeTypeRepository = new GradeTypeRepository(context);
	}

	[HttpGet("all")]
	public IActionResult getAll()
	{
		List<GradeTypeDTO> gradeTypeDTOs = new List<GradeTypeDTO>();

		List<GradeType> gradeTypes = _gradeTypeRepository.getAll();

		foreach (GradeType gradeType in gradeTypes)
		{
            gradeTypeDTOs.Add(new GradeTypeDTO
            {
				Id = gradeType.Id,
				Name = gradeType.Name,
			});
		}

		return Ok(gradeTypeDTOs);
	}
}


