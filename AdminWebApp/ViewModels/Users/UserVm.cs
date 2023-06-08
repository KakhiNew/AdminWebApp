namespace AdminWebApp.ViewModels;

public class UserVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTimeOffset? LastLoginTime { get; set; }
    public DateTimeOffset RegistrationTime { get; set; }
    public bool Blocked { get; set; }
    public bool Selected { get; set; }
   
}
