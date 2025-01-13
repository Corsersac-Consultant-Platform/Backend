using System.Runtime.CompilerServices;

namespace Support.Models;

public partial class User
{
    public int Id { get; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public int RoleId { get; set; }
    
}

public partial class User
{
    public User()
    {
        Username = string.Empty;
        Password = string.Empty;
        RoleId = 0;
    }
    
    public User(string username, string password, int roleId)
    {
        Username = username;
        Password = password;
        RoleId = roleId;
    }
    
    public void UpdatePassword(string password)
    {
        Password = password;
    }
}