namespace Domain.Model;
public class Schedule
{
	public Guid Id { get; set; }
	public Cabinet Cabinet { get; set; } = null!;
	public Teacher Teacher { get; set; } = null!;
	public Grade Grade { get; set; } = null!;
	public DateTime DateTime { get; set; }
}


