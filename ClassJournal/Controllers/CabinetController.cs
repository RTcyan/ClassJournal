using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Infrastructure.Repository;
using Domain.Model;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("cabinet")]
public class CabinetController : ControllerBase
{
	private Context _context;

	private CabinetRepository _cabinetRepository;
	private CabinetTypeRepository _cabinetTypeRepository;

	public CabinetController(Context context)
	{
		_context = context;
		_cabinetRepository = new CabinetRepository(context);
		_cabinetTypeRepository = new CabinetTypeRepository(context);
	}

	[HttpGet("all")]
	public IActionResult getAll()
	{
		List<CabinetDTO> cabinetDTOs = new List<CabinetDTO>();

		List<Cabinet> cabinetes = _cabinetRepository.getAll();

		foreach (Cabinet cabinet in cabinetes)
        {
			cabinetDTOs.Add(new CabinetDTO
			{
				Id = cabinet.Id,
				Number = cabinet.Number,
				CabinetType = cabinet.CabinetType.Name,
				PlaceCount = cabinet.PlaceCount,
			});
		}

		return Ok(cabinetDTOs);
	}

	[HttpGet("{guid}")]
	public IActionResult getById(Guid guid)
	{
		Cabinet? cabinet = _cabinetRepository.getById(guid);
		if (cabinet == null)
		{
			return NotFound();
		}
        return Ok(new CabinetDTO
		{
            Id = cabinet.Id,
            Number = cabinet.Number,
            CabinetType = cabinet.CabinetType.Name,
            PlaceCount = cabinet.PlaceCount,
        });
	}


    [HttpPut]
    public IActionResult update(CabinetUpdateDTO cabinetDTO)
    {
		Cabinet? oldCabinet = _cabinetRepository.getById(cabinetDTO.Id);
		if (oldCabinet == null)
		{
			return NotFound();
		}

        CabinetType? cabinetType = _cabinetTypeRepository.getById(cabinetDTO.CabinetTypeId);
        if (cabinetType == null)
        {
            return NotFound("Teacher is not found");
        }

        Cabinet newCabinet = new Cabinet
        {
            Id = cabinetDTO.Id,
            Number = cabinetDTO.Number,
            CabinetType = cabinetType,
            PlaceCount = cabinetDTO.PlaceCount,
        };
        Cabinet updatedCabinet = _cabinetRepository.Update(newCabinet);
        return Ok(new CabinetDTO
        {
            Id = updatedCabinet.Id,
            Number = updatedCabinet.Number,
            CabinetType = updatedCabinet.CabinetType.Name,
            PlaceCount = updatedCabinet.PlaceCount,
        });
    }
}


