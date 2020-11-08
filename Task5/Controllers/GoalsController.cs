using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Task5.Models;

namespace Task5.Controllers
{
    public class GoalsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public GoalsController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult AddGoal(int? idMatch, int? idPlayer)
        {
            _logger.LogInformation("Add goal");
            Goal goal = _context.Goals.FirstOrDefault(g => (g.MatchId == idMatch && g.PlayerId == idPlayer));
            Match match = _context.Matches.FirstOrDefault(m => m.MatchId == idMatch);
            Player player = _context.Players.FirstOrDefault(m => m.PlayerId == idPlayer);
            if (goal == null ||match == null || player == null)
            {
                return NotFound();
            }


            goal.NumberOfGoals++;
            string res = match.Result;
            if (player.TeamId == match.HostTeamId) {
                if (res.Equals(""))
                {
                    res = "1:0";
                }
                else {
                    string[] pom = res.Split(":");
                    int hostRes = int.Parse(pom[0]);
                    hostRes++;
                    res = hostRes + ":" + pom[1];
                }
            }else if (player.TeamId == match.GuestTeamId)
            {
                if (res.Equals(""))
                {
                    res = "0:1";
                }
                else
                {
                    string[] pom = res.Split(":");
                    int guestRes = int.Parse(pom[1]);
                    guestRes++;
                    res = pom[0] + ":" + guestRes;
                }
            }
            match.Result = res;
            try
            {
                _context.Update(goal);
                _context.Update(match);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (goal == null || match == null || player == null)
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



        private bool GoalExists(int id)
        {
            return _context.Goals.Any(e => e.GoalId == id);
        }
    }
}
