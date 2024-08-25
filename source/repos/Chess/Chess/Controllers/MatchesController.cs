using chesscodem.Data;
using chesscodem.Model;
using Microsoft.AspNetCore.Mvc;



namespace ChessCodeM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly ChessDao _dao;

        public MatchesController(ChessDao dao)
        {
            _dao = dao;
        }

        [HttpPost]
        public IActionResult CreateMatch([FromBody] Matches match)
        {
            if (PlayerExists(match.player1_id) && PlayerExists(match.player2_id))
            {
                _dao.AddMatch(match);
                return Ok("Match created successfully.");
            }
            return BadRequest("Players do not exist.");
        }

        private bool PlayerExists(int player_id)
        {
            return _dao.PlayerExists(player_id);
        }
    }
}
