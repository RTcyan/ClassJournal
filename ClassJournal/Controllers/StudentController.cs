using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Infrastructure.Repository;
using Domain.Model;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("student")]
public class StudentController : ControllerBase
{
	private Context _context;

	private GradeRepository _gradeRepository;
	private StudentRepository _studentRepository;

	public StudentController(Context context)
	{
		_context = context;
        _studentRepository = new StudentRepository(context);
        _gradeRepository = new GradeRepository(context);
	}

	[HttpGet("all")]
	public IActionResult getAll()
	{
		List<StudentDTO> studentDTOs = new List<StudentDTO>();

		List<Student> students = _studentRepository.getAll();

		foreach (Student student in students)
		{
			studentDTOs.Add(new StudentDTO
			{
				Id = student.Id,
				Birthday = student.Birthday,
				GradeId = student.Grade.Id,
				FullName = student.FullName,
				PersonalLifeNumber = student.PersonalLifeNumber,
				PhoneNumber = student.PhoneNumber,
			});
		}

		return Ok(studentDTOs);
	}

	[HttpGet("{guid}")]
	public IActionResult getById(Guid guid)
	{
		Student? student = _studentRepository.getById(guid);
		if (student == null)
		{
			return NotFound();
		}
        return Ok(new StudentDTO
		{
            Id = student.Id,
            Birthday = student.Birthday,
            GradeId = student.Grade.Id,
            FullName = student.FullName,
            PersonalLifeNumber = student.PersonalLifeNumber,
            PhoneNumber = student.PhoneNumber,
        });
	}

	[HttpPost]
	public IActionResult create(StudentCreateDTO studentDTO)
	{
		Grade? grade = _gradeRepository.getById(studentDTO.GradeId);
		if (grade == null)
		{
			return NotFound("Grade is not found");
		}
		Student student = new Student
        {
			Grade = grade,
			PhoneNumber = studentDTO.PhoneNumber,
			Birthday = studentDTO.Birthday,
			FullName = studentDTO.FullName,
			PersonalLifeNumber = studentDTO.PersonalLifeNumber,
		};
        Student newStudent = _studentRepository.Add(student);
		return Ok(new StudentDTO
		{
            Id = newStudent.Id,
            Birthday = newStudent.Birthday,
            GradeId = newStudent.Grade.Id,
            FullName = newStudent.FullName,
            PersonalLifeNumber = newStudent.PersonalLifeNumber,
            PhoneNumber = newStudent.PhoneNumber,
        });
	}

    [HttpPut]
    public IActionResult update(StudentDTO studentDTO)
    {
		Student? oldStudent = _studentRepository.getById(studentDTO.Id);
		if (oldStudent == null)
		{
			return NotFound();
		}

        Grade? grade = _gradeRepository.getById(studentDTO.GradeId);
        if (grade == null)
        {
            return NotFound("Grade is not found");
        }

        Student newStudent = new Student
        {
            Grade = grade,
            PhoneNumber = studentDTO.PhoneNumber,
            Birthday = studentDTO.Birthday,
            FullName = studentDTO.FullName,
            PersonalLifeNumber = studentDTO.PersonalLifeNumber,
        };
        Student updatedStudent = _studentRepository.Update(newStudent);
        return Ok(new StudentDTO
        {
            Id = updatedStudent.Id,
            Birthday = updatedStudent.Birthday,
            GradeId = updatedStudent.Grade.Id,
            FullName = updatedStudent.FullName,
            PersonalLifeNumber = updatedStudent.PersonalLifeNumber,
            PhoneNumber = updatedStudent.PhoneNumber,
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult delete(Guid guid)
    {
        Student? oldStudent = _studentRepository.getById(guid);
        if (oldStudent == null)
        {
            return NotFound();
        }

        
        _studentRepository.Delete(oldStudent);
        return Ok();
    }
}


