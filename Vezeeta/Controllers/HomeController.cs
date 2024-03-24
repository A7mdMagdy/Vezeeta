using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vezeeta.Data;
using Vezeeta.Models;

namespace Vezeeta.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext Context;
        private readonly UserManager<AppUser> UserManager;
        public HomeController(ILogger<HomeController> logger , ApplicationDbContext _context , UserManager<AppUser> _userManager)
        {
            _logger = logger;
            Context = _context;
            UserManager = _userManager;
        }

        public IActionResult Index()
        {
            var users = Context.Users.ToList();
            return View(users);
        }
        [Authorize]
        public IActionResult Privacy()
        {
            var id = UserManager.GetUserId(User);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
