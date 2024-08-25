namespace chesscodem.Model
{
    public class Player_Sponsors
    {
        public int player_id { get; set; }
        public int sponsor_id { get; set; }
        public int sponsorship_amount { get; set; }
        public DateTime contract_start_date { get; set; }
        public DateTime contract_end_date { get; set; }
        public int current_world_ranking { get; set; }
        public int total_matches_played { get; set; }
    }
}
