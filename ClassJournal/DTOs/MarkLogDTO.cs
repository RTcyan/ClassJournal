using System;
using Domain.Model;

namespace API.DTOs
{
	public class MarkLogDTO
	{
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ScheduleItemId { get; set; }
        public int value { get; set; }
    }
}

