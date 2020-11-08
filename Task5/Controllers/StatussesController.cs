using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Task5.Controllers
{
    public class StatussesController : Controller
    {
        private readonly AppDbContext _context;

        public StatussesController(AppDbContext context)
        {
            _context = context;
        }


        private bool StatussExists(int id)
        {
            return _context.Statusses.Any(e => e.StatussId == id);
        }
    }
}
