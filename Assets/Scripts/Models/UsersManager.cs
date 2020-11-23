using System.Collections.Generic;

namespace FizerFox
{
    public class UsersManager
    {
        public User CurrentUser;

        public List<User> WorldTopUsers = new List<User>();

        public List<User> FriendsUsers = new List<User>();
    }
}