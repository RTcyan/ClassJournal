using System;
using Domain.Model;

namespace API.DTOs
{
	public class ScheduleUpdateDTO
	{
        public Guid Id { get; set; }
        public Guid CabinetId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid GradeId { get; set; }
        public DateTime DateTime { get; set; }
    }
}

