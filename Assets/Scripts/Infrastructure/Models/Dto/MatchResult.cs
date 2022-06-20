using System;

namespace CheckerScoreAPI.Model
{
    public class MatchResult
    {
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int WinnerID { get; set; }
        public DateTime MatchTime { get; set; }
    }
}