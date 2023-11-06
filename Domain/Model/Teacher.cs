namespace Domain.Model;

public class Teacher
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public Discipline Discipline { get; set; } = null!;
    public int PersonalLifeNumber { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
}
