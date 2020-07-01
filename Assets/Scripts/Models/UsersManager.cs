using System.Collections.Generic;

namespace FizreFox
{
    public class UsersManager
    {
        public User CurrentUser;

        public List<User> WorldTopUsers = new List<User>();

        public List<User> FriendsUsers = new List<User>();
    }
}