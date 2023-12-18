using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Infrastructure.Repository;
using Domain.Model;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("markLog")]
public class MarkLogController : ControllerBase
{
	private Context _context;

	private ScheduleRepository _scheduleRepository;
	private StudentRepository _studentRepository;
	private MarkLogRepository _markLogRepository;

    public MarkLogController(Context context)
	{
		_context = context;
        _scheduleRepository = new ScheduleRepository(context);
        _studentRepository = new StudentRepository(context);
        _markLogRepository = new MarkLogRepository(context);
    }

	[HttpGet("all")]
	public IActionResult getAll()
	{
		List<MarkLogDTO> markLogDTOs = new List<MarkLogDTO>();

		List<MarkLog> markLogs = _markLogRepository.getAll();

		foreach (MarkLog markLog in markLogs)
		{
			markLogDTOs.Add(new MarkLogDTO
			{
				Id = markLog.Id,
				ScheduleItem = new ScheduleDTO
                {
                    Cabinet = markLog.ScheduleItem.Cabinet.Number,
                    Discipline = markLog.ScheduleItem.Teacher.Discipline.Name,
                    Teacher = markLog.ScheduleItem.Teacher.FullName,
                    Grade = markLog.ScheduleItem.Grade.Name,
                    DateTime = markLog.ScheduleItem.DateTime,
                    Id = markLog.ScheduleItem.Id,
                },
                Student = markLog.Student.FullName,
                value = markLog.value,
			});
		}

		return Ok(markLogDTOs);
	}

	[HttpGet("{guid}")]
	public IActionResult getById(Guid guid)
	{
		MarkLog? markLog = _markLogRepository.getById(guid);
		if (markLog == null)
		{
			return NotFound();
		}
        return Ok(new MarkLogDTO
		{
            Id = markLog.Id,
            ScheduleItem = new ScheduleDTO
            {
                Cabinet = markLog.ScheduleItem.Cabinet.Number,
                Discipline = markLog.ScheduleItem.Teacher.Discipline.Name,
                Teacher = markLog.ScheduleItem.Teacher.FullName,
                Grade = markLog.ScheduleItem.Grade.Name,
                DateTime = markLog.ScheduleItem.DateTime,
                Id = markLog.ScheduleItem.Id,
            },
            Student = markLog.Student.FullName,
            value = markLog.value,
        });
	}

    [HttpGet("student/{guid}")]
    public IActionResult getByStudentId(Guid guid)
    {
        List<MarkLogDTO> markLogDTOs = new List<MarkLogDTO>();

        List<MarkLog> markLogs = _markLogRepository.getByStudentId(guid);

        foreach (MarkLog markLog in markLogs)
        {
            markLogDTOs.Add(new MarkLogDTO
            {
                Id = markLog.Id,
                ScheduleItem = new ScheduleDTO
                {
                    Cabinet = markLog.ScheduleItem.Cabinet.Number,
                    Discipline = markLog.ScheduleItem.Teacher.Discipline.Name,
                    Teacher = markLog.ScheduleItem.Teacher.FullName,
                    Grade = markLog.ScheduleItem.Grade.Name,
                    DateTime = markLog.ScheduleItem.DateTime,
                    Id = markLog.ScheduleItem.Id,
                },
                Student = markLog.Student.FullName,
                value = markLog.value,
            });
        }

        return Ok(markLogDTOs);
    }

    [HttpPost]
	public IActionResult create(MarkLogCreateDTO markLogDTO)
	{
		Schedule? schedule = _scheduleRepository.getById(markLogDTO.ScheduleItemId);
		if (schedule == null)
		{
			return NotFound("Schedule is not found");
		}
		Student? student = _studentRepository.getById(markLogDTO.StudentId);
        if (student == null)
        {
            return NotFound("Student is not found");
        }

        MarkLog markLog = new MarkLog
        {
			ScheduleItem = schedule,
            Student = student,
            value = markLogDTO.value,
		};
        MarkLog newMarkLog = _markLogRepository.Add(markLog);
		return Ok(new MarkLogDTO
		{
            Id = newMarkLog.Id,
            ScheduleItem = new ScheduleDTO
            {
                Cabinet = newMarkLog.ScheduleItem.Cabinet.Number,
                Discipline = newMarkLog.ScheduleItem.Teacher.Discipline.Name,
                Teacher = newMarkLog.ScheduleItem.Teacher.FullName,
                Grade = newMarkLog.ScheduleItem.Grade.Name,
                DateTime = newMarkLog.ScheduleItem.DateTime,
                Id = newMarkLog.ScheduleItem.Id,
            },
            Student = newMarkLog.Student.FullName,
            value = newMarkLog.value,
        });
	}

    [HttpPut]
    public IActionResult update(MarkLogUpdateDTO markLogDTO)
    {
        MarkLog? oldMarkLog = _markLogRepository.getById(markLogDTO.Id);
        if (oldMarkLog == null)
        {
            return NotFound();
        }

        Schedule? schedule = _scheduleRepository.getById(markLogDTO.ScheduleItemId);
        if (schedule == null)
        {
            return NotFound("Schedule is not found");
        }
        Student? student = _studentRepository.getById(markLogDTO.StudentId);
        if (student == null)
        {
            return NotFound("Student is not found");
        }

        MarkLog newMarkLog = new MarkLog
        {
			Id = markLogDTO.Id,
            ScheduleItem = schedule,
            Student = student,
            value = markLogDTO.value,
        };
        MarkLog updatedMarkLog = _markLogRepository.Update(newMarkLog);
        return Ok(new MarkLogDTO
        {
            Id = updatedMarkLog.Id,
            ScheduleItem = new ScheduleDTO
            {
                Cabinet = updatedMarkLog.ScheduleItem.Cabinet.Number,
                Discipline = updatedMarkLog.ScheduleItem.Teacher.Discipline.Name,
                Teacher = updatedMarkLog.ScheduleItem.Teacher.FullName,
                Grade = updatedMarkLog.ScheduleItem.Grade.Name,
                DateTime = updatedMarkLog.ScheduleItem.DateTime,
                Id = updatedMarkLog.ScheduleItem.Id,
            },
            Student = updatedMarkLog.Student.FullName,
            value = updatedMarkLog.value,
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult delete(Guid guid)
    {
        MarkLog? oldMarkLog = _markLogRepository.getById(guid);
        if (oldMarkLog == null)
        {
            return NotFound();
        }

        
        _markLogRepository.Delete(oldMarkLog);
        return Ok();
    }
}


