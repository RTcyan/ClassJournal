using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Infrastructure.Repository;
using Domain.Model;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("teacher")]
public class TeacherController : ControllerBase
{
	private Context _context;

	private DisciplineRepository _disciplineRepository;
	private TeacherRepository _teacherRepository;

	public TeacherController(Context context)
	{
		_context = context;
        _disciplineRepository = new DisciplineRepository(context);
		_teacherRepository = new TeacherRepository(context);
	}

	[HttpGet("all")]
	public IActionResult getAll()
	{
		List<TeacherDTO> teacherDTOs = new List<TeacherDTO>();

		List<Teacher> teachers = _teacherRepository.getAll();

		foreach (Teacher teacher in teachers)
		{
			teacherDTOs.Add(new TeacherDTO
			{
				Id = teacher.Id,
				Birthday = teacher.Birthday,
				Discipline = teacher.Discipline.Name,
				FullName = teacher.FullName,
				PersonalLifeNumber = teacher.PersonalLifeNumber,
				PhoneNumber = teacher.PhoneNumber,
			});
		}

		return Ok(teacherDTOs);
	}

	[HttpGet("{guid}")]
	public IActionResult getById(Guid guid)
	{
		Teacher? teacher = _teacherRepository.getById(guid);
		if (teacher == null)
		{
			return NotFound();
		}
        return Ok(new TeacherDTO
		{
            Id = teacher.Id,
            Birthday = teacher.Birthday,
            Discipline = teacher.Discipline.Name,
            FullName = teacher.FullName,
            PersonalLifeNumber = teacher.PersonalLifeNumber,
            PhoneNumber = teacher.PhoneNumber,
        });
	}

	[HttpPost]
	public IActionResult create(TeacherCreateDTO teacherDTO)
	{
		Discipline? discipline = _disciplineRepository.getById(teacherDTO.DisciplineId);
		if (discipline == null)
		{
			return NotFound("Discipline is not found");
		}
		Teacher teacher = new Teacher
        {
			Discipline = discipline,
			PhoneNumber = teacherDTO.PhoneNumber,
			Birthday = teacherDTO.Birthday,
			FullName = teacherDTO.FullName,
			PersonalLifeNumber = teacherDTO.PersonalLifeNumber,
		};
        Teacher newTeacher = _teacherRepository.Add(teacher);
		return Ok(new TeacherDTO
		{
            Id = newTeacher.Id,
            Birthday = newTeacher.Birthday,
            Discipline = newTeacher.Discipline.Name,
            FullName = newTeacher.FullName,
            PersonalLifeNumber = newTeacher.PersonalLifeNumber,
            PhoneNumber = newTeacher.PhoneNumber,
        });
	}

    [HttpPut]
    public IActionResult update(TeacherUpdateDTO teacherDTO)
    {
		Teacher? oldTeacher = _teacherRepository.getById(teacherDTO.Id);
		if (oldTeacher == null)
		{
			return NotFound();
		}

        Discipline? discipline = _disciplineRepository.getById(teacherDTO.DisciplineId);
        if (discipline == null)
        {
            return NotFound("Discipline is not found");
        }

        Teacher newTeacher = new Teacher
        {
            Discipline = discipline,
            PhoneNumber = teacherDTO.PhoneNumber,
            Birthday = teacherDTO.Birthday,
            FullName = teacherDTO.FullName,
            PersonalLifeNumber = teacherDTO.PersonalLifeNumber,
        };
        Teacher updatedTeacher = _teacherRepository.Update(newTeacher);
        return Ok(new TeacherDTO
        {
            Id = updatedTeacher.Id,
            Birthday = updatedTeacher.Birthday,
            Discipline = updatedTeacher.Discipline.Name,
            FullName = updatedTeacher.FullName,
            PersonalLifeNumber = updatedTeacher.PersonalLifeNumber,
            PhoneNumber = updatedTeacher.PhoneNumber,
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult delete(Guid guid)
    {
        Teacher? oldTeacher = _teacherRepository.getById(guid);
        if (oldTeacher == null)
        {
            return NotFound();
        }

        
        _teacherRepository.Delete(oldTeacher);
        return Ok();
    }
}


