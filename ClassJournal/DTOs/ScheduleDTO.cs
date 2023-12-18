using System;
using Domain.Model;

namespace API.DTOs
{
	public class ScheduleDTO
	{
        public Guid Id { get; set; }
        public string Cabinet { get; set; } = string.Empty;
        public string Teacher { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string Discipline { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
    }
}

