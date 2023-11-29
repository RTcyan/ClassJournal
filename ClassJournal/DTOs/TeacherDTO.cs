﻿using System;
using Domain.Model;

namespace API.DTOs
{
	public class TeacherDTO
	{
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public Guid DisciplineId { get; set; }
        public int PersonalLifeNumber { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}

