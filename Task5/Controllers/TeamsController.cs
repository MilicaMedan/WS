using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Task5;
using Task5.Models;

namespace Task5.Controllers
{
    public class TeamsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HomeController> _logger;

        public TeamsController(AppDbContext context, IWebHostEnvironment env, ILogger<HomeController> logger)
        {
            _context = context;
            _env = env;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Team main page has been accessed");
            return View(await _context.Teams.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogInformation("Team details page has been accessed");
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            string path = "/Players/Index/" + id;
            return Redirect(path);
        }


        public IActionResult Create()
        {
            _logger.LogInformation("Team create page has been accessed");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,Name")] Team team, [FromForm] IFormFile file)
        {
            _logger.LogInformation("Create team");
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0) {
                    string filePath = $"{_env.WebRootPath}/logo/{file.FileName}";
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    team.LogoPath = filePath;
                }
                    team.NumeberOfPlayers = 0;
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }


        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.TeamId == id);
        }
    }
}
