using Microsoft.AspNetCore.Mvc;
using ProjetoAEDI.Data;
using ProjetoAEDI.Models;
using System.Diagnostics;

namespace ProjetoAEDI.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
