namespace Interface.DTO.Response;

public class UsageResponseDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Product { get; set; }
    public decimal Quantity { get; set; }
    public string UsageCenter { get; set; }
    public string VehicleIdentifier { get; set; }
}