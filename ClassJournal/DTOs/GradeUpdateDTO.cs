using System;
using Domain.Model;

namespace API.DTOs
{
	public class GradeUpdateDTO
	{

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid GradeTypeId { get; set; }
        public Guid GradeTeacherId { get; set; }
    }
}

