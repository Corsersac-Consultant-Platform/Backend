namespace Interface.DTO.Request;

public class InvoiceRequestDTO
{
    public DateTime EmitDate { get; set; }
    public string Number { get; set; }
    public string Serie { get; set; }
    public string Register { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
}