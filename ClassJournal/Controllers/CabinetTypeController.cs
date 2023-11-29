using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Infrastructure.Repository;
using Domain.Model;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("cabinetType")]
public class CabinetTypeController : ControllerBase
{
	private Context _context;
	private CabinetTypeRepository _cabinetTypeRepository;

	public CabinetTypeController(Context context)
	{
		_context = context;
        _cabinetTypeRepository = new CabinetTypeRepository(context);
	}

	[HttpGet("all")]
	public IActionResult getAll()
	{
		List<CabinetTypeDTO> cabinetTypeDTOs = new List<CabinetTypeDTO>();

		List<CabinetType> cabinetTypes = _cabinetTypeRepository.getAll();

		foreach (CabinetType cabinetType in cabinetTypes)
		{
            cabinetTypeDTOs.Add(new CabinetTypeDTO
            {
				Id = cabinetType.Id,
				Name = cabinetType.Name,
			});
		}

		return Ok(cabinetTypeDTOs);
	}
}


