namespace Support.Models;

public partial class Role
{
    public int Id { get; }
    public string Type { get; set; }
}

public partial class Role
{
    public Role()
    {
        Type = string.Empty;
    }
    
    public Role(string type)
    {
        Type = type;
    }
}