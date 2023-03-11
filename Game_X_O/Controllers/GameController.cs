using Game_X_O.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Runtime.Intrinsics.X86;
using System.Numerics;
using System.Reflection;

namespace Game_X_O.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ModelContext _context;

        public GameController(ModelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Game>>> NewGame()
        {

            Game game = new Game { PlayerTurn = 1, Winner = 0, Stage = "active" ,error = "not" };

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return Ok(await _context.Games.ToListAsync());
        }

        [HttpGet("{gameId},{column},{row}")]
        public async Task<ActionResult<List<Game>>> PlayerMove(int gameId, int column, int row)
        {
            Game game;
            try
            {
                 game = await _context.Games.Include(u => u.PlayerMoves).AsSplitQuery().FirstAsync(x => x.Id == gameId);
            }
            catch
            {
                return BadRequest("not found Game");
            }

            if (game.Stage == "completed")
            {
                return BadRequest("game completed");
            }

            if (column > 3 && row > 3)
            {
                return BadRequest("out of range column or row");
            }

          
            int[,] arrXO = { { -1, -2, -3 }, { -4, -5, -6 }, { -7, -8, -9 } }; 

            foreach (var player in game.PlayerMoves)
            {
                arrXO[player.column - 1, player.row - 1] = player.Player;
            }
           
            if (arrXO[column - 1, row - 1] > 0) 
            {
                //return BadRequest("there has already been such a move");

                game.error = "there has already been such a move";

            
            }
            else
            {
                arrXO[column - 1, row - 1] = game.PlayerTurn;

                game.error = "not";

                string value;

                if (game.PlayerTurn == 1) value = "O"; else value = "X";

                var playermove = new PlayerMove { Player = game.PlayerTurn, column = column, row = row, value = value };

                game.PlayerMoves.Add(playermove);


                if (arrXO[0, 0] == arrXO[1, 0] && arrXO[1, 0] == arrXO[2, 0]) {

                    return winner_run(_context, game);
                }
                else if (arrXO[0, 1] == arrXO[1, 1] && arrXO[1, 1] == arrXO[2, 1]) { return winner_run(_context, game); }
                else if (arrXO[0, 2] == arrXO[1, 2] && arrXO[1, 2] == arrXO[2, 2]) { return winner_run(_context, game); }

                else if (arrXO[0, 0] == arrXO[0, 1] && arrXO[0, 1] == arrXO[0, 2]) { return winner_run(_context, game); }
                else if (arrXO[1, 0] == arrXO[1, 1] && arrXO[1, 1] == arrXO[1, 2]) { return winner_run(_context, game); }
                else if (arrXO[2, 0] == arrXO[2, 1] && arrXO[2, 1] == arrXO[2, 2]) { return winner_run(_context, game); }

                else if (arrXO[0, 0] == arrXO[1, 1] && arrXO[1, 1] == arrXO[2, 2]) { return winner_run(_context, game); }
                else if (arrXO[2, 0] == arrXO[1, 1] && arrXO[1, 1] == arrXO[0, 2]) { return winner_run(_context, game); }


                if (game.PlayerTurn == 1) game.PlayerTurn = 2; else game.PlayerTurn = 1;

                BadRequestObjectResult winner_run(ModelContext context, Game game)
                {
                    game.Winner = game.PlayerTurn;
                    game.PlayerTurn = 0;
                    game.Stage = "completed";
                    context.Games.Update(game);
                    context.SaveChanges();

                    return BadRequest($"player won the game {game.Winner}");

                }

                if(game.PlayerMoves.Count >= 9) {

                    game.Winner = 0;
                    game.PlayerTurn = 0;
                    game.Stage = "completed";

                    _context.Games.Update(game);
                    await _context.SaveChangesAsync();

                    return BadRequest($"draw in the gameid {game.Id} ");

                }
           
            }

            _context.Games.Update(game);
            await _context.SaveChangesAsync();

            return Ok(game);
        }
  
  
    }
}
