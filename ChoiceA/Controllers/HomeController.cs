using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChoiceA.Models;
using Microsoft.AspNetCore.Authorization;
using ChoiceA.Data;
using Microsoft.EntityFrameworkCore;

namespace ChoiceA.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "studentId");
            if (claim == null)
                return View(_db.Students);
            return RedirectToAction(nameof(Select), new { id = Convert.ToInt32(claim.Value) });
            
        }

        public IActionResult Select(int? id)
        {
            var student = _db.Students.Include(s => s.StudDiscs).SingleOrDefault(s => s.Id == id);
            var selDiscIds = student.StudDiscs.Select(p => p.DisciplineId);
            var discs = _db.Disciplines;

            var model = new HomeSelectViewModel { Student = student };
            model.SelDiscs = discs.Where(d => selDiscIds.Contains(d.Id)).OrderBy(d => d.Title);
            model.NonSelDiscs = discs.Except(model.SelDiscs).OrderBy(d => d.Title);
            return View(model);
        }

        [HttpPost]
        public IActionResult Select(int studentId, int[] selDiscIds)
        {
            var student = _db.Students.Include(s => s.StudDiscs).SingleOrDefault(s => s.Id == studentId);
            student.StudDiscs = new List<StudDisc>();
            foreach (var id in selDiscIds)
                student.StudDiscs.Add(new StudDisc { StudentId = student.Id, DisciplineId = id });
            _db.SaveChanges();

            return RedirectToAction("Select");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
