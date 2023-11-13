namespace Domain.Model;
public class MarkLog
{
	public Guid Id { get; set; }
	public Student Student { get; set; } = null!;
	public Schedule ScheduleItem { get; set; } = null!;
	public int value { get; set; }
}


