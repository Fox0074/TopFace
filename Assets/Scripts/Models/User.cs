using System;

[Serializable]
public class User
{
    public string UserId { get; set; }
    public string UserName { get; set; } = "";
    public float Donate { get; set; }
    public string Avatar { get; set; } = "";
}

[Serializable]
public class VkUserData
{
    public string id { get; set; }
    public string first_name { get; set; } = "";
    public string last_name { get; set; }
    public string photo_200 { get; set; } = "";
    public bool online { get; set; }

    public User ConvertToUser()
    {
        User user = new User();
        user.Avatar = photo_200;
        user.UserName = string.Format("{0} {1}", first_name, last_name);
        user.UserId = id;
        return user;
    }
}


