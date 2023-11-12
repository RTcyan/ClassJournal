namespace Domain.Model;
public class Grade
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public GradeType GradeType { get; set; } = null!;
	public Teacher GradeTeacher { get; set; } = null!;
}


