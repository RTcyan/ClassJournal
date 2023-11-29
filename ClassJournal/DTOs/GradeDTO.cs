using System;
using Domain.Model;

namespace API.DTOs
{
	public class GradeCreateDTO
	{
        public string Name { get; set; } = string.Empty;
        public Guid GradeTypeId { get; set; }
        public Guid GradeTeacherId { get; set; }
    }
}

