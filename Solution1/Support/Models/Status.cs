namespace Support.Models;

public partial class Status
{
    public int Id { get; }
    
    public string Type { get; set; }
    
}

public partial class Status
{
    public Status()
    {
        Type = string.Empty;
    }
    
    public Status(string type)
    {
        Type = type;
    }
}