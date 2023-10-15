using System;

namespace Authorization.Desktop.ViewModels.Users;

public class UserViewModel
{
    public long Id { get; set; }
    public string UserName { get; set; }=string.Empty;
    public string Password { get; set; } =string.Empty; 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set;}

}
