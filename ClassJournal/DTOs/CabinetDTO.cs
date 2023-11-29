namespace API.DTOs
{
	public class CabinetDTO
	{
        public Guid Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public int PlaceCount { get; set; }
        public Guid CabinetTypeId { get; set; }
    }
}

