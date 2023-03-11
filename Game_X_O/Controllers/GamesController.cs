using Game_X_O.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Game_X_O.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ModelContext _context;

        public GamesController(ModelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Game>>> Games()
        {
            return Ok(await _context.Games.Include(u => u.PlayerMoves).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Game>>> GameDetails(int id )
        {
            Game game;

            try
            {
                game = await _context.Games.Include(u => u.PlayerMoves).AsSplitQuery().FirstAsync(x => x.Id == id);
            }
            catch
            {
                return BadRequest("not found Game");
            }

            return Ok(game);
        }

    }
}
