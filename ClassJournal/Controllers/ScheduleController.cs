using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Infrastructure.Repository;
using Domain.Model;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("schedule")]
public class ScheduleController : ControllerBase
{
	private Context _context;

	private ScheduleRepository _scheduleRepository;
	private TeacherRepository _teacherRepository;
	private GradeRepository _gradeRepository;
	private CabinetRepository _cabinetRepository;
	private DisciplineRepository _disciplineRepository;

    public ScheduleController(Context context)
	{
		_context = context;
        _scheduleRepository = new ScheduleRepository(context);
        _gradeRepository = new GradeRepository(context);
		_teacherRepository = new TeacherRepository(context);
        _cabinetRepository = new CabinetRepository(context);
        _disciplineRepository = new DisciplineRepository(context);
    }

	[HttpGet("all")]
	public IActionResult getAll()
	{
		List<ScheduleDTO> scheduleDTOs = new List<ScheduleDTO>();

		List<Schedule> schedules = _scheduleRepository.getAll();

		foreach (Schedule schedule in schedules)
		{
			scheduleDTOs.Add(new ScheduleDTO
			{
				Id = schedule.Id,
				CabinetId = schedule.Cabinet.Id,
				DateTime = schedule.DateTime,
				GradeId = schedule.Grade.Id,
				TeacherId = schedule.Teacher.Id,
                DisciplineId = schedule.Discipline.Id,
			});
		}

		return Ok(scheduleDTOs);
	}

	[HttpGet("{guid}")]
	public IActionResult getById(Guid guid)
	{
		Schedule? schedule = _scheduleRepository.getById(guid);
		if (schedule == null)
		{
			return NotFound();
		}
        return Ok(new ScheduleDTO
		{
            Id = schedule.Id,
            CabinetId = schedule.Cabinet.Id,
            DateTime = schedule.DateTime,
            GradeId = schedule.Grade.Id,
            TeacherId = schedule.Teacher.Id,
            DisciplineId = schedule.Discipline.Id,
        });
	}

	[HttpPost]
	public IActionResult create(ScheduleCreateDTO scheduleDTO)
	{
		Teacher? teacher = _teacherRepository.getById(scheduleDTO.TeacherId);
		if (teacher == null)
		{
			return NotFound("Teacher is not found");
		}
		Cabinet? cabinet = _cabinetRepository.getById(scheduleDTO.CabinetId);
        if (cabinet == null)
        {
            return NotFound("Cabinet is not found");
        }
        Grade? grade = _gradeRepository.getById(scheduleDTO.GradeId);
        if (grade == null)
        {
            return NotFound("Grade is not found");
        }
        Discipline? discipline = _disciplineRepository.getById(scheduleDTO.DisciplineId);
        if (discipline == null)
        {
            return NotFound("Discipline is not found");
        }

        Schedule schedule = new Schedule
        {
			Teacher = teacher,
            Grade = grade,
            Cabinet = cabinet,
            DateTime = scheduleDTO.DateTime,
            Discipline = discipline,
		};
        Schedule newSchedule = _scheduleRepository.Add(schedule);
		return Ok(new ScheduleDTO
		{
            Id = newSchedule.Id,
            CabinetId = newSchedule.Cabinet.Id,
            DateTime = newSchedule.DateTime,
            GradeId = newSchedule.Grade.Id,
            TeacherId = newSchedule.Teacher.Id,
            DisciplineId = schedule.Discipline.Id,
        });
	}

    [HttpPut]
    public IActionResult update(ScheduleDTO scheduleDTO)
    {
        Schedule? oldSchedule = _scheduleRepository.getById(scheduleDTO.Id);
        if (oldSchedule == null)
        {
            return NotFound();
        }

        Grade? grade = _gradeRepository.getById(scheduleDTO.GradeId);
		if (grade == null)
		{
			return NotFound("Grade is not found");
		}
        Teacher? teacher = _teacherRepository.getById(scheduleDTO.TeacherId);
        if (teacher == null)
        {
            return NotFound("Teacher is not found");
        }
        Discipline? discipline = _disciplineRepository.getById(scheduleDTO.DisciplineId);
        if (discipline == null)
        {
            return NotFound("Discipline type is not found");
        }
        Cabinet? cabinet = _cabinetRepository.getById(scheduleDTO.CabinetId);
        if (cabinet == null)
        {
            return NotFound("Cabinet type is not found");
        }

        Schedule newSchedule = new Schedule
        {
			Id = scheduleDTO.Id,
            Discipline = discipline,
            DateTime = scheduleDTO.DateTime,
            Cabinet = cabinet,
            Grade = grade,
            Teacher = teacher,
        };
        Schedule updatedSchedule = _scheduleRepository.Update(newSchedule);
        return Ok(new ScheduleDTO
        {
            Id = updatedSchedule.Id,
            CabinetId = updatedSchedule.Cabinet.Id,
            DateTime = updatedSchedule.DateTime,
            GradeId = updatedSchedule.Grade.Id,
            TeacherId = updatedSchedule.Teacher.Id,
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult delete(Guid guid)
    {
        Schedule? oldSchedule = _scheduleRepository.getById(guid);
        if (oldSchedule == null)
        {
            return NotFound();
        }

        
        _scheduleRepository.Delete(oldSchedule);
        return Ok();
    }
}


