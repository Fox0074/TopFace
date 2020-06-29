using System;

[Serializable]
public class User
{
    public string UserId { get; set; }
    public string UserName { get; set; } = "";
    public float Donate { get; set; } 
    public string Avatar { get; set; } = "";
}
