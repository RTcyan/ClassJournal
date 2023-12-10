namespace API.DTOs
{
	public class GradeDTO
	{
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string GradeType { get; set; } = string.Empty;
        public string GradeTeacher { get; set; } = string.Empty;
    }
}

