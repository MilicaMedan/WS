using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Task5.Models;

namespace Task5.Controllers
{
    public class PlayersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PlayersController> _logger;

        public PlayersController(AppDbContext context, ILogger<PlayersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Cancle()
        {
            return Redirect("/Teams/Index");
        }

        public async Task<IActionResult> Index(int? id)
        {
            _logger.LogInformation("Player main page has been accessed");
            var players = await _context.Players.ToListAsync();
            var players1 = players.Where(p => p.TeamId == id);
            var team = await _context.Teams.FindAsync(id);
            ViewBag.TeamId = team.TeamId;
            ViewBag.TeamName = team.Name;
            ViewBag.TeamLogo = team.LogoPath;
            return View(players1);
        }



        [HttpPost]

        public IActionResult AddPlayer([FromForm] int teamId, [FromForm] string name)
        {
            _logger.LogInformation("Add player");
            Player player = new Player();
            player.TeamId = teamId;
            player.Name = name;
            Team team = _context.Teams.FirstOrDefault(t => t.TeamId == player.TeamId);
            if (team == null)
            {
                return NotFound();
            }
            team.NumeberOfPlayers++;
            try
            {
                _context.Add(player);
                _context.Update(team);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (team == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            string path = "/Players/Index/" + player.TeamId;
            return Redirect(path);

        }



        public async Task<IActionResult> Delete(int? id)
        {
            _logger.LogInformation("Delete player");
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            string path = "/Players/Index/" + player.TeamId;

            Team team = _context.Teams.FirstOrDefault(t => t.TeamId == player.TeamId);
            if (team == null || player == null)
            {
                return NotFound();
            }
            team.NumeberOfPlayers--;
            try
            {
                _context.Players.Remove(player);
                _context.Update(team);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (team == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Redirect(path);
        }


        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }
    }
}
