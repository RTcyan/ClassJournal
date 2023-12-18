using System;
using Domain.Model;

namespace API.DTOs
{
	public class MarkLogDTO
	{
        public Guid Id { get; set; }
        public string Student { get; set; } = string.Empty;
        public ScheduleDTO ScheduleItem { get; set; } = null!;
        public int value { get; set; }
    }
}

