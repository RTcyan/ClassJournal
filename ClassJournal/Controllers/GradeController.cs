﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Infrastructure.Repository;
using Domain.Model;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("grade")]
public class GradeController : ControllerBase
{
	private Context _context;

	private GradeRepository _gradeRepository;
	private TeacherRepository _teacherRepository;
	private GradeTypeRepository _gradeTypeRepository;

	public GradeController(Context context)
	{
		_context = context;
		_gradeRepository = new GradeRepository(context);
		_gradeTypeRepository = new GradeTypeRepository(context);
		_teacherRepository = new TeacherRepository(context);
	}

	[HttpGet("all")]
	public IActionResult getAll()
	{
		List<GradeDTO> gradeDTOs = new List<GradeDTO>();

		List<Grade> grades = _gradeRepository.getAll();

		foreach (Grade grade in grades)
		{
			gradeDTOs.Add(new GradeDTO
			{
				Id = grade.Id,
				Name = grade.Name,
				GradeTeacherId = grade.GradeTeacher.Id,
				GradeTypeId = grade.GradeType.Id,
			});
		}

		return Ok(gradeDTOs);
	}

	[HttpGet("{guid}")]
	public IActionResult getById(Guid guid)
	{
		Grade? grade = _gradeRepository.getById(guid);
		if (grade == null)
		{
			return NotFound();
		}
        return Ok(new GradeDTO
		{
            Id = grade.Id,
            Name = grade.Name,
            GradeTeacherId = grade.GradeTeacher.Id,
            GradeTypeId = grade.GradeType.Id,
        });
	}

	[HttpPost]
	public IActionResult create(GradeCreateDTO gradeDTO)
	{
		Teacher? teacher = _teacherRepository.getById(gradeDTO.GradeTeacherId);
		if (teacher == null)
		{
			return NotFound("Teacher is not found");
		}
		GradeType? gradeType = _gradeTypeRepository.getById(gradeDTO.GradeTypeId);
        if (gradeType == null)
        {
            return NotFound("Grade type is not found");
        }
		Grade grade = new Grade
		{
			GradeTeacher = teacher,
			GradeType = gradeType,
			Name = gradeDTO.Name,
		};
        Grade newGrade = _gradeRepository.Add(grade);
		return Ok(new GradeDTO
		{
			GradeTeacherId = newGrade.GradeTeacher.Id,
			Id = newGrade.Id,
			GradeTypeId = newGrade.GradeType.Id,
			Name = newGrade.Name,
		});
	}

    [HttpPut]
    public IActionResult update(GradeDTO gradeDTO)
    {
		Grade? oldGrade = _gradeRepository.getById(gradeDTO.Id);
		if (oldGrade == null)
		{
			return NotFound();
		}

        Teacher ? teacher = _teacherRepository.getById(gradeDTO.GradeTeacherId);
        if (teacher == null)
        {
            return NotFound("Teacher is not found");
        }
        GradeType? gradeType = _gradeTypeRepository.getById(gradeDTO.GradeTypeId);
        if (gradeType == null)
        {
            return NotFound("Grade type is not found");
        }

        Grade newGrade = new Grade
        {
			Id = gradeDTO.Id,
            GradeTeacher = teacher,
            GradeType = gradeType,
            Name = gradeDTO.Name,
        };
        Grade updatedGrade = _gradeRepository.Update(newGrade);
        return Ok(new GradeDTO
        {
            GradeTeacherId = updatedGrade.GradeTeacher.Id,
            Id = updatedGrade.Id,
            GradeTypeId = updatedGrade.GradeType.Id,
            Name = updatedGrade.Name,
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult delete(Guid guid)
    {
        Grade? oldGrade = _gradeRepository.getById(guid);
        if (oldGrade == null)
        {
            return NotFound();
        }

        
        _gradeRepository.Delete(oldGrade);
        return Ok();
    }
}


