namespace HappyGames.SocialAPI
{
    public class SocialProfile
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public SocialGender Gender { get; set; }

        public string Name => string.Format("{0} {1}", FirstName, LastName);
    }
}
