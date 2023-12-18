namespace API.DTOs
{
	public class CabinetUpdateDTO
	{
        public Guid Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public int PlaceCount { get; set; }
        public Guid CabinetTypeId { get; set; }
    }
}

