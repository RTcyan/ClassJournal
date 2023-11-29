using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Infrastructure.Repository;
using Domain.Model;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("administrator")]
public class AdministratorController : ControllerBase
{
	private Context _context;

	private AdministratorRepository _administratorRepository;

	public AdministratorController(Context context)
	{
		_context = context;
        _administratorRepository = new AdministratorRepository(context);
	}

	[HttpGet("{guid}")]
	public IActionResult getById(Guid guid)
	{
		Administrator? administrator = _administratorRepository.getById(guid);
		if (administrator == null)
		{
			return NotFound();
		}
        return Ok(new AdministratorDTO
		{
            Id = administrator.Id,
            Birthday = administrator.Birthday,
            FullName = administrator.FullName,
            PersonalLifeNumber = administrator.PersonalLifeNumber,
            PhoneNumber = administrator.PhoneNumber,
        });
	}

	[HttpPost]
	public IActionResult create(AdministratorCreateDTO administratorDTO)
	{
		Administrator teacher = new Administrator
        {
            Birthday = administratorDTO.Birthday,
            FullName = administratorDTO.FullName,
            PersonalLifeNumber = administratorDTO.PersonalLifeNumber,
            PhoneNumber = administratorDTO.PhoneNumber,
        };
        Administrator newTeacher = _administratorRepository.Add(teacher);
		return Ok(new TeacherDTO
		{
            Id = newTeacher.Id,
            Birthday = newTeacher.Birthday,
            FullName = newTeacher.FullName,
            PersonalLifeNumber = newTeacher.PersonalLifeNumber,
            PhoneNumber = newTeacher.PhoneNumber,
        });
	}

    [HttpDelete("{guid}")]
    public IActionResult delete(Guid guid)
    {
        Administrator? oldAdministrator = _administratorRepository.getById(guid);
        if (oldAdministrator == null)
        {
            return NotFound();
        }

        
        _administratorRepository.Delete(oldAdministrator);
        return Ok();
    }
}


