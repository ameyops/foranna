using Microsoft.AspNetCore.Mvc;
using chesscodem.Model;
using chesscodem.Data;

namespace chesscodem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly ChessDao _dao;

        public PlayersController(ChessDao dao)
        {
            _dao = dao;
        }

        [HttpGet("by-country")]
        public IActionResult GetPlayersByCountry([FromQuery] string country, [FromQuery] int ranking)
        {
            var players = _dao.GetPlayersByCountrySorted(country);
            return Ok(players);
        }

        [HttpGet("performance")]
        public IActionResult GetPlayerPerformance()
        {
            var performances = _dao.GetPlayerPerformance();
            return Ok(performances);
        }

        [HttpGet("above-average-wins")]
        public IActionResult GetPlayersAboveAverageWins()
        {
            var players = _dao.GetPlayersAboveAverageWins();
            return Ok(players);
        }
    }
}
