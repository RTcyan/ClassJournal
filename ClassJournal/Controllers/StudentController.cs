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
				Grade = student.Grade.Name,
				FullName = student.FullName,
				PersonalLifeNumber = student.PersonalLifeNumber,
				PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                ParentsFullName = student.ParentsFullName,
                ParentsPhoneNumber = student.ParentsPhoneNumber,
                Sex = student.Sex,
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
            Grade = student.Grade.Name,
            FullName = student.FullName,
            PersonalLifeNumber = student.PersonalLifeNumber,
            PhoneNumber = student.PhoneNumber,
            Address = student.Address,
            ParentsFullName = student.ParentsFullName,
            ParentsPhoneNumber = student.ParentsPhoneNumber,
            Sex = student.Sex,
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
			Address = studentDTO.Address,
			Sex = studentDTO.Sex,
			ParentsPhoneNumber = studentDTO.ParentsPhoneNumber,
			ParentsFullName = studentDTO.ParentsFullName,
		};
        Student newStudent = _studentRepository.Add(student);
		return Ok(new StudentDTO
		{
            Id = newStudent.Id,
            Birthday = newStudent.Birthday,
            Grade = student.Grade.Name,
            FullName = newStudent.FullName,
            PersonalLifeNumber = newStudent.PersonalLifeNumber,
            PhoneNumber = newStudent.PhoneNumber,
            Address = newStudent.Address,
            ParentsFullName = newStudent.ParentsFullName,
            ParentsPhoneNumber = newStudent.ParentsPhoneNumber,
            Sex = newStudent.Sex,
        });
	}

    [HttpPut]
    public IActionResult update(StudentUpdateDTO studentDTO)
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
            Grade = updatedStudent.Grade.Name,
            FullName = updatedStudent.FullName,
            PersonalLifeNumber = updatedStudent.PersonalLifeNumber,
            PhoneNumber = updatedStudent.PhoneNumber,
			Address = updatedStudent.Address,
			ParentsFullName = updatedStudent.ParentsFullName,
			ParentsPhoneNumber = updatedStudent.ParentsPhoneNumber,
			Sex = updatedStudent.Sex,
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


