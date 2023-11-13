namespace Domain.Model;
public class Cabinet
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public int PlaceCount { get; set; }
    public CabinetType CabinetType { get; set; } = null!;
}


