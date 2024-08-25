//using Npgsql;
//using System.Collections.Generic;
//using System.Data;
//using chesscodem.Model;

//namespace chesscodem.Data
//{
//    public class ChessDao
//    {
//        private readonly string _connectionString;

//        public ChessDao(IConfiguration configuration)
//        {
//            _connectionString = configuration.GetConnectionString("PostgreDB");
//        }

//        public void AddMatch(Matches match)
//        {
//            using (var connection = new NpgsqlConnection(_connectionString))
//            {
//                connection.Open();

//                // Use CALL for stored procedure
//                using (var cmd = new NpgsqlCommand("CALL  sp_AddMatch(@Player1Id, @Player2Id, @Player1Won)", connection))
//                {
//                    cmd.Parameters.AddWithValue("@Player1Id", match.player1_id);
//                    cmd.Parameters.AddWithValue("@Player2Id", match.player2_id);
//                    cmd.Parameters.AddWithValue("@Player1Won", match.player1_id == match.winner_id);

//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }

//        public bool PlayerExists(int player_id)
//        {
//            using (var connection = new NpgsqlConnection(_connectionString))
//            {
//                connection.Open();

//                // Use CALL for stored procedure
//                using (var cmd = new NpgsqlCommand("CALL  sp_CheckPlayerExists(@PlayerId)", connection))
//                {
//                    cmd.Parameters.AddWithValue("@PlayerId", player_id);

//                    var result = cmd.ExecuteScalar();
//                    return (bool)result;
//                }
//            }
//        }


//        public List<Player> GetPlayersByCountrySorted(string country)
//        {
//            var players = new List<Player>();
//            using (var connection = new NpgsqlConnection(_connectionString))
//            {
//                connection.Open();
//                using (var cmd = new NpgsqlCommand(" ", connection))
//                {
//                    cmd.Parameters.AddWithValue("@Country", country);

//                    using (var reader = cmd.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            players.Add(new Player
//                            {
//                                player_id = reader.GetInt32(0),
//                                first_name = reader.GetString(1),
//                                last_name = reader.GetString(2),
//                                country = reader.GetString(3),
//                                current_world_ranking = reader.GetInt32(4),
//                                total_matches_played = reader.GetInt32(5)
//                            });
//                        }
//                    }
//                }
//            }
//            return players;
//        }
//        public List<PlayerPerformance> GetPlayersAboveAverageWins()
//        {
//            var playersAboveAverage = new List<PlayerPerformance>();
//            using (var connection = new NpgsqlConnection(_connectionString))
//            {
//                connection.Open();
//                using (var cmd = new NpgsqlCommand(" sp_GetPlayersAboveAverageWins", connection))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;

//                    using (var reader = cmd.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {

//                            playersAboveAverage.Add(new PlayerPerformance
//                            {
//                                full_name = reader.GetString(0),
//                                matches_win = reader.GetInt32(1),
//                                win_percentage = reader.GetDecimal(2)
//                            });
//                        }
//                    }
//                }
//            }
//            return playersAboveAverage;
//        }
//        public List<PlayerPerformance> GetPlayerPerformance()
//        {
//            var performances = new List<PlayerPerformance>();

//            using (var connection = new NpgsqlConnection(_connectionString))
//            {
//                connection.Open();
//                using (var cmd = new NpgsqlCommand(" sp_GetPlayerPerformance", connection))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;

//                    using (var reader = cmd.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            performances.Add(new PlayerPerformance
//                            {
//                                full_name = reader.GetString(0),
//                                total_matches_played = reader.GetInt32(1),
//                                matches_win = reader.GetInt32(2),
//                                win_percentage = reader.GetDecimal(3)
//                            });
//                        }
//                    }
//                }
//            }

//            return performances;
//        }


//    }
//}

using Npgsql;
using System.Collections.Generic;
using System.Data;
using chesscodem.Model;

namespace chesscodem.Data
{
    public class ChessDao
    {
        private readonly string _connectionString;

        public ChessDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgreDB");
        }

        public void AddMatch(Matches match)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = @"
                    INSERT INTO  Matches (player1_id, player2_id, match_date, match_level, winner_id) 
                    VALUES (@Player1Id, @Player2Id, CURRENT_DATE, 'International', @WinnerId)";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Player1Id", match.player1_id);
                    cmd.Parameters.AddWithValue("@Player2Id", match.player2_id);
                    cmd.Parameters.AddWithValue("@WinnerId", match.player1_id == match.winner_id ? match.player1_id : (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool PlayerExists(int player_id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT EXISTS(SELECT 1 FROM players WHERE player_id = @PlayerId)";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@PlayerId", player_id);

                    var result = cmd.ExecuteScalar();
                    return (bool)result;
                }
            }
        }

        public List<Player> GetPlayersByCountrySorted(string country)
        {
            var players = new List<Player>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = @"
                    SELECT player_id, first_name, last_name, country, current_world_ranking, total_matches_played 
                    FROM  players
                    WHERE country = @Country 
                    ORDER BY current_world_ranking";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Country", country);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            players.Add(new Player
                            {
                                player_id = reader.GetInt32(0),
                                first_name = reader.GetString(1),
                                last_name = reader.GetString(2),
                                country = reader.GetString(3),
                                current_world_ranking = reader.GetInt32(4),
                                total_matches_played = reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            return players;
        }

        public List<PlayerPerformance> GetPlayersAboveAverageWins()
        {
            var playersAboveAverage = new List<PlayerPerformance>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var sql = @"
        SELECT 
    p.first_name || ' ' || p.last_name AS full_name,
    COUNT(CASE WHEN m.winner_id = p.player_id THEN 1 END) AS matches_win,
    ROUND((COUNT(CASE WHEN m.winner_id = p.player_id THEN 1 END) * 100.0) / NULLIF(p.total_matches_played, 0), 2) AS win_percentage,
    p.total_matches_played
FROM 
    players p 
LEFT JOIN 
    matches m ON p.player_id IN (m.player1_id, m.player2_id)
GROUP BY 
    p.player_id, p.first_name, p.last_name, p.total_matches_played";


                //var sql = @"SELECT * FROM playerperformancesummary;";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            playersAboveAverage.Add(new PlayerPerformance
                            {
                                full_name = reader.GetString(0),
                                matches_win = reader.GetInt32(1),
                                win_percentage = reader.GetDecimal(2),
                                total_matches_played = reader.GetInt32(3)
                            });
                        }
                    }
                }
            }
            return playersAboveAverage;
        }

        public List<PlayerPerformance> GetPlayerPerformance()
        {
            var performances = new List<PlayerPerformance>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = @"
                    SELECT 
                    p.first_name || ' ' || p.last_name AS full_name,
                    COUNT(m.match_id) AS total_matches_played,
                    COUNT(CASE WHEN m.winner_id = p.player_id THEN 1 END) AS total_matches_won,
                    COALESCE((COUNT(CASE WHEN m.winner_id = p.player_id THEN 1 END) * 100.0) / COUNT(m.match_id), 0) AS win_percentage
                    FROM 
                    Players p
                    JOIN 
                    Matches m ON p.player_id IN (m.player1_id, m.player2_id)
                    GROUP BY 
                    p.player_id";


                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            performances.Add(new PlayerPerformance
                            {
                                full_name = reader.GetString(0),
                                total_matches_played = reader.GetInt32(1),
                                matches_win = reader.GetInt32(2),
                                win_percentage = reader.GetDecimal(3)
                            });
                        }
                    }
                }
            }

            return performances;
        }
    }
}

