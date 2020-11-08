using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Task5.Models;

namespace Task5.Controllers
{
    public class MatchesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public MatchesController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Match main page has been accessed");
            DateTime now = DateTime.Now;
            var matches = await _context.Matches.ToListAsync();
            foreach (Match match in matches)
            {
                if (now > match.Time) {
                    match.StatussId = 2;
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
            }

            var appDbContext = _context.Matches.Include(m => m.GuestTeam).Include(m => m.HostTeam).Include(m => m.statuss);
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogInformation("Match details page has been accessed");
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.GuestTeam)
                .Include(m => m.HostTeam)
                .Include(m => m.statuss)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Match create page has been accessed");
            ViewData["GuestTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId");
            ViewData["HostTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId");
            ViewData["StatussId"] = new SelectList(_context.Statusses, "StatussId", "StatussId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchId,HostTeamId,GuestTeamId,Time,Place")] Match match, [FromForm] string hostPlayersString, [FromForm] string guestPlayersString)
        {
            _logger.LogInformation("Create match");
            bool matchOnSameDay = false;
            var matches = _context.Matches.Where(m => (m.Time.Date == match.Time.Date && (m.HostTeamId == match.HostTeamId || m.HostTeamId == match.GuestTeamId || m.GuestTeamId == match.GuestTeamId || m.GuestTeamId == match.HostTeamId) ));
            if (matches.Any()) {
                matchOnSameDay = true;
            }

            string[] hostPlayers = hostPlayersString.Split(',');
            string[] guestPlayers = guestPlayersString.Split(',');
            if (ModelState.IsValid && !matchOnSameDay && !(hostPlayersString.Equals("") || guestPlayersString.Equals("") || (hostPlayers.Length-1) < 6 || (guestPlayers.Length - 1) < 6))
            {
                match.Result = "";
                match.StatussId = 1;
                _context.Add(match);
                _context.SaveChanges();

                Match added = _context.Matches.Last();
                for (int i = 0; i < (hostPlayers.Length - 1); i++) {
                    int id = int.Parse(hostPlayers[i]);
                    Goal goal = new Goal();
                    goal.MatchId = added.MatchId;
                    goal.PlayerId = id;
                    goal.NumberOfGoals = 0;
                    _context.Goals.Add(goal);
                    await _context.SaveChangesAsync();
                }

                
                for (int i = 0; i < (guestPlayers.Length - 1); i++)
                {
                    int id = int.Parse(guestPlayers[i]);
                    Goal goal = new Goal();
                    goal.MatchId = added.MatchId;
                    goal.PlayerId = id;
                    goal.NumberOfGoals = 0;
                    _context.Goals.Add(goal);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            if ((hostPlayers.Length - 1) < 6 || (guestPlayers.Length - 1) < 6) {
                ViewBag.Message = "You have to chose at least 6 players from team!";
            }

            if (matchOnSameDay)
            {
                ViewBag.Message = "A team can not have more than one match on same day!";
            }

            ViewData["GuestTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", match.GuestTeamId);
            ViewData["HostTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", match.HostTeamId);
            ViewData["StatussId"] = new SelectList(_context.Statusses, "StatussId", "StatussId", match.StatussId);
            return View(match);
        }

        [HttpGet]
        public JsonResult GetPlayers(int? id)
        {
            _logger.LogInformation("Get players");
            var players1 = _context.Players.Where(p => p.TeamId == id).ToListAsync();
            return Json(players1);
        }



        [HttpGet]
        public IActionResult FinishMatch(int? id)
        {
            _logger.LogInformation("Finish match");
            Match match = _context.Matches.FirstOrDefault(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }
            match.StatussId = 3;
            try
            {
                _context.Update(match);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (match == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult CancleMatch(int? id)
        {
            _logger.LogInformation("Cancle match");
            Match match = _context.Matches.FirstOrDefault(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }
            match.StatussId = 4;
            try
            {
                _context.Update(match);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (match == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }




        [HttpGet]
        public JsonResult GetGoalsByMatch(int? id)
        {
            _logger.LogInformation("Get goals by match");
            var goals = _context.Goals.Where(g => g.MatchId == id);
            foreach (Goal g in goals) {
                g.Player = _context.Players.FirstOrDefault(p => p.PlayerId == g.PlayerId);
            }
            var goals1 = goals.ToListAsync();
            return Json(goals1);
        }


        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.MatchId == id);
        }
    }
}
