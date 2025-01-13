namespace Support.Models;

public partial class Invoice
{
    public int Id { get; }
    public DateTime EmitDate { get; set; }
    public string Number { get; set; }
    public string Serie { get; set; }
    public string Register { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public int StatusId { get; set; }
    
}

public partial class Invoice
{
    public Invoice()
    {
        StatusId = 0;
        EmitDate = DateTime.Now;
        Number = string.Empty;
        Serie = string.Empty;
        Register = string.Empty;
        Name = string.Empty;
        Amount = 0;
    }
    
    public Invoice(DateTime emitDate, string number, string serie, string register, string name, decimal amount, int statusId)
    {
        EmitDate = emitDate;
        Number = number;
        Serie = serie;
        Register = register;
        Name = name;
        Amount = amount;
        StatusId = statusId;
    }
    
    public void UpdateStatus(int statusId)
    {
        StatusId = statusId;
    }
}