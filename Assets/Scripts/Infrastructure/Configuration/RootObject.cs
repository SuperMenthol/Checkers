namespace Assets.Scripts.Infrastructure.Configuration
{
    public class Rootobject
    {
        public Apiconnection APIConnection { get; set; }
    }

    public class Apiconnection
    {
        public string BaseAddress { get; set; }
        public Methods Methods { get; set; }
    }

    public class Methods
    {
        public string GetPlayerInfo { get; set; }
        public string Login { get; set; }
        public string CreatePlayer { get; set; }
        public string RenamePlayer { get; set; }
        public string PostMatchResult { get; set; }
    }
}