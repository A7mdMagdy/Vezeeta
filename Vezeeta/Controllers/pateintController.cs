using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Data;
using Vezeeta.Models;
using Vezeeta.ViewModels;

namespace Vezeeta.Controllers
{
    public class pateintController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> userManager;

        public ApplicationDbContext Context { get; }

        public pateintController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            this.userManager = userManager;
        }
        public pateintController(ApplicationDbContext context)
        {
            Context = context;
        }

        // GET: pateintController/Create
        public ActionResult Book()
        {
            return View();
        }

        // POST: pateintController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Book(BookInfo bookInfo)
        {
            try
            {
                AppUser User = await userManager.FindByEmailAsync(bookInfo.Email);
                if (User == null)
                {
                    return View();
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
