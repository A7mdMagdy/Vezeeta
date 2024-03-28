using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Data;
using Vezeeta.Models;
using Vezeeta.ViewModels;

namespace Vezeeta.Controllers
{
    public class PatientAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PatientViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.Where(u=>u.Role == Models.Role.Patient).ToListAsync());
        }

        // GET: PatientViewModels/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientViewModel = await _context.Users
				.FirstOrDefaultAsync(m => m.Id == id);
            if (patientViewModel == null)
            {
                return NotFound();
            }

            return View(patientViewModel);
        }

        // GET: PatientViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatientViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("firstName,lastName,Email,birthDate,Address,Role,Gender,Image")] PatientViewModel patientViewModel)
        {
            AppUser user = new AppUser();
            user.firstName = patientViewModel.firstName;
            user.lastName = patientViewModel.lastName;
            user.Email = patientViewModel.Email;
            user.birthDate = patientViewModel.birthDate;
            user.Address = patientViewModel.Address;
            user.Role = patientViewModel.Role;
            user.Gender = patientViewModel.Gender;
            user.Image = patientViewModel.Image;
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: PatientViewModels/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Users.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: PatientViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("firstName,lastName,Email,birthDate,Address,Role,Gender,Image")] PatientViewModel patientViewModel)
        {

            AppUser user = await _context.Users.FindAsync(id);
            user.firstName = patientViewModel.firstName;
            user.lastName = patientViewModel.lastName;
            user.Email = patientViewModel.Email;
            user.birthDate = patientViewModel.birthDate;
            user.Address = patientViewModel.Address;
            user.Role = patientViewModel.Role;
            user.Gender = patientViewModel.Gender;
            user.Image = patientViewModel.Image;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patientViewModel);
        }

        // GET: PatientViewModels/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: PatientViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var patient = await _context.Users.FindAsync(id);
            if (patient != null)
            {
                _context.Users.Remove(patient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
