using System;
using Domain.Model;

namespace API.DTOs
{
	public class ScheduleCreateDTO
	{
        public Guid CabinetId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid GradeId { get; set; }
        public Guid DisciplineId { get; set; }
        public DateTime DateTime { get; set; }
    }
}

