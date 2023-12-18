using System;
using Domain.Model;

namespace API.DTOs
{
	public class StudentUpdateDTO
	{
        public Guid Id { get; set; }
        public Guid GradeId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string ParentsFullName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Sex { get; set; } = string.Empty;
        public int PersonalLifeNumber { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ParentsPhoneNumber { get; set; } = string.Empty;
    }
}

