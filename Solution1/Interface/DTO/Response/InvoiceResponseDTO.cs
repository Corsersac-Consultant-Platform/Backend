namespace Interface.DTO.Response;

public class InvoiceResponseDTO
{
    public int Id { get; set; }
    public DateTime EmitDate { get; set; }
    public string Number { get; set; }
    public string Serie { get; set; }
    public string Register { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public int StatusId { get; set; }
}