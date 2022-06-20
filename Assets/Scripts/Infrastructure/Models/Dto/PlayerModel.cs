using System;

namespace CheckerScoreAPI.Model
{
    public class PlayerModel
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}