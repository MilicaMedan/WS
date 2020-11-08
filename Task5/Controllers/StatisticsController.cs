using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task5.Models;

namespace Task5.Controllers
{
    public class StatisticController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PlayersController> _logger;

        public StatisticController(AppDbContext context, ILogger<PlayersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var teams = _context.Teams.ToList();
            var matches = _context.Matches.ToList();
            var players = _context.Players.ToList();
            var goals = _context.Goals.ToList();
            List<PlayerStatistic> playerStatistics = new List<PlayerStatistic>();
            List<TeamStatistic> teamStatistics = new List<TeamStatistic>();
            Statistic statistics = new Statistic();

            foreach (Team t in teams) {
                TeamStatistic ts = new TeamStatistic();
                ts.team = t;

                int numOfWins = 0;
                int numOfDraws = 0;
                int numOfLosses = 0;

                foreach (Match m in matches) {
                    if(m.HostTeamId == t.TeamId || (m.GuestTeamId == t.TeamId))
                    {
                        string result = m.Result;
                        int res1 = int.Parse(result.Split(":")[0]);
                        int res2 = int.Parse(result.Split(":")[1]);
                        if (res1 > res2)
                        {
                            if (m.HostTeamId == t.TeamId)
                            {
                                numOfWins++;
                            }
                            else {
                                numOfLosses++;
                            }
                            
                        } else if (res1 == res2) {
                            numOfDraws++;
                        }
                    }
                }

                ts.numOfDraws = numOfDraws;
                ts.numOfLosses = numOfLosses;
                ts.numOfWins = numOfWins;

                teamStatistics.Add(ts);

            }

            foreach (Player p in players) {
                PlayerStatistic ps = new PlayerStatistic();
                Team team = _context.Teams.FirstOrDefault(t => t.TeamId == p.TeamId);
                p.Team = team;
                ps.player = p;
                int numOfMatches = 0;
                int numOfGoals = 0;

                foreach (Goal g in goals) { 
                    if(g.PlayerId == p.PlayerId)
                    {
                        numOfMatches++;
                        numOfGoals += g.NumberOfGoals;
                    }
                }

                ps.numOfGoals = numOfGoals;
                ps.numOfMatches = numOfMatches;
                playerStatistics.Add(ps);

            }

            statistics.playerStatistics = playerStatistics;
            statistics.teamStatistics = teamStatistics;
            return View("~/Views/Statistics/Index.cshtml",statistics);
        }
    }
}