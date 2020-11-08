using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Task5.Models;

namespace Task5.Controllers
{
    public class APIController : Controller
    {
        private readonly AppDbContext _context;

        public APIController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetMatches()
        {
            var matches = _context.Matches.Include(m => m.GuestTeam).Include(m => m.HostTeam).Include(m => m.statuss).ToListAsync();
            List<MatchAndGoals> matchesAndGoals = new List<MatchAndGoals>();
            foreach(Match match in matches.Result){
                MatchAndGoals pom = new MatchAndGoals();
                pom.Match = match;
                var goals = _context.Goals.ToList();
                pom.Goals = goals.FindAll(g => g.MatchId == match.MatchId);
            }
            return Json(matchesAndGoals);
        }
    }
}