namespace chesscodem.Model
{
    public class Matches
    {
        public int match_id { get; set; }
        public int player1_id { get; set; }
        public int player2_id { get; set; }
        public DateTime match_date { get; set; }
        public string match_level { get; set; }
        public int winner_id { get; set; }


    }
}
