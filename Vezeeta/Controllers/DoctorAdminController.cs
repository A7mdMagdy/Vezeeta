using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Data;
using Vezeeta.Models;
using Vezeeta.RepoServices;
using Vezeeta.ViewModels;

namespace Vezeeta.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorAdminController : Controller
    {

		public IDoctorRepository DoctorRepo { get; }

		public DoctorAdminController(IDoctorRepository repository)
        {
            
			DoctorRepo = repository;
		}

        // GET: DoctorViewModels
        public IActionResult Index()
        {
            List<AppUser> model = DoctorRepo.GetAllDoctor();

            return View(model);
        }

        // GET: DoctorViewModels/Details/5
        public IActionResult Details(string id)
        {

            var doctorViewModel = DoctorRepo.GetDoctorDetails(id);
            if (doctorViewModel == null)
            {
                return NotFound();
            }

            return View(doctorViewModel);
        }

        // GET: DoctorViewModels/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: DoctorViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,firstName,lastName,birthDate,Address,Role,Gender,Specialization,typeOfSpecialization,fees,Image")] DoctorViewModel doctorViewModel)
        {
            AppUser user = new AppUser();
            user.firstName = doctorViewModel.firstName;
            user.lastName = doctorViewModel.lastName;
            user.Gender = doctorViewModel.Gender;
            user.Address = doctorViewModel.Address;
            user.birthDate = doctorViewModel.birthDate;
            user.Image = doctorViewModel.Image;
            user.fees = doctorViewModel.fees;
            user.Role = doctorViewModel.Role;
            user.Specialization = doctorViewModel.Specialization;
            user.typeOfSpecialization = doctorViewModel.typeOfSpecialization;
            if (ModelState.IsValid)
            {
                DoctorRepo.InsertDoctor(user);
               
                return RedirectToAction(nameof(Index));
            }
            return View(doctorViewModel);
        }

        // GET: DoctorViewModels/Edit/
        
        public IActionResult Edit(string id)
        {
            var doctorViewModel =  DoctorRepo.GetDoctorDetails(id);

            if (doctorViewModel == null)
            {
                return NotFound();
            }
            return View(doctorViewModel);
        }

        // POST: DoctorViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,firstName,lastName,birthDate,Address,Role,Gender,Specialization,typeOfSpecialization,fees,Image")] AppUser doctorViewModel)
        {
            if (ModelState.IsValid)
            {
                
                AppUser user = new AppUser();
                user.firstName = doctorViewModel.firstName;
                user.lastName = doctorViewModel.lastName;
                user.Gender = doctorViewModel.Gender;
                user.Address = doctorViewModel.Address;
                user.birthDate = doctorViewModel.birthDate;
                user.Image = doctorViewModel.Image;
                user.fees = doctorViewModel.fees;
                user.Role = doctorViewModel.Role;
                user.Specialization = doctorViewModel.Specialization;
                user.typeOfSpecialization = doctorViewModel.typeOfSpecialization;
                DoctorRepo.UpdateDoctor(id,user);
                return RedirectToAction(nameof(Index));
            }
            return View(doctorViewModel);
        }

        // GET: DoctorViewModels/Delete/5
        public IActionResult Delete(string id)
        {
           
            var doctorViewModel =DoctorRepo.GetDoctorDetails(id);
            if (doctorViewModel == null)
            {
                return NotFound();
            }

            return View(doctorViewModel);
        }

        // POST: DoctorViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var doctorViewModel = DoctorRepo.GetDoctorDetails(id);
			if (doctorViewModel != null)
            {
                DoctorRepo.DeleteDoctor(id);
            }

            
            return RedirectToAction(nameof(Index));
        }

       
    }
}
