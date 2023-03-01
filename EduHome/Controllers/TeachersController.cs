using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EduHome.Controllers
{
    public class TeachersController : Controller
    {
        private readonly AppDbContext _db;
        public TeachersController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Teacher> Teachers = _db.Teachers.ToList();
            return View(Teachers);
        }
    }
}
